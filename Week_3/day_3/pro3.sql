CREATE DATABASE RetailDB;

USE RetailDB;

CREATE TABLE stores (
    store_id INT PRIMARY KEY,
    store_name VARCHAR(100) NOT NULL
);

CREATE TABLE orders (
    order_id INT PRIMARY KEY,
    store_id INT,
    order_status INT,
    FOREIGN KEY (store_id) REFERENCES stores(store_id)
);

CREATE TABLE order_items (
    item_id INT PRIMARY KEY,
    order_id INT,
    quantity INT,
    list_price DECIMAL(10,2),
    discount DECIMAL(4,2),
    FOREIGN KEY (order_id) REFERENCES orders(order_id)
);

INSERT INTO stores VALUES
(1, 'Bangalore Central'),
(2, 'Hyderabad Mall'),
(3, 'Chennai Plaza');

INSERT INTO orders VALUES
(101, 1, 4),  
(102, 1, 1),  
(103, 2, 4),  
(104, 3, 4);  

INSERT INTO order_items VALUES
(1, 101, 2, 1000.00, 0.10),
(2, 101, 1, 500.00, 0.05),
(3, 103, 3, 700.00, 0.15),
(4, 104, 4, 400.00, 0.00),
(5, 102, 2, 800.00, 0.10); 

SELECT 
    s.store_name,
    SUM(oi.quantity * oi.list_price * (1 - oi.discount)) AS total_sales
FROM stores s
INNER JOIN orders o
    ON s.store_id = o.store_id
INNER JOIN order_items oi
    ON o.order_id = oi.order_id
WHERE o.order_status = 4
GROUP BY s.store_name
ORDER BY total_sales 
