CREATE DATABASE EcommDb;
USE EcommDb;

CREATE TABLE Categories (
    category_id INT PRIMARY KEY,
    category_name VARCHAR(50) NOT NULL
);

CREATE TABLE Brands (
    brand_id INT PRIMARY KEY,
    brand_name VARCHAR(50) NOT NULL
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

CREATE TABLE Staffs (
    staff_id INT PRIMARY KEY,
    first_name VARCHAR(50),
    last_name VARCHAR(50),
    email VARCHAR(100),
    store_id INT,

    FOREIGN KEY (store_id) REFERENCES Stores(store_id)
);

CREATE TABLE Orders (
    order_id INT PRIMARY KEY,
    customer_id INT,
    store_id INT,
    staff_id INT,
    order_date DATE,

    FOREIGN KEY (customer_id) REFERENCES Customers(customer_id),
    FOREIGN KEY (store_id) REFERENCES Stores(store_id),
    FOREIGN KEY (staff_id) REFERENCES Staffs(staff_id)
);

CREATE TABLE Order_Items (
    order_item_id INT PRIMARY KEY,
    order_id INT,
    product_id INT,
    quantity INT,
    list_price DECIMAL(10,2),
    discount DECIMAL(5,2),

    FOREIGN KEY (order_id) REFERENCES Orders(order_id),
    FOREIGN KEY (product_id) REFERENCES Products(product_id)
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
(1,'AutoWorld','Bangalore'),
(2,'Speed Motors','Hyderabad'),
(3,'DriveHub','Mumbai'),
(4,'CarZone','Delhi'),
(5,'Urban Cars','Chennai');

INSERT INTO Staffs VALUES
(1,'Amit','Verma','amit@store.com',1),
(2,'Rohit','Mehta','rohit@store.com',2),
(3,'Neha','Shah','neha@store.com',3),
(4,'Karan','Kapoor','karan@store.com',4),
(5,'Anita','Das','anita@store.com',5);

INSERT INTO Orders VALUES
(1,1,1,1,'2024-01-10'),
(2,2,2,2,'2024-01-11'),
(3,3,1,1,'2024-01-12'),
(4,4,3,3,'2024-01-13'),
(5,5,4,4,'2024-01-14');

INSERT INTO Order_Items VALUES
(1,1,1,1,3500000,0),
(2,2,2,1,2000000,0.05),
(3,3,3,1,8000000,0),
(4,4,4,1,4500000,0.10),
(5,5,5,2,900000,0);

CREATE VIEW vw_ProductSummary
AS
SELECT 
p.product_name,
b.brand_name,
c.category_name,
p.model_year,
p.list_price
FROM Products p
JOIN Brands b ON p.brand_id = b.brand_id
JOIN Categories c ON p.category_id = c.category_id;

CREATE VIEW vw_OrderSummary
AS
SELECT
o.order_id,
c.first_name + ' ' + c.last_name AS customer_name,
s.store_name,
st.first_name + ' ' + st.last_name AS staff_name,
o.order_date
FROM Orders o
JOIN Customers c ON o.customer_id = c.customer_id
JOIN Stores s ON o.store_id = s.store_id
JOIN Staffs st ON o.staff_id = st.staff_id;

CREATE INDEX idx_products_brand
ON Products(brand_id);

CREATE INDEX idx_products_category
ON Products(category_id);

CREATE INDEX idx_orders_customer
ON Orders(customer_id);

CREATE INDEX idx_orders_store
ON Orders(store_id);

CREATE INDEX idx_orders_staff
ON Orders(staff_id);
