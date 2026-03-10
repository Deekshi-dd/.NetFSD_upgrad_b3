CREATE DATABASE AutoRetailDb;
USE AutoRetailDb;

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
    order_date DATE
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
(2,30),
(3,20);

CREATE TRIGGER trg_reduce_stock
ON order_items
AFTER INSERT
AS
BEGIN

    IF EXISTS
    (
        SELECT * FROM inserted i
        JOIN stocks s ON i.product_id = s.product_id
        WHERE s.quantity < i.quantity
    )
    BEGIN
        RAISERROR('Insufficient stock for product',16,1);
        ROLLBACK TRANSACTION;
        RETURN;
    END

    UPDATE s
    SET s.quantity = s.quantity - i.quantity
    FROM stocks s
    JOIN inserted i
    ON s.product_id = i.product_id;

END;

BEGIN TRANSACTION;

BEGIN TRY

INSERT INTO orders VALUES
(101,GETDATE());

INSERT INTO order_items VALUES
(1,101,1,10),   -- Brake Pad
(2,101,2,5);    -- Oil Filter

COMMIT TRANSACTION;
PRINT 'Order placed successfully';

END TRY

BEGIN CATCH

ROLLBACK TRANSACTION;
PRINT 'Transaction failed due to insufficient stock or other error';

END CATCH

BEGIN TRANSACTION;

BEGIN TRY

INSERT INTO orders VALUES
(102,GETDATE());

INSERT INTO order_items VALUES
(3,102,3,50);   -- stock only 20

COMMIT TRANSACTION;

END TRY

BEGIN CATCH

ROLLBACK TRANSACTION;
PRINT 'Order cancelled because stock is insufficient';

END CATCH

SELECT * FROM stocks;
