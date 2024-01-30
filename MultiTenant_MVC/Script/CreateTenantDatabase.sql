-- Crea il database "Tenant"
CREATE DATABASE Tenant;
GO

-- Utilizza il database "Tenant"
USE Tenant;
GO

-- Crea la tabella "Orders"
CREATE TABLE Orders
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(255),
    Type NVARCHAR(255),
    Description NVARCHAR(MAX),
    Price NVARCHAR(255)
);
GO