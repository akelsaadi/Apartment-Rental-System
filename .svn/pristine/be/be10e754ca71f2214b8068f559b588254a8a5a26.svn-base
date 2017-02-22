CREATE VIEW [View_Tenant]
	AS SELECT Tenant_SS,Tenant_Name,Tenant_DOB,Tenant_Marital,Work_Phone,Home_Phone,Employer,Tenant_Gender FROM [Tenant];

CREATE VIEW [View_Rental]
	AS SELECT R.Rental_No,R.Rental_Date,A.Apt_no,A.Apt_Deposit_Amt,R.Lease_Type,S.Tenant_SS,S.Tenant_Name,S.Tenant_DOB,S.Work_Phone FROM [Rental] R, [Apartment] A,[Tenant] S, [Owns] O
	WHERE O.Apt_No = A.Apt_no
	AND O.Tenant_SS = S.Tenant_SS
	AND O.Rental_No = R.Rental_No;

CREATE VIEW [View_RentalRate]
	AS SELECT DISTINCT A.Apt_Type, A.Apt_Rent_Amt FROM [Apartment] A;

CREATE VIEW [View_VacantApt]
	AS SELECT DISTINCT A.Apt_no,A.Apt_Type, A.Apt_Rent_Amt 
	FROM [Apartment] A
	WHERE A.Apt_Status = 'V';

CREATE VIEW [View_LeaseLength]
	AS SELECT DISTINCT R.Lease_Start,R.Lease_End, DATEDIFF(dy,R.Lease_Start,R.Lease_End) AS 'Lease Length (Days)'
	FROM [Rental] R;

CREATE VIEW [View_MonthlyRentCollected]
	AS SELECT DATEPART(Year,Invoice_Date) [Year],DATEPART(Month,Invoice_Date) [Month],sum(Invoice_Due) AS [Total Rent Collected]
	FROM Rental_Invoice
	Group BY DATEPART(Year,Invoice_Date),DATEPART(Month,Invoice_Date);

create view [View_Pending_Complaints] as
	select isnull(c.Complaint_No,-1) as Complaint_No, nullif(a.Apt_no,-1) as Apt_no, a.Apt_type, 
	Description_Complaint=case when c.Rental_Complaint = null then c.Apt_Complaint else c.Rental_Complaint end from Complaint c 
	join Owns o on o.Tenant_SS = c.Tenant_SS 
	join Apartment a on a.Apt_no = o.Apt_No 
	where c.Status ='P';