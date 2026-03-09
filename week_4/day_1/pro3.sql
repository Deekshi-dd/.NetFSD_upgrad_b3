CREATE DATABASE OrderStatusDb;

USE OrderStatusDb;
--Create Tables
CREATE TABLE stores
(
    store_id INT PRIMARY KEY,
    store_name VARCHAR(100)
);

-- Products
CREATE TABLE products
(
    product_id INT PRIMARY KEY,
    product_name VARCHAR(100)
);

-- Stocks
CREATE TABLE stocks
(
    product_id INT PRIMARY KEY,
    quantity INT,
    FOREIGN KEY(product_id) REFERENCES products(product_id)
);

-- Orders
CREATE TABLE orders
(
    order_id INT PRIMARY KEY,
    store_id INT,
    customer_name VARCHAR(100),
    order_status INT,  -- 4 = Completed
    order_date DATE,
    shipped_date DATE,
    FOREIGN KEY(store_id) REFERENCES stores(store_id)
);

-- Order Items
CREATE TABLE order_items
(
    order_item_id INT PRIMARY KEY,
    order_id INT,
    product_id INT,
    quantity INT,
    list_price DECIMAL(10,2),
    discount DECIMAL(5,2),
    FOREIGN KEY(order_id) REFERENCES orders(order_id),
    FOREIGN KEY(product_id) REFERENCES products(product_id)
);

INSERT INTO stores VALUES
(1,'Central Store'),
(2,'City Store');

-- Products
INSERT INTO products VALUES
(101,'Laptop'),
(102,'Mobile'),
(103,'Tablet');

-- Stocks
INSERT INTO stocks VALUES
(101,10),
(102,20),
(103,15);

-- Orders
INSERT INTO orders VALUES
(101,1,'Rahul',1,'2025-03-01',NULL),
(102,1,'Sneha',2,'2025-03-02','2025-03-03'),
(103,2,'Amit',3,'2025-03-03',NULL),
(104,2,'Ravi',4,'2025-03-04','2025-03-05');

-- Order Items
INSERT INTO order_items VALUES
(1,101,101,2,5000,0.10),
(2,101,102,1,3000,0.05),
(3,102,101,1,5000,0.10),
(4,103,103,3,2000,0.08),
(5,104,102,2,3000,0.05);

--Stock Auto-Update Trigger
CREATE TRIGGER trg_AutoUpdateStock
ON order_items
AFTER INSERT
AS
BEGIN
    BEGIN TRY
        -- Prevent insufficient stock
        IF EXISTS (
            SELECT 1
            FROM inserted i
            JOIN stocks s ON i.product_id = s.product_id
            WHERE s.quantity < i.quantity
        )
        BEGIN
            RAISERROR('Insufficient stock. Order cannot be processed.',16,1);
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -- Update stock
        UPDATE s
        SET s.quantity = s.quantity - i.quantity
        FROM stocks s
        JOIN inserted i
        ON s.product_id = i.product_id;
    END TRY
    BEGIN CATCH
        PRINT 'Error occurred while updating stock';
        ROLLBACK TRANSACTION;
    END CATCH
END;
GO

--Order Status Validation Trigger
CREATE TRIGGER trg_ValidateOrderStatus
ON orders
AFTER UPDATE
AS
BEGIN
    BEGIN TRY
        -- If order_status = 4, shipped_date must NOT be NULL
        IF EXISTS
        (
            SELECT 1
            FROM inserted
            WHERE order_status = 4
            AND shipped_date IS NULL
        )
        BEGIN
            RAISERROR('Cannot mark order as Completed. Shipped date must not be NULL.',16,1);
            ROLLBACK TRANSACTION;
            RETURN;
        END
    END TRY
    BEGIN CATCH
        PRINT 'Error while updating order status';
        ROLLBACK TRANSACTION;
    END CATCH
END;

--Optional: Cursor-Based Revenue Calculation
-- For management reporting, calculate store-wise revenue for completed orders

BEGIN TRY
BEGIN TRANSACTION

CREATE TABLE #OrderRevenue
(
    order_id INT,
    store_id INT,
    revenue DECIMAL(12,2)
)

-- Declare variables first
DECLARE @order_id INT;
DECLARE @store_id INT;
DECLARE @revenue DECIMAL(12,2);

-- Temporary table for storing revenue per order
IF OBJECT_ID('tempdb..#OrderRevenue') IS NOT NULL DROP TABLE #OrderRevenue;
CREATE TABLE #OrderRevenue
(
    order_id INT,
    store_id INT,
    revenue DECIMAL(12,2)
);

IF CURSOR_STATUS('global','order_cursor') >= -1
BEGIN
    CLOSE order_cursor;
    DEALLOCATE order_cursor;
END

-- 4️⃣ Declare cursor
DECLARE order_cursor CURSOR LOCAL STATIC FOR
SELECT order_id, store_id
FROM orders
WHERE order_status = 4;

OPEN order_cursor;


-- 4️⃣ Begin transaction and TRY…CATCH
BEGIN TRY
    BEGIN TRANSACTION;

    FETCH NEXT FROM order_cursor INTO @order_id, @store_id;

    WHILE @@FETCH_STATUS = 0
    BEGIN
        -- Compute revenue for the order
        SELECT @revenue = SUM(quantity * list_price * (1 - discount))
        FROM order_items
        WHERE order_id = @order_id;

        -- Insert into temp table
        INSERT INTO #OrderRevenue(order_id, store_id, revenue)
        VALUES (@order_id, @store_id, ISNULL(@revenue,0));

        FETCH NEXT FROM order_cursor INTO @order_id, @store_id;
    END

    CLOSE order_cursor;
    DEALLOCATE order_cursor;

    -- Store-wise revenue summary
    SELECT s.store_name, SUM(o.revenue) AS Total_Revenue
    FROM #OrderRevenue o
    JOIN stores s ON o.store_id = s.store_id
    GROUP BY s.store_name;

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    PRINT 'Error occurred during revenue calculation';
    ROLLBACK TRANSACTION;

    -- Ensure cursor is cleaned up
    IF CURSOR_STATUS('variable','order_cursor') >= -1
    BEGIN
        CLOSE order_cursor;
        DEALLOCATE order_cursor;
    END
END CATCH;
