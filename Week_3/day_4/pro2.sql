CREATE DATABASE RecoveryDb;

USE RecoveryDb;

CREATE TABLE customers (
    customer_id INT PRIMARY KEY,
    first_name VARCHAR(50),
    last_name VARCHAR(50)
);

CREATE TABLE [orders] (
    order_id INT PRIMARY KEY,
    customer_id INT,
    order_date DATE,
    total_amount DECIMAL(10,2),
    FOREIGN KEY (customer_id) REFERENCES customers(customer_id)
);

INSERT INTO customers VALUES
(1,'John','Smith'),
(2,'Emma','Johnson'),
(3,'Michael','Brown'),
(4,'Sophia','Davis'),
(5,'Daniel','Wilson');


INSERT INTO [orders] VALUES
(101,1,'2023-01-10',4000),
(102,1,'2023-02-15',3000),
(103,1,'2023-03-10',5000),
(104,2,'2023-02-05',2000),
(105,2,'2023-03-12',1500),
(106,3,'2023-01-20',12000),
(107,4,'2023-02-22',6000);

SELECT 
    CONCAT(c.first_name,' ',c.last_name) AS full_name,
    t.total_order_value,
    CASE 
        WHEN t.total_order_value > 10000 THEN 'Premium'
        WHEN t.total_order_value BETWEEN 5000 AND 10000 THEN 'Regular'
        ELSE 'Basic'
    END AS customer_type
FROM customers c
JOIN
(
 --Nested query to calculate total order value per customer
    SELECT customer_id, SUM(total_amount) AS total_order_value
    FROM [orders]
    GROUP BY customer_id
) t ON c.customer_id = t.customer_id
UNION
-- Include customers with no orders
SELECT
    CONCAT(first_name,' ',last_name) AS full_name,
    NULL AS total_order_value,
    'No Orders' AS customer_type
FROM customers
WHERE customer_id NOT IN (SELECT customer_id FROM [orders])
ORDER BY full_name;
