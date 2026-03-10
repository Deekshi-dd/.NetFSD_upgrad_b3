CREATE DATABASE AutomicDb;

USE AutomicDb;

CREATE TABLE products
(
    product_id INT PRIMARY KEY,
    product_name VARCHAR(100)
);

CREATE TABLE stocks
(
    product_id INT PRIMARY KEY,
    quantity INT CHECK(quantity >= 0),
    FOREIGN KEY(product_id) REFERENCES products(product_id)
);

CREATE TABLE orders
(
    order_id INT PRIMARY KEY,
    order_status INT
);

CREATE TABLE order_items
(
    order_item_id INT PRIMARY KEY,
    order_id INT,
    product_id INT,
    quantity INT,
    FOREIGN KEY(order_id) REFERENCES orders(order_id),
    FOREIGN KEY(product_id) REFERENCES products(product_id)
);

INSERT INTO products VALUES
(1,'Brake Pad'),
(2,'Oil Filter'),
(3,'Clutch Plate');

INSERT INTO stocks VALUES
(1,50),
(2,40),
(3,30);

INSERT INTO orders VALUES
(101,1),   -- 1 = Confirmed
(102,1);

INSERT INTO order_items VALUES
(1,101,1,10),
(2,101,2,5),
(3,102,3,8);

SELECT * FROM products;
SELECT * FROM stocks;
SELECT * FROM orders;
SELECT * FROM order_items;


--Atomic Order Cancellation Using SAVEPOINT
DECLARE @order_id INT = 101;

BEGIN TRANSACTION;

BEGIN TRY

    PRINT 'Starting Order Cancellation...';

    -- SAVEPOINT before restoring stock
    SAVE TRANSACTION RestoreStockPoint;

    -- Restore stock quantities
    UPDATE s
    SET s.quantity = s.quantity + oi.quantity
    FROM stocks s
    JOIN order_items oi
        ON s.product_id = oi.product_id
    WHERE oi.order_id = @order_id;

    PRINT 'Stock restored successfully';

    -- Update order status to Rejected (3)
    UPDATE orders
    SET order_status = 3
    WHERE order_id = @order_id;

    PRINT 'Order status updated to Rejected';

    -- Commit transaction
    COMMIT TRANSACTION;

    PRINT 'Transaction committed successfully';

END TRY

BEGIN CATCH

    PRINT 'Error occurred during order cancellation';

    -- Rollback to savepoint
    ROLLBACK TRANSACTION RestoreStockPoint;

    PRINT 'Rolled back to SAVEPOINT';

    -- Rollback full transaction
    ROLLBACK TRANSACTION;

    PRINT 'Transaction rolled back';

END CATCH;

--Check Data After Cancellation

SELECT * FROM stocks;
SELECT * FROM orders;
