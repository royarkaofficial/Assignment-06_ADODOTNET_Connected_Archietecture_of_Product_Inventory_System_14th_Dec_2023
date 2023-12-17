-- Create the ProductInventoryDB database
CREATE DATABASE ProductInventoryDB;
USE ProductInventoryDB

-- Create the Products table
CREATE TABLE Products (
    ProductId INT PRIMARY KEY,
    ProductName NVARCHAR(255),
    Price FLOAT,
    Quantity INT,
    MfDate DATE,
    ExpDate DATE
);

-- Insert 5 rows into the Products table
INSERT INTO Products (ProductId, ProductName, Price, Quantity, MfDate, ExpDate)
VALUES 
    (1, 'Product A', 10.99, 50, '2023-01-01', '2023-12-31'),
    (2, 'Product B', 20.50, 30, '2023-02-01', '2023-11-30'),
    (3, 'Product C', 15.75, 40, '2023-03-01', '2023-10-31'),
    (4, 'Product D', 8.99, 20, '2023-04-01', '2023-09-30'),
    (5, 'Product E', 25.00, 25, '2023-05-01', '2023-08-31');

select * from Products