namespace EmployeePayroll
{
    using System;
    using System.Collections.Generic;
    class Program
    {
        EmployeeModel employeeModel = new EmployeeModel();
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Employee Payroll");
            UpdatingSalary();
            ReadingUpdatedData();

        }
        public static void GettingAllData()
        {
            EmployeeRepository employeeRepository = new EmployeeRepository();
            List<EmployeeModel> list = employeeRepository.GetAllemployee();
            try
            {
                foreach (EmployeeModel employeeModel in list)
                {
                    Console.WriteLine($"Id: {employeeModel.EmployeeID} Name:{employeeModel.EmployeeName} CompanyName: {employeeModel.companyName} DepartmentName: {employeeModel.Department} phoneNumber: {employeeModel.PhoneNumber} gender: {employeeModel.Gender}  address: {employeeModel.Address} netpay={employeeModel.NetPay}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex);
            }
        }
        public static void AddingDataInDataBase()
        {
            EmployeeModel employeeModel = new EmployeeModel();
            employeeModel.EmployeeName = "Ajay";
            employeeModel.Gender = "M";
            employeeModel.PhoneNumber = 8585858585;
            employeeModel.StartDate = Convert.ToDateTime("2020-09-18");
            employeeModel.Address = "Bangalore";
            employeeModel.companyId = 101;
            employeeModel.salaryid = 03;
            EmployeeRepository employeeRepository = new EmployeeRepository();
            bool result = employeeRepository.AddEmployee(employeeModel);
            Console.WriteLine(result == true ? "data writtern in database" : "data is not written in database");

        }
        public static void UpdatingSalary()
        {
            EmployeeModel employeeModel = new EmployeeModel();
            employeeModel.EmployeeID = 1;
            employeeModel.EmployeeName = "Tushar";
            employeeModel.BasicPay = 300000;
            EmployeeRepository employeeRepository = new EmployeeRepository();
            bool result = employeeRepository.UpdatingSalaryInDataBase(employeeModel);
            Console.WriteLine(result == true ? "data writtern in database" : "data is not written in database");
        }
        public static void ReadingUpdatedData()
        {
            EmployeeRepository repository = new EmployeeRepository();
            decimal result = repository.ReadingUpdatedSalaryFromDataBase();
            Console.WriteLine("Updated Salary" + result);
        }
    }
}