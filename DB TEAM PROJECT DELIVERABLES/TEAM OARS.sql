-- fn: TEAM4OARS.sql 

-- SQL COMMENTED SQL COMMANDS
-- =============================================
-- Author:		<Stephen Carver>
-- Create date: <04/4/2016>
-- Description:	<Create Tables>
-- =============================================

--DROP OLD TABLES


DROP TABLE Complaint;
DROP TABLE Owns;
DROP TABLE Rental_Invoice;
DROP TABLE Tenant_Family;
DROP TABLE Tenant_Auto;
DROP TABLE Testimonial;

DROP TABLE Rental;
DROP TABLE Staff;
DROP TABLE Apartment;
DROP TABLE Tenant;

--CREATE TABLES

CREATE TABLE Staff (
    Staff_number VARCHAR(10)  NOT NULL,
    First_Name   VARCHAR (50) NULL,
    Last_Name    VARCHAR (50) NULL,
    Position     VARCHAR (50) NULL,
    Gender       VARCHAR (1)   NULL,
    DOB  DATETIME NULL,
    Salary       REAL NULL,
    Username     VARCHAR (20)   NULL,
    Password     VARCHAR (20)   NULL,
    PRIMARY KEY (Staff_number)
);

CREATE TABLE Tenant (
    Tenant_SS       INT       NOT NULL,
    Tenant_Name     VARCHAR (30) NULL,
    Tenant_DOB      DATETIME  NULL,
    Tenant_Marital  VARCHAR (1) NULL,
    Work_Phone      BIGINT    NULL,
    Home_Phone      BIGINT       NULL,
    Employer	VARCHAR (30) NULL,
    Tenant_Gender   VARCHAR (1) NULL,
    Tenant_Username VARCHAR (30) NULL,
    Tenant_Password VARCHAR (30) NULL,
    PRIMARY KEY (Tenant_SS)
);

CREATE TABLE Apartment (
    Apt_no  INT       NOT NULL,
    Apt_Type	INT NULL,
    Apt_Status      VARCHAR (1) NULL,
    Apt_Utility     VARCHAR (1) NULL,
    Apt_Deposit_Amt MONEY      NULL,
    Apt_Rent_Amt    MONEY     NULL,
    PRIMARY KEY  (Apt_no ),
	CONSTRAINT chk_Type CHECK(Apt_Type IN (0,1,2,3))
);

CREATE TABLE Tenant_Auto (
    License_No VARCHAR (7)  NOT NULL,
    Auto_Make  VARCHAR (30) NULL,
    Auto_Model VARCHAR (30) NULL,
    Auto_Year  INT       NULL,
    Auto_Color VARCHAR (30) NULL,
    Tenant_SS  INT       NULL,
    PRIMARY KEY (License_No),
    FOREIGN KEY (Tenant_SS) REFERENCES Tenant (Tenant_SS)
);


CREATE TABLE Tenant_Family (
    Family_SS     INT       NOT NULL,
    Family_Name   VARCHAR (30) NULL,
    Spouse	VARCHAR (1) NULL,
    Child VARCHAR (1) NULL,
    Divorced      VARCHAR (1) NULL,
    Single	VARCHAR (1) NULL,
    Family_Gender VARCHAR (1) NULL,
    Family_DOB    DATETIME  NULL,
    Tenant_SS     INT       NULL,
    PRIMARY KEY (Family_SS),
    FOREIGN KEY (Tenant_SS) REFERENCES Tenant (Tenant_SS)
);


CREATE TABLE Rental (
    Rental_No     INT IDENTITY(100101,1)      NOT NULL,
    Rental_Date   DATETIME  NULL,
    Rental_Status VARCHAR (1) NULL,
    Cancel_Date   DATETIME  NULL,
    Lease_Type    VARCHAR (10) NULL,
    Lease_Start   DATETIME  NULL,
    Lease_End     DATETIME  NULL,
    Renewal_Date  DATETIME  NULL,
	Staff_number VARCHAR(10),
	Apt_no       INT,
    PRIMARY KEY (Rental_No),
	FOREIGN KEY (Staff_number) REFERENCES Staff (Staff_number),
	FOREIGN KEY (Apt_no) REFERENCES Apartment (Apt_no)
);


CREATE TABLE Owns (
    Tenant_SS INT ,
    Rental_No INT ,
	Apt_No INT,
    PRIMARY KEY (Tenant_SS, Rental_No,Apt_No),
    FOREIGN KEY (Tenant_SS) REFERENCES Tenant (Tenant_SS),
	FOREIGN KEY (Apt_no) REFERENCES Apartment (Apt_No),
    FOREIGN KEY (Rental_No) REFERENCES Rental (Rental_No)
);


CREATE TABLE Rental_Invoice (
    Invoice_No   INT    IDENTITY(1000,1)   NOT NULL,
    Invoice_Date DATETIME  NULL,
    Invoice_Due  MONEY  NULL,
    CC_No	VARCHAR(16)      NULL,
    CC_Type      VARCHAR (10) NULL,
    CC_Exp_Date  DATETIME  NULL,
    CC_Amt       MONEY      NULL,
    Rental_No    INT       NULL,
    PRIMARY KEY (Invoice_No),
    FOREIGN KEY (Rental_No) REFERENCES Rental (Rental_No)
);

CREATE TABLE Complaint (
    Complaint_No     INT   IDENTITY(10010,1)     NOT NULL,
    Complaint_Date   DATETIME   NULL,
    Rental_Complaint VARCHAR (160) NULL,
    Apt_Complaint    VARCHAR (160) NULL,
    Status           VARCHAR (1)  NULL,
    Rental_No        INT        NULL,
	Apt_no INT NULL,
    PRIMARY KEY  (Complaint_No ),
    FOREIGN KEY (Rental_No) REFERENCES Rental (Rental_No),
	FOREIGN KEY (Apt_no) REFERENCES Apartment (Apt_no)
);


CREATE TABLE Testimonial (
    Testimonial_ID      INT   IDENTITY(100,1)     NOT NULL,
    Testimonial_Date    DATETIME   NULL,
    Testimonial_Content VARCHAR (256) NULL,
    Tenant_SS           INT        NULL,
    PRIMARY KEY (Testimonial_ID ),
    FOREIGN KEY (Tenant_SS) REFERENCES Tenant (Tenant_SS)
);

--Store Procedure
USE [TEAM4OARS]
GO

/****** Object:  StoredProcedure [dbo].[sp_findTestimonial]    Script Date: 4/18/2016 9:55:05 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Bertol Salgado>
-- Create date: <04/18/2016>
-- Description:	<Store Procedure to Search Testimonials using keyword>
-- =============================================
ALTER PROCEDURE [dbo].[sp_findTestimonial]
	-- Add the parameters for the stored procedure here
	@Keyword nvarchar(50)
AS
BEGIN
SET NOCOUNT ON
	    -- Insert statements for procedure here
	Select Testimonial_id,Testimonial_Content 
	from TEAM4OARS.Testimonial 
	where Testimonial_Content like '%'+@Keyword+'%'
END

GO

--ADD DATA
-- =============================================
-- Author:		<Stephen Carver>
-- Create date: <04/4/2016>
-- Description:	<Baseline Data>
-- =============================================

--STAFF

INSERT INTO [TEAM4OARS].[Staff]  VALUES ('SA200', 'Joe', 'White', 'Assistant', 'M', '1982-07-08 00:00:00', 24000, 'Assistant1', 'ASSISTANT1#');
INSERT INTO [TEAM4OARS].[Staff]  VALUES ('SA210', 'Ann', 'Tremble', 'Assistant', 'F', '1981-06-12 00:00:00', 26000, 'Assistant2', 'ASSISTANT2#');
INSERT INTO [TEAM4OARS].[Staff]  VALUES ('SA220', 'Terry', 'Ford', 'Manager', 'M', '1967-10-20 00:00:00', 53000, 'Manager', 'MANAGER#');
INSERT INTO [TEAM4OARS].[Staff]  VALUES ('SA230', 'Susan', 'Brandon', 'Supervisor', 'F', '1977-03-10 00:00:00', 46000, 'Supervisor', 'SUPERVISOR#');
INSERT INTO [TEAM4OARS].[Staff]  VALUES ('SA240', 'Julia', 'Roberts', 'Assistant', 'F', '1982-09-12 00:00:00', 28000, 'Assistant3', 'ASSISTANT3#');


--TENANT

INSERT INTO [TEAM4OARS].[Tenant]  VALUES (123456789, 'Jack Robin'      , '1960-06-21 00:00:00', 'M', '4173452323', '4175556565', 'Kraft Inc.', 'M', 'Tenant1', 'TENANT1#');
INSERT INTO [TEAM4OARS].[Tenant]  VALUES (723556089, 'Mary Stackles'   , '1980-08-02 00:00:00', 'S', '4175453320', '4176667565', 'Kraft Inc.', 'F', 'Tenant2', 'TENANT2#');
INSERT INTO [TEAM4OARS].[Tenant]  VALUES (450452267, 'Ramu Reddy'      , '1962-04-11 00:00:00', 'M', '4178362323', '4172220565', 'SMSU'      , 'M', 'Tenant3', 'TENANT3#');
INSERT INTO [TEAM4OARS].[Tenant]  VALUES (223056180, 'marion Black'    , '1981-05-25 00:00:00', 'S', '4174257766', '4176772364', 'SMSU'      , 'M', 'Tenant4', 'TENANT4#');
INSERT INTO [TEAM4OARS].[Tenant]  VALUES (173662690, 'Venessa Williams', '1970-03-12 00:00:00', 'M', '4175557878', '4173362565', 'Kraft Inc.', 'F', 'Tenant5', 'TENANT5#');

--APARTMENT

INSERT INTO [TEAM4OARS].[Apartment]  VALUES (100, '0', 'R', 'Y', 200, 300);
INSERT INTO [TEAM4OARS].[Apartment]  VALUES (101, '0', 'R', N'N', 200, 300);
INSERT INTO [TEAM4OARS].[Apartment]  VALUES (102, '0', 'R', 'Y', 200, 300);
INSERT INTO [TEAM4OARS].[Apartment]  VALUES (103, '1', 'V', N'N', 300, 400);
INSERT INTO [TEAM4OARS].[Apartment]  VALUES (104, '1', 'R', 'Y', 300, 400);
INSERT INTO [TEAM4OARS].[Apartment]  VALUES (200, '2', 'V', 'Y', 400, 500);
INSERT INTO [TEAM4OARS].[Apartment]  VALUES (201, '2', 'R', 'Y', 400, 500);
INSERT INTO [TEAM4OARS].[Apartment]  VALUES (202, '3', 'V', 'Y', 500, 700);
INSERT INTO [TEAM4OARS].[Apartment]  VALUES (203, '3', 'R', 'Y', 500, 700);

--TENANT_AUTO

INSERT INTO [TEAM4OARS].[Tenant_Auto]  VALUES ('SYK332', 'Ford'  , 'Taurus', 1999, 'Red'   , 123456789);
INSERT INTO [TEAM4OARS].[Tenant_Auto]  VALUES ('TTS430', 'Volvo' , 'GL 740', 1990, 'Green' , 123456789);
INSERT INTO [TEAM4OARS].[Tenant_Auto]  VALUES ('ABC260', 'Toyota', 'Lexus' , 2000, 'Maroon', 723556089);
INSERT INTO [TEAM4OARS].[Tenant_Auto]  VALUES ('LLT322', 'Honda' , 'Accord', 2001, 'Blue'  , 450452267);
INSERT INTO [TEAM4OARS].[Tenant_Auto]  VALUES ('KTK100', 'Toyota', 'Camry' , 1999, 'Black' , 450452267);
INSERT INTO [TEAM4OARS].[Tenant_Auto]  VALUES ('FLT232', 'Honda' , 'Civic' , 1999, 'Red'   , 223056180);
INSERT INTO [TEAM4OARS].[Tenant_Auto]  VALUES ('LLT668', 'Volvo' , 'GL 980', 2000, 'Velvet', 173662690);

--TENANT_FAMILY

INSERT INTO [TEAM4OARS].[Tenant_Family]  VALUES (444663434, 'Kay Robin ', 'Y', 'N', 'N', 'N', 'F', '1965-06-21 00:00:00', 123456789);
INSERT INTO [TEAM4OARS].[Tenant_Family]  VALUES (222664343, 'Sarla Reddy', 'Y', 'N', 'N', 'N', 'F', '1965-06-11 00:00:00', 450452267);
INSERT INTO [TEAM4OARS].[Tenant_Family]  VALUES (222663434, 'Anjali Reddy ', 'N', 'Y', 'N', 'N', 'F', '1990-08-10 00:00:00', 450452267);
INSERT INTO [TEAM4OARS].[Tenant_Family]  VALUES (111444663, 'Terry Williams', 'Y', 'N', 'N', 'N', 'F', '1968-03-21 00:00:00', 173662690);
INSERT INTO [TEAM4OARS].[Tenant_Family]  VALUES (242446634, 'Tom Williams ', 'N', 'Y', 'N', 'N', 'M', '1991-05-20 00:00:00', 173662690);


--RENTAL

INSERT INTO [TEAM4OARS].[Rental]  VALUES ('2001-05-12 00:00:00', 'O', '2001-06-30 00:00:00', 'One', '2001-06-01 00:00:00', '2003-05-31 00:00:00', '2003-03-31 00:00:00','SA200',201);
INSERT INTO [TEAM4OARS].[Rental]  VALUES ('2001-05-21 00:00:00', 'O', '2001-06-30 00:00:00', 'Six', '2001-06-01 00:00:00', '2003-05-31 00:00:00', '2003-03-31 00:00:00','SA220',102);
INSERT INTO [TEAM4OARS].[Rental]  VALUES ('2001-10-12 00:00:00', 'O', '2001-11-30 00:00:00', 'Six', '2001-11-01 00:00:00', '2003-11-30 00:00:00', '2003-09-30 00:00:00','SA240',203);
INSERT INTO [TEAM4OARS].[Rental]  VALUES ('2002-03-06 00:00:00', 'O', '2002-04-30 00:00:00', 'One', '2002-04-01 00:00:00', '2003-03-31 00:00:00', '2003-01-31 00:00:00','SA210',101);
INSERT INTO [TEAM4OARS].[Rental]  VALUES ('2002-04-15 00:00:00', 'O', '2002-05-30 00:00:00', 'One', '2002-05-01 00:00:00', '2003-04-30 00:00:00', '2003-02-28 00:00:00','SA220',104);
INSERT INTO [TEAM4OARS].[Rental]  VALUES ('2002-07-15 00:00:00', 'S', '2002-08-30 00:00:00', 'One', '2002-08-01 00:00:00', '2003-06-30 00:00:00', '2003-06-30 00:00:00','SA200',100);

--OWNS

INSERT INTO [TEAM4OARS].[Owns] ([Tenant_SS], [Rental_No], [Apt_No]) VALUES (223056180, 100104, 101);
INSERT INTO [TEAM4OARS].[Owns] ([Tenant_SS], [Rental_No], [Apt_No]) VALUES (723556089, 100102, 102);
INSERT INTO [TEAM4OARS].[Owns] ([Tenant_SS], [Rental_No], [Apt_No]) VALUES (173662690, 100105, 104);
INSERT INTO [TEAM4OARS].[Owns] ([Tenant_SS], [Rental_No], [Apt_No]) VALUES (123456789, 100101, 201);
INSERT INTO [TEAM4OARS].[Owns] ([Tenant_SS], [Rental_No], [Apt_No]) VALUES (450452267, 100103, 203);

--RENTAL_INVOICE

INSERT INTO [TEAM4OARS].[Rental_Invoice]  VALUES ('2001-03-12 00:00:00', 500, '1234567890123456', 'visa', '2002-12-01 00:00:00', 500, 100101);
INSERT INTO [TEAM4OARS].[Rental_Invoice]  VALUES ('2001-04-30 00:00:00', 500, '1234567890123456', 'visa', '2002-12-01 00:00:00', 500, 100101);
INSERT INTO [TEAM4OARS].[Rental_Invoice]  VALUES ('2001-05-20 00:00:00', 500, '1234567890123456', 'visa', '2002-12-01 00:00:00', 500, 100101);
INSERT INTO [TEAM4OARS].[Rental_Invoice]  VALUES ('2001-06-30 00:00:00', 500, '1234567890123456', 'visa', '2002-12-01 00:00:00', 500, 100101);
INSERT INTO [TEAM4OARS].[Rental_Invoice]  VALUES ('2001-07-30 00:00:00', 500, '1234567890123456', 'mastercard', '2002-12-01 00:00:00', 500, 100101);
INSERT INTO [TEAM4OARS].[Rental_Invoice]  VALUES ('2001-08-30 00:00:00', 500, '1234567890123456', 'mastercard', '2002-12-01 00:00:00', 500, 100101);
INSERT INTO [TEAM4OARS].[Rental_Invoice]  VALUES ('2001-09-30 00:00:00', 500, '1234567890123456', 'visa', '2002-12-01 00:00:00', 500, 100101);
INSERT INTO [TEAM4OARS].[Rental_Invoice]  VALUES ('2001-10-30 00:00:00', 500, '1234567890123456', 'visa', '2002-12-01 00:00:00', 500, 100101);
INSERT INTO [TEAM4OARS].[Rental_Invoice]  VALUES ('2001-11-30 00:00:00', 500, '1234567890123456', 'visa', '2002-12-01 00:00:00', 500, 100101);
INSERT INTO [TEAM4OARS].[Rental_Invoice]  VALUES ('2001-05-21 00:00:00', 300, '3343567890123456', 'mastercard', '2003-10-01 00:00:00', 300, 100102);
INSERT INTO [TEAM4OARS].[Rental_Invoice]  VALUES ('2001-06-30 00:00:00', 300, '3343567890123456', 'mastercard', '2003-10-01 00:00:00', 300, 100102);
INSERT INTO [TEAM4OARS].[Rental_Invoice]  VALUES ('2001-06-30 00:00:00', 300, '3343567890123456', 'mastercard', '2003-10-01 00:00:00', 300, 100102);
INSERT INTO [TEAM4OARS].[Rental_Invoice]  VALUES ('2001-08-30 00:00:00', 300, '3343567890123456', 'mastercard', '2003-10-01 00:00:00', 300, 100102);
INSERT INTO [TEAM4OARS].[Rental_Invoice]  VALUES ('2001-09-30 00:00:00', 300, '3343567890123456', 'mastercard', '2003-10-01 00:00:00', 300, 100102);
INSERT INTO [TEAM4OARS].[Rental_Invoice]  VALUES ('2001-10-30 00:00:00', 300, '3343567890123456', 'mastercard', '2003-10-01 00:00:00', 300, 100102);
INSERT INTO [TEAM4OARS].[Rental_Invoice]  VALUES ('2001-11-30 00:00:00', 300, '3343567890123456', 'mastercard', '2003-10-01 00:00:00', 300, 100102);
INSERT INTO [TEAM4OARS].[Rental_Invoice]  VALUES ('2001-10-12 00:00:00', 700, '8654567890123456', 'discover  ', '2003-11-01 00:00:00', 700, 100103);
INSERT INTO [TEAM4OARS].[Rental_Invoice]  VALUES ('2001-11-30 00:00:00', 700, '8654567890123456', 'discover  ', '2003-11-01 00:00:00', 700, 100103);
INSERT INTO [TEAM4OARS].[Rental_Invoice]  VALUES ('2002-03-06 00:00:00', 500, '7766567890123456', 'visa', '2003-09-01 00:00:00', 500, 100104);
INSERT INTO [TEAM4OARS].[Rental_Invoice]  VALUES ('2002-04-30 00:00:00', 300, '7766567890123456', 'visa', '2003-09-01 00:00:00', 300, 100104);
INSERT INTO [TEAM4OARS].[Rental_Invoice]  VALUES ('2002-05-30 00:00:00', 300, '7766567890123456', 'visa', '2003-09-01 00:00:00', 300, 100104);
INSERT INTO [TEAM4OARS].[Rental_Invoice]  VALUES ('2002-06-30 00:00:00', 300, '7766567890123456', 'visa', '2003-09-01 00:00:00', 300, 100104);
INSERT INTO [TEAM4OARS].[Rental_Invoice]  VALUES ('2002-07-30 00:00:00', 300, '7766567890123456', 'visa', '2003-09-01 00:00:00', 300, 100104);
INSERT INTO [TEAM4OARS].[Rental_Invoice]  VALUES ('2002-04-15 00:00:00', 700, '7766567890123456', 'visa', '2003-12-01 00:00:00', 700, 100105);
INSERT INTO [TEAM4OARS].[Rental_Invoice]  VALUES ('2002-05-30 00:00:00', 400, '6599567890126211', 'visa', '2003-12-01 00:00:00', 400, 100105);
INSERT INTO [TEAM4OARS].[Rental_Invoice]  VALUES ('2002-06-30 00:00:00', 400, '6599567890126211', 'discover  ', '2003-12-01 00:00:00', 400, 100105);
INSERT INTO [TEAM4OARS].[Rental_Invoice]  VALUES ('2002-07-30 00:00:00', 400, '6599567890126211', 'discover  ', '2003-12-01 00:00:00', 400, 100105);



--COMPLAINT

INSERT INTO [TEAM4OARS].[Complaint]  VALUES ( '2001-06-12 00:00:00', NULL, 'kitchen sink clogged', 'F', 100103,203);
INSERT INTO [TEAM4OARS].[Complaint]  VALUES ( '2001-08-17 00:00:00', NULL, 'water heater not working', 'F', 100105,104);
INSERT INTO [TEAM4OARS].[Complaint]  VALUES ( '2002-09-17 00:00:00', NULL, 'room heater problem ', 'F', 100105,104);
INSERT INTO [TEAM4OARS].[Complaint]  VALUES ( '2002-09-17 00:00:00', NULL, 'air conditioning not working', NULL, NULL,103);
INSERT INTO [TEAM4OARS].[Complaint]  VALUES ( '2002-10-20 00:00:00', 'car parking not proper ', NULL, NULL, 100103,NULL);
INSERT INTO [TEAM4OARS].[Complaint]  VALUES ( '2002-11-08 00:00:00', 'delay in payment ', NULL, 'F', 100102,NULL);
INSERT INTO [TEAM4OARS].[Complaint]  VALUES ( '2002-11-16 00:00:00', NULL, 'utility not working ', NULL, NULL,202);

--TESTIMONIAL

INSERT INTO [TEAM4OARS].[Testimonial]  VALUES ('2002-03-31 00:00:00', 'I really like TEAM4OARS Online Apartment Rental System!', 123456789);
INSERT INTO [TEAM4OARS].[Testimonial]  VALUES ('2002-04-09 00:00:00', 'I think that this TEAM4OARS website can be improved!', 450452267);
INSERT INTO [TEAM4OARS].[Testimonial]  VALUES ('2002-04-25 00:00:00', 'I believe that the Tenants and Visitors will like TEAM4OARS since they can rent and manage apartments online.', 173662690);

