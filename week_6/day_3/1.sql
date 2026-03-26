create database dbconnection;
use dbconnection;

CREATE TABLE Products (
    ProductId INT PRIMARY KEY IDENTITY(1,1),
    ProductName VARCHAR(100),
    Category VARCHAR(50),
    Price DECIMAL(10,2)
);


CREATE PROCEDURE sp_InsertProduct
    @ProductName VARCHAR(100),
    @Category VARCHAR(50),
    @Price DECIMAL(10,2)
AS
BEGIN
    INSERT INTO Products(ProductName, Category, Price)
    VALUES (@ProductName, @Category, @Price)
END

CREATE PROCEDURE sp_GetAllProducts
AS
BEGIN
    Select * FROM Products
END


CREATE PROCEDURE sp_UpdateProduct
    @ProductId INT,
    @ProductName VARCHAR(100),
    @Category VARCHAR(50),
    @Price DECIMAL(10,2)
AS
BEGIN
    UPDATE Products
    SET ProductName = @ProductName,
        Category = @Category,
        Price = @Price
    WHERE ProductId = @ProductId
END

CREATE PROCEDURE sp_DeleteProduct
    @ProductId INT
AS
BEGIN
    DELETE FROM Products WHERE ProductId = @ProductId
END

CREATE PROCEDURE sp_GetProductById
    @ProductId INT
AS
BEGIN
    SELECT * FROM Products WHERE ProductId = @ProductId
END

EXEC dbo.sp_InsertProduct 
    @ProductName = 'Laptop',
    @Category = 'Electronics',
    @Price = 50000;

EXEC dbo.sp_GetAllProducts;
