CREATE DATABASE SalesDB;

USE SalesDB;

CREATE TABLE categories (
    category_id INT PRIMARY KEY,
    category_name VARCHAR(50) NOT NULL
);

CREATE TABLE brands (
    brand_id INT PRIMARY KEY,
    brand_name VARCHAR(50) NOT NULL
);

CREATE TABLE products (
    product_id INT PRIMARY KEY,
    product_name VARCHAR(100) NOT NULL,
    brand_id INT,
    category_id INT,
    model_year INT,
    list_price DECIMAL(10,2),
    FOREIGN KEY (brand_id) REFERENCES brands(brand_id),
    FOREIGN KEY (category_id) REFERENCES categories(category_id)
);

INSERT INTO categories VALUES
(1, 'Electronics'),
(2, 'Appliances'),
(3, 'Accessories');

INSERT INTO brands VALUES
(1, 'Samsung'),
(2, 'Apple'),
(3, 'Sony'),
(4, 'LG');

INSERT INTO products VALUES
(101, 'Galaxy S23', 1, 1, 2023, 800.00),
(102, 'iPhone 14', 2, 1, 2023, 999.00),
(103, 'Sony Headphones', 3, 3, 2022, 250.00),
(104, 'LG Refrigerator', 4, 2, 2021, 1200.00),
(105, 'Samsung TV', 1, 1, 2022, 600.00),
(106, 'Microwave Oven', 4, 2, 2023, 450.00);

SELECT 
    p.product_name,
    b.brand_name,
    c.category_name,
    p.model_year,
    p.list_price
FROM products p
INNER JOIN brands b
    ON p.brand_id = b.brand_id
INNER JOIN categories c
    ON p.category_id = c.category_id
WHERE p.list_price > 500
ORDER BY p.list_price ASC;
