CREATE DATABASE PerformanceDb;
USE PerformanceDb;

CREATE TABLE IF NOT EXISTS stores (
    store_id INT PRIMARY KEY,
    store_name VARCHAR(100)
);

CREATE TABLE IF NOT EXISTS products (
    product_id INT PRIMARY KEY,
    product_name VARCHAR(100),
    list_price DECIMAL(10,2),
    discount DECIMAL(4,2)
);

CREATE TABLE IF NOT EXISTS stocks (
    store_id INT,
    product_id INT,
    quantity INT,
    PRIMARY KEY (store_id, product_id),
    FOREIGN KEY (store_id) REFERENCES stores(store_id),
    FOREIGN KEY (product_id) REFERENCES products(product_id)
);

CREATE TABLE IF NOT EXISTS orders (
    order_id INT PRIMARY KEY,
    store_id INT,
    order_status INT,
    FOREIGN KEY (store_id) REFERENCES stores(store_id)
);

CREATE TABLE IF NOT EXISTS order_items (
    item_id INT PRIMARY KEY,
    order_id INT,
    product_id INT,
    quantity INT,
    FOREIGN KEY (order_id) REFERENCES orders(order_id)
);

----------------------------------------------
-- 2️⃣ Insert Sample Data
----------------------------------------------
INSERT INTO stores VALUES (1, 'Bangalore Store'), (2, 'Hyderabad Store');

INSERT INTO products VALUES
(1, 'Galaxy S23', 800, 0.10),
(2, 'iPhone 14', 1000, 0.05),
(3, 'Sony Headphones', 200, 0.00);

INSERT INTO stocks VALUES
(1, 1, 10),
(1, 2, 0),
(1, 3, 5),
(2, 1, 0),
(2, 2, 20),
(2, 3, 0);

INSERT INTO orders VALUES
(101, 1, 4),  -- Completed
(102, 2, 4);

INSERT INTO order_items VALUES
(1, 101, 1, 8),
(2, 101, 2, 5),
(3, 102, 1, 7),
(4, 102, 3, 3);

----------------------------------------------
-- 3️⃣ Identify Sold Products in Each Store (Subquery)
----------------------------------------------
WITH SoldProducts AS (
    SELECT o.store_id, oi.product_id, SUM(oi.quantity) AS total_sold
    FROM orders o
    INNER JOIN order_items oi ON o.order_id = oi.order_id
    WHERE o.order_status = 4
    GROUP BY o.store_id, oi.product_id
),
CurrentStock AS (
    SELECT store_id, product_id, quantity
    FROM stocks
)
----------------------------------------------
-- 4️⃣ Compare Sold vs Stock using INTERSECT/EXCEPT
----------------------------------------------
SELECT s.store_name, p.product_name, sp.total_sold,
       (sp.total_sold * pr.list_price * (1 - pr.discount)) AS total_revenue
FROM SoldProducts sp
INNER JOIN stores s ON sp.store_id = s.store_id
INNER JOIN products pr ON sp.product_id = pr.product_id
WHERE (sp.store_id, sp.product_id) IN 
(
    -- Sold products that currently have zero stock
    SELECT store_id, product_id
    FROM SoldProducts
    EXCEPT
    SELECT store_id, product_id
    FROM CurrentStock
    WHERE quantity > 0
)
ORDER BY s.store_name, p.product_name;

----------------------------------------------
-- 5️⃣ Update Stock Quantity to 0 for discontinued products
----------------------------------------------
UPDATE stocks
SET quantity = 0
WHERE (store_id, product_id) IN 
(
    SELECT store_id, product_id
    FROM SoldProducts
    EXCEPT
    SELECT store_id, product_id
    FROM CurrentStock
    WHERE quantity > 0
);
