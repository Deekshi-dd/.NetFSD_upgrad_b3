CREATE DATABASE AutoDb;

USE AutoDb;

CREATE TABLE categories (
    category_id INT PRIMARY KEY,
    category_name VARCHAR(50)
);

CREATE TABLE products (
    product_id INT PRIMARY KEY,
    product_name VARCHAR(100),
    model_year INT,
    list_price DECIMAL(10,2),
    category_id INT,
    FOREIGN KEY (category_id) REFERENCES categories(category_id)
);

INSERT INTO categories VALUES
(1,'Mountain Bikes'),
(2,'Road Bikes'),
(3,'Electric Bikes'),
(4,'Kids Bikes');

INSERT INTO products VALUES
(101,'Trek 820',2017,800,1),
(102,'Giant ATX',2018,750,1),
(103,'Specialized Rockhopper',2019,950,1),
(104,'Trek Domane',2018,1200,2),
(105,'Giant Contend',2019,1100,2),
(106,'Cannondale CAAD',2020,1400,2),
(107,'Rad Power Bike',2020,1800,3);

SELECT * FROM categories;

SELECT * FROM products;

SELECT 
CONCAT(product_name,' (',model_year,')') AS product_info,
list_price,
list_price - (
    SELECT AVG(list_price)
    FROM products p2
    WHERE p2.category_id = p1.category_id
) AS price_difference
FROM products p1
WHERE list_price >
(
SELECT AVG(list_price)
FROM products p2
WHERE p2.category_id = p1.category_id
);
