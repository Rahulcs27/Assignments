-- Step 1: Create the Database
CREATE DATABASE InsuranceDB;
GO
USE InsuranceDB;

CREATE TABLE Users (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    UserName NVARCHAR(50) UNIQUE NOT NULL,
    Password NVARCHAR(100) NOT NULL
);

CREATE TABLE Policies (
    PolicyId INT IDENTITY(1,1) PRIMARY KEY,
    PolicyHolderName NVARCHAR(100) NOT NULL,
    PolicyType NVARCHAR(20) NOT NULL CHECK (PolicyType IN ('Life', 'Health', 'Vehicle', 'Property')),
    StartDate DATETIME NOT NULL DEFAULT GETDATE(),
    EndDate DATETIME NOT NULL
);



INSERT INTO Users (UserName, Password) VALUES 
('admin', 'admin123'),
('user1', 'password1');

INSERT INTO Policies (PolicyHolderName, PolicyType, StartDate, EndDate)
VALUES 
('Rahul Suthar', 'Life', GETDATE(), '2027-12-31'),
('Vaishnavi Bhambure', 'Health', GETDATE(), '2026-06-15'),
('Sakshi Bahirat', 'Vehicle', GETDATE(), '2025-09-10');

SELECT * FROM Users;
SELECT * FROM Policies;

