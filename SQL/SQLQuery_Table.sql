create database payroll_service_ADONET

create table employee_payroll(
EmployeeID int Primary Key identity,
EmployeeName varchar(255),
PhoneNumber varchar(255),
Address varchar(255),
Department varchar(255),
Gender char(1),
BasicPay float,
Deduction float,
TaxablePay float,
Tax float,
NetPay float,
StartDate datetime,
City varchar(255),
Country varchar(255)
)

select * from employee_payroll

insert into employee_payroll values('Tushar','474743847','Maharashtra','Angular Developer','M',400000,40000,6000,6000,355000,GETDATE(),'Pune','India')