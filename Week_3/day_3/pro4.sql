CREATE DATABASE InventoryDB;

USE InventoryDB;

CREATE TABLE products (
    product_id INT PRIMARY KEY,
    product_name VARCHAR(100) NOT NULL
);

CREATE TABLE stores (
    store_id INT PRIMARY KEY,
    store_name VARCHAR(100) NOT NULL
);

CREATE TABLE stocks (
    store_id INT,
    product_id INT,
    quantity INT,
    PRIMARY KEY (store_id, product_id),
    FOREIGN KEY (store_id) REFERENCES stores(store_id),
    FOREIGN KEY (product_id) REFERENCES products(product_id)
);

CREATE TABLE order_items (
    item_id INT PRIMARY KEY,
    order_id INT,
    product_id INT,
    store_id INT,
    quantity INT
);

INSERT INTO products VALUES
(1, 'Galaxy S23'),
(2, 'iPhone 14'),
(3, 'Sony Headphones');

INSERT INTO stores VALUES
(1, 'Bangalore Store'),
(2, 'Hyderabad Store');

INSERT INTO stocks VALUES
(1, 1, 50),
(1, 2, 40),
(2, 1, 30),
(2, 3, 20);

INSERT INTO order_items VALUES
(1, 101, 1, 1, 10),
(2, 102, 1, 1, 2),
(3, 103, 2, 1, 8),
(4, 104, 1, 2, 5);

SELECT 
    p.product_name,
    s.store_name,
    st.quantity AS available_stock,
    ISNULL(SUM(oi.quantity), 0) AS total_quantity_sold
FROM stocks st
INNER JOIN products p
    ON st.product_id = p.product_id
INNER JOIN stores s
    ON st.store_id = s.store_id
LEFT JOIN order_items oi
    ON st.product_id = oi.product_id
    AND st.store_id = oi.store_id
GROUP BY 
    p.product_name,
    s.store_name,
    st.quantity
ORDER BY 
    p.product_name;
