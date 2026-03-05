CREATE DATABASE MaintenanceDb;

USE MaintenanceDb;

CREATE TABLE categories (
    category_id INT PRIMARY KEY,
    category_name VARCHAR(50)
);

INSERT INTO categories VALUES
(1,'Mountain Bikes'),
(2,'Road Bikes'),
(3,'Electric Bikes'),
(4,'Kids Bikes');

CREATE TABLE products (
    product_id INT PRIMARY KEY,
    product_name VARCHAR(100),
    model_year INT,
    list_price DECIMAL(10,2),
    category_id INT,
    discontinued BIT DEFAULT 0,
    FOREIGN KEY (category_id) REFERENCES categories(category_id)
);

INSERT INTO products VALUES
(101,'Trek 820',2017,800,1,0),
(102,'Giant ATX',2018,750,1,0),
(103,'Specialized Rockhopper',2019,950,1,0),
(104,'Trek Domane',2018,1200,2,0),
(105,'Giant Contend',2019,1100,2,0),
(106,'Cannondale CAAD',2020,1400,2,0),
(107,'Rad Power Bike',2020,1800,3,0),
(108,'Specialized Turbo',2021,3200,3,0),
(109,'Giant Quick E+',2020,2500,3,0),
(110,'Kids Fun Bike',2019,200,4,0),
(111,'Junior Speed Bike',2020,250,4,0);

CREATE TABLE stores (
    store_id INT PRIMARY KEY,
    store_name VARCHAR(100)
);

INSERT INTO stores VALUES
(1,'Downtown Store'),
(2,'Uptown Store');

CREATE TABLE stocks (
    stock_id INT PRIMARY KEY,
    store_id INT,
    product_id INT,
    quantity INT,
    FOREIGN KEY (store_id) REFERENCES stores(store_id),
    FOREIGN KEY (product_id) REFERENCES products(product_id)
);

INSERT INTO stocks VALUES
(1,1,101,0),
(2,1,102,10),
(3,1,103,5),
(4,2,101,0),
(5,2,104,20);


CREATE TABLE customers (
    customer_id INT PRIMARY KEY,
    first_name VARCHAR(50),
    last_name VARCHAR(50)
);

INSERT INTO customers VALUES
(1,'John','Smith'),
(2,'Emma','Johnson'),
(3,'Michael','Brown'),
(4,'Sophia','Davis'),
(5,'Daniel','Wilson');


CREATE TABLE [orders] (
    order_id INT PRIMARY KEY,
    customer_id INT,
    store_id INT,
    order_date DATE,
    required_date DATE,
    shipped_date DATE,
    order_status INT,
    total_amount DECIMAL(10,2),
    FOREIGN KEY (customer_id) REFERENCES customers(customer_id),
    FOREIGN KEY (store_id) REFERENCES stores(store_id)
);

INSERT INTO [orders] VALUES
(201,1,1,'2023-01-10','2023-01-15','2023-01-14',2,12000),
(202,1,1,'2023-02-15','2023-02-20','2023-02-22',2,3000),
(203,2,2,'2022-01-05','2022-01-10','2022-01-09',3,3500), -- Rejected/old
(204,3,1,'2023-01-20','2023-01-25','2023-01-27',2,12000),
(205,4,2,'2023-02-22','2023-02-27','2023-02-28',2,6000);


CREATE TABLE order_items (
    order_item_id INT PRIMARY KEY,
    order_id INT,
    product_id INT,
    quantity INT,
    list_price DECIMAL(10,2),
    discount DECIMAL(10,2),
    FOREIGN KEY (order_id) REFERENCES [orders](order_id),
    FOREIGN KEY (product_id) REFERENCES products(product_id)
);

INSERT INTO order_items VALUES
(301,201,101,3,800,50),
(302,201,102,5,750,0),
(303,202,101,2,800,0),
(304,202,104,1,1200,100),
(305,203,103,1,950,0),
(306,204,106,2,1400,0),
(307,205,110,5,200,0);

CREATE TABLE archived_orders (
    order_id INT PRIMARY KEY,
    customer_id INT,
    store_id INT,
    order_date DATE,
    required_date DATE,
    shipped_date DATE,
    order_status INT,
    total_amount DECIMAL(10,2)
);

SELECT 
    CONCAT(product_name, ' (', model_year, ')') AS product_info,
    list_price,
    list_price - (
        SELECT AVG(list_price)
        FROM products p2
        WHERE p2.category_id = p1.category_id
    ) AS price_difference
FROM products p1
WHERE list_price > (
    SELECT AVG(list_price)
    FROM products p2
    WHERE p2.category_id = p1.category_id
);

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
    SELECT customer_id, SUM(total_amount) AS total_order_value
    FROM [orders]
    GROUP BY customer_id
) t ON c.customer_id = t.customer_id
UNION
SELECT
    CONCAT(first_name,' ',last_name) AS full_name,
    NULL AS total_order_value,
    'No Orders' AS customer_type
FROM customers
WHERE customer_id NOT IN (SELECT customer_id FROM [orders]);

SELECT 
    s.store_name,
    p.product_name,
    t.total_quantity_sold,
    (t.total_quantity_sold * t.list_price - t.total_discount) AS total_revenue
FROM (
    SELECT o.store_id, oi.product_id,
           SUM(oi.quantity) AS total_quantity_sold,
           SUM(oi.list_price) AS list_price,
           SUM(oi.discount) AS total_discount
    FROM [orders] o
    JOIN order_items oi ON o.order_id = oi.order_id
    GROUP BY o.store_id, oi.product_id
) t
JOIN stores s ON t.store_id = s.store_id
JOIN products p ON t.product_id = p.product_id
LEFT JOIN stocks st
    ON st.store_id = t.store_id
    AND st.product_id = t.product_id
    AND st.quantity > 0
WHERE st.product_id IS NULL;  

INSERT INTO archived_orders (order_id, customer_id, store_id, order_date, required_date, shipped_date, order_status, total_amount)
SELECT order_id, customer_id, store_id, order_date, required_date, shipped_date, order_status, total_amount
FROM [orders]
WHERE order_status = 3
  AND order_date < DATEADD(YEAR, -1, GETDATE());


DELETE oi
FROM order_items oi
JOIN [orders] o ON oi.order_id = o.order_id
WHERE o.order_status = 3
  AND o.order_date < DATEADD(YEAR, -1, GETDATE());

DELETE FROM [orders]
WHERE order_status = 3
  AND order_date < DATEADD(YEAR, -1, GETDATE());DELETE FROM [orders]
WHERE order_status = 3
  AND order_date < DATEADD(YEAR, -1, GETDATE());


SELECT c.customer_id, CONCAT(c.first_name,' ',c.last_name) AS full_name
FROM customers c
WHERE NOT EXISTS (
    SELECT 1
    FROM [orders] o
    WHERE o.customer_id = c.customer_id
      AND o.order_status <> 2
);

SELECT o.order_id,
       CONCAT(c.first_name,' ',c.last_name) AS customer_name,
       DATEDIFF(DAY, o.order_date, o.shipped_date) AS processing_delay,
       CASE
           WHEN o.shipped_date > o.required_date THEN 'Delayed'
           ELSE 'On Time'
       END AS delivery_status
FROM [orders] o
JOIN customers c ON o.customer_id = c.customer_id;
