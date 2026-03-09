CREATE DATABASE cursorDb;
USE cursorDb;

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

-- Orders
INSERT INTO orders VALUES
(101,1,'Rahul',4,'2025-03-01','2025-03-02'),
(102,1,'Sneha',2,'2025-03-02',NULL),
(103,2,'Amit',4,'2025-03-03','2025-03-04'),
(104,2,'Ravi',4,'2025-03-04','2025-03-05');

-- Order Items
INSERT INTO order_items VALUES
(1,101,101,2,5000,0.10),
(2,101,102,1,3000,0.05),
(3,103,103,3,2000,0.08),
(4,104,102,2,3000,0.05);

--Declare variables at the top
DECLARE @order_id INT;
DECLARE @store_id INT;
DECLARE @revenue DECIMAL(12,2);

-- 4.2 Create temp table for revenue
IF OBJECT_ID('tempdb..#OrderRevenue') IS NOT NULL DROP TABLE #OrderRevenue;
CREATE TABLE #OrderRevenue
(
    order_id INT,
    store_id INT,
    revenue DECIMAL(12,2)
);

-- 4.3 Declare cursor for completed orders
IF CURSOR_STATUS('global','order_cursor') >= -1
BEGIN
    CLOSE order_cursor;
    DEALLOCATE order_cursor;
END

DECLARE order_cursor CURSOR LOCAL STATIC FOR
SELECT order_id, store_id
FROM orders
WHERE order_status = 4;

OPEN order_cursor;

-- 4.4 Begin TRY…CATCH with transaction
BEGIN TRY
    BEGIN TRANSACTION;

    FETCH NEXT FROM order_cursor INTO @order_id, @store_id;

    WHILE @@FETCH_STATUS = 0
    BEGIN
        -- Calculate revenue for current order
        SELECT @revenue = SUM(quantity * list_price * (1 - discount))
        FROM order_items
        WHERE order_id = @order_id;

        -- Insert into temporary table
        INSERT INTO #OrderRevenue(order_id, store_id, revenue)
        VALUES (@order_id, @store_id, ISNULL(@revenue,0));

        FETCH NEXT FROM order_cursor INTO @order_id, @store_id;
    END

    CLOSE order_cursor;
    DEALLOCATE order_cursor;

    -- Display store-wise revenue summary
    SELECT s.store_name, SUM(o.revenue) AS Total_Revenue
    FROM #OrderRevenue o
    JOIN stores s ON o.store_id = s.store_id
    GROUP BY s.store_name;

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    PRINT 'Error occurred during revenue calculation';
    ROLLBACK TRANSACTION;

    -- Ensure cursor cleanup
    IF CURSOR_STATUS('variable','order_cursor') >= -1
    BEGIN
        CLOSE order_cursor;
        DEALLOCATE order_cursor;
    END
END CATCH;
