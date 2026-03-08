CREATE DATABASE EcommDb;
USE EcommDb;
CREATE TABLE Categories (
    category_id INT PRIMARY KEY,
    category_name VARCHAR(100) NOT NULL
);

CREATE TABLE Brands (
    brand_id INT PRIMARY KEY,
    brand_name VARCHAR(100) NOT NULL
);

CREATE TABLE Products (
    product_id INT PRIMARY KEY,
    product_name VARCHAR(100) NOT NULL,
    brand_id INT,
    category_id INT,
    model_year INT,
    list_price DECIMAL(10,2),

    FOREIGN KEY (brand_id) REFERENCES Brands(brand_id),
    FOREIGN KEY (category_id) REFERENCES Categories(category_id)
);

CREATE TABLE Customers (
    customer_id INT PRIMARY KEY,
    first_name VARCHAR(50),
    last_name VARCHAR(50),
    email VARCHAR(100),
    phone VARCHAR(20),
    city VARCHAR(50)
);

CREATE TABLE Stores (
    store_id INT PRIMARY KEY,
    store_name VARCHAR(100),
    city VARCHAR(50)
);

INSERT INTO Categories VALUES
(1,'SUV'),
(2,'Sedan'),
(3,'Hatchback'),
(4,'Truck'),
(5,'Electric');

INSERT INTO Brands VALUES
(1,'Toyota'),
(2,'Honda'),
(3,'Tesla'),
(4,'Ford'),
(5,'Hyundai');

INSERT INTO Products VALUES
(1,'Fortuner',1,1,2023,3500000),
(2,'Civic',2,2,2022,2000000),
(3,'Model S',3,5,2024,8000000),
(4,'F150',4,4,2023,4500000),
(5,'i20',5,3,2022,900000);

INSERT INTO Customers VALUES
(1,'Rahul','Sharma','rahul@gmail.com','9876543210','Bangalore'),
(2,'Priya','Reddy','priya@gmail.com','9876543211','Hyderabad'),
(3,'Arjun','Kumar','arjun@gmail.com','9876543212','Bangalore'),
(4,'Sneha','Patel','sneha@gmail.com','9876543213','Mumbai'),
(5,'Vikram','Singh','vikram@gmail.com','9876543214','Delhi');

INSERT INTO Stores VALUES
(1,'AutoWorld Bangalore','Bangalore'),
(2,'Speed Motors','Hyderabad'),
(3,'DriveHub','Mumbai'),
(4,'CarZone','Delhi'),
(5,'Urban Cars','Chennai');

SELECT 
    p.product_name,
    b.brand_name,
    c.category_name,
    p.model_year,
    p.list_price
FROM Products p
JOIN Brands b 
ON p.brand_id = b.brand_id
JOIN Categories c 
ON p.category_id = c.category_id;

SELECT *
FROM Customers
WHERE city = 'Bangalore';

SELECT 
    c.category_name,
    COUNT(p.product_id) AS total_products
FROM Categories c
LEFT JOIN Products p
ON c.category_id = p.category_id
GROUP BY c.category_name;
