IF NOT EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = 'rafilearningmvcdb')
CREATE DATABASE rafilearningmvcdb;

use rafilearningmvcdb;

--1. users
IF  NOT EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID('[dbo].[Users]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Users](
    UserId int IDENTITY(1,1) NOT NULL,
    Username varchar(100) NOT NULL,
    [Password] varchar(100) NOT NULL,
    DisplayName varchar(100) NOT NULL,
	ContactNo varchar(10) NOT NULL,
	IsActive bit,
    PRIMARY KEY (UserId)
) 

END


--2. Categories
IF EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID('[dbo].[Categories]') AND type in (N'U'))
Drop table Categories;

CREATE TABLE [dbo].[Categories](
    CategoriesId int IDENTITY(1,1) NOT NULL,
    CategoriesTitle varchar(100) NOT NULL,
    ParentCategory int NOT NULL,
    CategoriesImage varchar(255) NOT NULL,
	CategoriesSlug varchar(255) NOT NULL,
	IsActive bit,
	IsPopular bit,
    PRIMARY KEY (CategoriesId)
); 


--2. Products

IF  NOT EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID('[dbo].[Products]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Products](
    ProductId int identity(1,1) NOT NULL,
    ProductTitle varchar(255) NOT NULL,
	ProductImage1 varchar(255) NOT NULL,
	ProductImage2 varchar(255) NULL,
	ProductImage3 varchar(255) NULL,
	ProductImage4 varchar(255) NULL,
	ProductImage5 varchar(255) NULL,
	ProductSlug varchar(255) NOT NULL,
    ProductCategory int NOT NULL,
	Quantity int NOT NULL,
	Units int NOT NULL,
	Price decimal(10, 2) NOT NULL,
	Discount decimal(10, 2) NOT NULL,
	Tax decimal(10, 2) NOT NULL,
	IsActive bit,
    PRIMARY KEY (ProductId)
) 

END

IF  NOT EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID('[dbo].[Units]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Units](
    UitId int IDENTITY(1,1) NOT NULL,
    UnitTitle varchar(100) NOT NULL,
    PRIMARY KEY (UitId)
) 

END


IF  NOT EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID('[dbo].[Prices]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Prices](
    PriceId int IDENTITY(1,1) NOT NULL,
    SellingPrice decimal (10,2) NOT NULL,
	OfferPrice decimal (10, 2) NOT NULL,
    PRIMARY KEY (PriceId)
) 

END

IF  NOT EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID('[dbo].[Customers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Customers](
    CustomerId int IDENTITY(1,1) NOT NULL,
    CustomerName varchar (100) NOT NULL,
	CustomerPhone varchar (10) NOT NULL,
	CustomerEmail varchar (100) NOT NULL,
	CustomerPassword varchar (20) NOT NULL,
	IsActive bit,
    PRIMARY KEY (CustomerId)
) 

END

IF  NOT EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID('[dbo].[CustomersAddress]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CustomersAddress](
    AddressId int IDENTITY(1,1) NOT NULL,
    AddressLine1 varchar (100) NOT NULL,
	AddressLine2 varchar (100) NOT NULL,
	AddressLine3 varchar (100) NOT NULL,
	isDefault bit,
	PostalCode varchar (10) NOT NULL,
    PRIMARY KEY (AddressId)
) 

END

IF EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID('[dbo].[Cart]') AND type in (N'U'))
drop table cart;
CREATE TABLE [dbo].[Cart](
    CartId int IDENTITY(1,1) NOT NULL,
    RandomCustomer varchar (100) NOT NULL,
	ProductId int not null,
	IsLoggedIn bit,
	Quantity Int  NOT NULL,
	IsProcessed bit,
    PRIMARY KEY (CartId)
);

IF  NOT EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID('[dbo].[Orders]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Orders](
    OrderId int IDENTITY(1,1) NOT NULL,
    InvoiceNo varchar (100) NOT NULL,
	TotalPrice decimal (10, 2) NOT NULL,
	Discount decimal (10, 2) NOT NULL,
	Taxes decimal (10, 2) NOT NULL,
	NetTotal decimal (10, 2) NOT NULL,
	OrderStatus Int  NOT NULL,
	OrderPlaced datetime  NOT NULL,
    PRIMARY KEY (OrderId)
) 

END

IF  NOT EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID('[dbo].[OrderedProducts]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[OrderedProducts](
    OrderedProductId int IDENTITY(1,1) NOT NULL,
	OrderId Int  NOT NULL,
	ProductId Int  NOT NULL,
	ProductName  varchar (100) NOT NULL,
	PriceId Int  NOT NULL,
	UnitId Int  NOT NULL,
	UnitName varchar (100) NOT NULL,
	SellingPrice decimal (10, 2) NOT NULL,
	OfferPrice decimal (10, 2) NOT NULL,
	Quantity Int  NOT NULL,
	TotalAmount decimal (10, 2) NOT NULL,
	Tax decimal (10, 2) NOT NULL,
	NetTotal decimal (10, 2) NOT NULL,
    PRIMARY KEY (OrderedProductId)
) 

END

IF  NOT EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID('[dbo].[Stock]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Stock](
    StockId int IDENTITY(1,1) NOT NULL,
	ProductId Int  NOT NULL,
	Quantity Int  NOT NULL,
	
    PRIMARY KEY (StockId)
) 

END

IF  NOT EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID('[dbo].[Payments]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Payments](
    PaymentId int IDENTITY(1,1) NOT NULL,
	PaymentType Int  NOT NULL,
	PaymentDatetime datetime  NOT NULL,
	PaymentStatus Int  NOT NULL,
	OrderId Int  NOT NULL,
	
    PRIMARY KEY (PaymentId)
) 

END

IF  NOT EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID('[dbo].[Ledger]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Ledger](
    LedgerId int IDENTITY(1,1) NOT NULL,
	ProductId Int  NOT NULL,
	purchaseprice decimal (10, 2) NOT NULL,
	purchaseddate datetime  NOT NULL,
	quantity Int  NOT NULL,

    PRIMARY KEY (LedgerId)
) 

END


IF  NOT EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID('[dbo].[AdminMenus]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AdminMenus](
    AdminMenuId int IDENTITY(1,1) NOT NULL,
	AdminMenuTitle1 varchar(100)  NOT NULL,
	AdminMenuTitle2 varchar(100) NULL,
	AdminMenuFavIcon varchar(50) NULL,
	AdminMenuParent INT NOT NULL,
	AdminMenuController varchar(100) NULL,
	AdminMenuAction varchar(100) NULL,
	AdminMenuStatus bit
    PRIMARY KEY (AdminMenuId)
) 
END

--IF  NOT EXISTS (SELECT * FROM sys.objects 
--WHERE object_id = OBJECT_ID('[dbo].[ProductListing]') AND type in (N'U'))
--BEGIN
--CREATE TABLE [dbo].[ProductListing](
--    ProductListingId int IDENTITY(1,1) NOT NULL,
--	ProductId Int  NOT NULL,
--	ListingType Int NOT NULL,
--	PRIMARY KEY (ProductListingId)
--) 

--END