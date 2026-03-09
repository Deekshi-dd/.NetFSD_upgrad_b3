CREATE DATABASE StockDb;
GO

USE StockDb;

CREATE TABLE products
(
    product_id INT PRIMARY KEY,
    product_name VARCHAR(100)
);
CREATE TABLE stocks
(
    product_id INT,
    quantity INT,
    PRIMARY KEY(product_id),
    FOREIGN KEY(product_id) REFERENCES products(product_id)
);
CREATE TABLE order_items
(
    order_item_id INT PRIMARY KEY,
    product_id INT,
    quantity INT,
    FOREIGN KEY(product_id) REFERENCES products(product_id)
);
INSERT INTO products VALUES
(101,'Laptop'),
(102,'Mobile'),
(103,'Tablet');

INSERT INTO stocks VALUES
(101,10),
(102,20),
(103,15);

CREATE TRIGGER trg_AutoUpdateStock
ON order_items
AFTER INSERT
AS
BEGIN
    BEGIN TRY
        -- Check if stock is sufficient
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

        -- Reduce stock quantity
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
END

INSERT INTO order_items VALUES(1,101,2);

SELECT * FROM stocks;
