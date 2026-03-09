CREATE DATABASE ReportDb;
GO

USE ReportDb;

CREATE TABLE Stores(
    store_id INT PRIMARY KEY,
    store_name VARCHAR(50),
    city VARCHAR(50)
);

CREATE TABLE Customers(
    customer_id INT PRIMARY KEY,
    first_name VARCHAR(50),
    last_name VARCHAR(50)
);
CREATE TABLE Products(
    product_id INT PRIMARY KEY,
    product_name VARCHAR(100),
    list_price DECIMAL(10,2)
);
CREATE TABLE Orders(
    order_id INT PRIMARY KEY,
    customer_id INT,
    store_id INT,
    order_date DATE,
    FOREIGN KEY(customer_id) REFERENCES Customers(customer_id),
    FOREIGN KEY(store_id) REFERENCES Stores(store_id)
);
CREATE TABLE Order_Items(
    order_item_id INT PRIMARY KEY,
    order_id INT,
    product_id INT,
    quantity INT,
    list_price DECIMAL(10,2),
    discount DECIMAL(5,2),
    FOREIGN KEY(order_id) REFERENCES Orders(order_id),
    FOREIGN KEY(product_id) REFERENCES Products(product_id)
);
INSERT INTO Stores VALUES
(1,'Central Store','Hyderabad'),
(2,'City Store','Bangalore'),
(3,'Mall Store','Chennai');
INSERT INTO Customers VALUES
(1,'Rahul','Sharma'),
(2,'Sneha','Reddy'),
(3,'Amit','Verma');
INSERT INTO Products VALUES
(101,'Laptop',60000),
(102,'Mobile',25000),
(103,'Tablet',20000),
(104,'Headphones',2000),
(105,'Smart Watch',8000);
INSERT INTO Orders VALUES
(1001,1,1,'2025-01-10'),
(1002,2,2,'2025-02-15'),
(1003,3,1,'2025-03-01');
INSERT INTO Order_Items VALUES
(1,1001,101,1,60000,0.10),
(2,1001,104,2,2000,0.05),
(3,1002,102,1,25000,0.08),
(4,1003,103,1,20000,0.05),
(5,1003,105,2,8000,0.10);

CREATE PROCEDURE sp_TotalSalesPerStore
AS
BEGIN
    SELECT 
        s.store_name,
        SUM(oi.quantity * oi.list_price * (1 - oi.discount)) AS Total_Sales
    FROM Stores s
    JOIN Orders o ON s.store_id = o.store_id
    JOIN Order_Items oi ON o.order_id = oi.order_id
    GROUP BY s.store_name
END

EXEC sp_TotalSalesPerStore;

CREATE PROCEDURE sp_GetOrdersByDateRange
    @StartDate DATE,
    @EndDate DATE
AS
BEGIN
    SELECT 
        o.order_id,
        o.order_date,
        c.first_name,
        s.store_name
    FROM Orders o
    JOIN Customers c ON o.customer_id = c.customer_id
    JOIN Stores s ON o.store_id = s.store_id
    WHERE o.order_date BETWEEN @StartDate AND @EndDate
END

EXEC sp_GetOrdersByDateRange '2025-01-01','2025-02-28';

CREATE FUNCTION fn_CalculateDiscountPrice
(
    @Price DECIMAL(10,2),
    @Discount DECIMAL(5,2)
)
RETURNS DECIMAL(10,2)
AS
BEGIN
    DECLARE @FinalPrice DECIMAL(10,2)

    SET @FinalPrice = @Price * (1 - ISNULL(@Discount,0))

    RETURN @FinalPrice
END

SELECT 
product_id,
dbo.fn_CalculateDiscountPrice(list_price,0.10) AS DiscountPrice
FROM Products;

CREATE FUNCTION fn_Top5SellingProducts()
RETURNS TABLE
AS
RETURN
(
    SELECT TOP 5
        p.product_name,
        SUM(oi.quantity) AS TotalSold
    FROM Products p
    JOIN Order_Items oi ON p.product_id = oi.product_id
    GROUP BY p.product_name
    ORDER BY TotalSold DESC
)

SELECT * FROM dbo.fn_Top5SellingProducts();
