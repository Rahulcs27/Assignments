CREATE DATABASE BookStoreDB;
USE BookStoreDB;
-- Table Creation--

CREATE TABLE Authors (
    AuthorID INT PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Country VARCHAR(100) NOT NULL
);

CREATE TABLE Books (
    BookID INT PRIMARY KEY,
    Title VARCHAR(255) NOT NULL UNIQUE,
    AuthorID INT,
    Price DECIMAL(10,2) NOT NULL,
    PublishedYear INT NOT NULL,
    FOREIGN KEY (AuthorID) REFERENCES Authors(AuthorID)
);

CREATE TABLE Customers (
    CustomerID INT PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Email VARCHAR(100) UNIQUE NOT NULL,
    PhoneNumber VARCHAR(15) UNIQUE NOT NULL
);

CREATE TABLE Orders (
    OrderID INT PRIMARY KEY,
    CustomerID INT,
    OrderDate DATE NOT NULL,
    TotalAmount DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID)
);

CREATE TABLE OrderItems (
    OrderItemID INT PRIMARY KEY,
    OrderID INT,
    BookID INT,
    Quantity INT NOT NULL CHECK (Quantity > 0),
    SubTotal DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
    FOREIGN KEY (BookID) REFERENCES Books(BookID)
);

--Putting Data--

INSERT INTO Authors VALUES
(1, 'J.K. Rowling', 'UK'),
(2, 'George R.R. Martin', 'USA'),
(3, 'Dan Brown', 'USA'),
(4, 'Paulo Coelho', 'Brazil'),
(5, 'E.L. James', 'UK');

INSERT INTO Books VALUES
(1, 'SQL Mastery', 3, 2500.00, 2020),
(2, 'Game of Thrones', 2, 1500.00, 1996),
(3, 'Harry Potter', 1, 1800.00, 1997),
(4, 'The Alchemist', 4, 1200.00, 1988),
(5, 'Fifty Shades of Grey', 5, 2200.00, 2011);

INSERT INTO Customers VALUES
(1, 'Rahul Suthar', 'rahulsuthar@email.com', '9876543210'),
(2, 'Om', 'Om@email.com', '9123456789'),
(3, 'Sakshi', 'sakshi@email.com', '9234567891'),
(4, 'vaishnavi', 'vaishnavi@email.com', '9345678901'),
(5, 'No Orders Customer', 'noorders@email.com', '9456789012');

INSERT INTO Orders VALUES
(1, 1, '2024-03-01', 2500.00),
(2, 2, '2024-03-02', 1500.00),
(3, 3, '2024-03-03', 1200.00),
(4, 4, '2024-03-04', 1800.00);

INSERT INTO OrderItems VALUES
(1, 1, 1, 1, 2500.00),
(2, 2, 2, 1, 1500.00),
(3, 3, 4, 1, 1200.00),
(4, 4, 3, 1, 1800.00);

--Update Book Price
UPDATE Books 
SET Price = Price * 1.10
WHERE Title = 'SQL Mastery';

--3. Delete a customer who has not placed any orders.
DELETE FROM Customers
WHERE CustomerID NOT IN (SELECT DISTINCT CustomerID FROM Orders);

--Operators
-- 1. Retrieve all books with a price greater than 2000. 
Select * from Books where Price>2000;

--2. Find the total number of books available. Select COUNT(*) as TotalNoBooks from Books;--3. Find books published between 2015 and 2023. Select * from Books where PublishedYear Between 2015 And 2023--4. Find all customers who have placed at least one order. INSERT INTO Customers (CustomerID, Name, Email, PhoneNumber) 
VALUES (5, 'saktish', 'saktish@email.com', '9567890123');

select * from Customers

SELECT DISTINCT Customers.*
FROM Customers
INNER JOIN Orders ON Customers.CustomerID = Orders.CustomerID;

--5. Retrieve books where the title contains the word "SQL". select * from Books where Title like '%SQL%'
--6. Find the most expensive book in the store. select Title from Books where Price = (select MAX(Price) from Books)

--7. Retrieve customers whose name starts with "A" or "J". select * from Customers where Name Like 'A%' or Name Like 'J%'--8. Calculate the total revenue from all orders. SELECT SUM(TotalAmount) AS TotalRevenue FROM Orders;

--Joins
--1. Retrieve all book titles along with their respective author names. 
select Books.Title , Authors.Name as Authors
from Books
Join Authors
on Books.AuthorID = Authors.AuthorID

--2. List all customers and their orders (if any). Select Customers.Name , Orders.OrderID , Orders.OrderDate , Orders.TotalAmount
FROM Customers
left join Orders
on Customers.CustomerID = Orders.CustomerID


-- 3. Find all books that have never been ordered. select Books.Title
from Books
left join OrderItems 
on Books.BookID = OrderItems.BookID
where orderItems.OrderID is Null

--4. Retrieve the total number of orders placed by each customer.
select Customers.Name , Count(Orders.OrderID) as TotalOrders
from Customers
left join Orders
on Customers.CustomerID = Orders.CustomerID
group by Customers.Name

--5. Find the books ordered along with the quantity for each order. select OrderItems.OrderID , Books.Title , OrderItems.Quantity
from OrderItems
Join Books
on Books.BookID = OrderItems.BookID

--6. Display all customers, even those who haven’t placed any orders. Select Customers.Name , Orders.OrderID , Orders.TotalAmountfrom CustomersLeft join Orderson Customers.CustomerID = Orders.CustomerID--7. Find authors who have not written any books select Authors.Namefrom Authorsleft join Bookson Books.AuthorID = Authors.AuthorIDwhere Books.BookID is Null--Assignment Day10--1. Find the customer(s) who placed the first order (earliest OrderDate). select * from customersselect * from Ordersselect c.*, o.OrderDate from customers cjoin Orders oon c.CustomerID = o.CustomerIDwhere OrderDate = (select min(OrderDate) from Orders)SELECT Name as customername ,(SELECT MIN(OrderDate) FROM Orders)as OrderDate
FROM Customers 
WHERE CustomerID = (
    SELECT CustomerID 
    FROM Orders 
    WHERE OrderDate = (SELECT MIN(OrderDate) FROM Orders)
);

select name 
from Customers
where CustomerID in
(select customerid,min(orderdate)
from Orders)
  SELECT c.Name AS CustomerName, o.OrderDate
FROM Customers c, Orders o
WHERE c.CustomerID = o.CustomerID
AND o.OrderDate = (SELECT MIN(OrderDate) FROM Orders);

select * from Customers ;
select * from orders;

--3. Find customers who have not placed any orders.
SELECT * 
FROM Customers 
WHERE CustomerID NOT IN (SELECT DISTINCT CustomerID FROM Orders);

--4. Retrieve all books cheaper than the most expensive book written bySELECT * FROM Books WHERE Price < (SELECT MAX(Price) FROM Books)--5. List all customers whose total spending is greater than the average
SELECT Customers.CustomerId, Name, TotalAmountFROM Customers JOIN OrdersON Customers.CustomerId = Orders.CustomerIdWHERE Customers.CustomerIdIN (SELECT CustomerId FROM Orders WHERE TotalAmount >(SELECT AVG(TotalAmount) FROM Orders))--Stored Procedures--1. Write a stored procedure that accepts a CustomerID and returns all orders
--placed by that customer 
CREATE PROC OrderDetailByCustomerId 
@Id int
AS
BEGIN
	SELECT * FROM Orders WHERE CustomerId = @Id
END

EXEC OrderDetailByCustomerId 3

--2. Create a procedure that accepts MinPrice and MaxPrice as parameters
--and returns all books within that range 
CREATE PROC GetBookByMinAndMaxPrice
@MinPrice int,
@MaxPrice int
AS
BEGIN 
	SELECT * FROM Books WHERE Price BETWEEN @MinPrice AND @MaxPrice
END

EXEC GetBookByMinAndMaxPrice 100, 250

--Views
--1.Create a view named AvailableBooks that shows only books that are in
--stock, including BookID, Title, AuthorID, Price, and PublishedYear

CREATE VIEW BookPublishedBefore2015
AS
	SELECT * FROM Books WHERE PublishedYear > 2015

SELECT * FROM BookPublishedBefore2015