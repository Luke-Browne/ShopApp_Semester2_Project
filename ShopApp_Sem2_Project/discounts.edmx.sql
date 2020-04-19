
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/19/2020 05:13:09
-- Generated from EDMX file: C:\Users\Owner\Desktop\College Work\OOD\ShopApp_Sem2_Project\ShopApp_Sem2_Project\discounts.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [users];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'discounts'
CREATE TABLE [dbo].[discounts] (
    [codeID] int IDENTITY(1,1) NOT NULL,
    [Code] nvarchar(max)  NOT NULL,
    [Value] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [codeID] in table 'discounts'
ALTER TABLE [dbo].[discounts]
ADD CONSTRAINT [PK_discounts]
    PRIMARY KEY CLUSTERED ([codeID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------