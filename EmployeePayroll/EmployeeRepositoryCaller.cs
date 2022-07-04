using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeePayroll
{
    class EmployeeRepositoryCaller
    {
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
            employeeModel.StartDate = Convert.ToDateTime("2022-07-05");
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
        public static void RetrievingSpecificDateRangeData()
        {
            EmployeeRepository employeeRepository = new EmployeeRepository();
            List<EmployeeModel> list = new List<EmployeeModel>();
            try
            {
                list = employeeRepository.GetAllemployeeInDateRange();
                foreach (EmployeeModel employees in list)
                {
                    Console.WriteLine($"Employee Id :{employees.EmployeeID}");
                    Console.WriteLine($"Employee Name :{employees.EmployeeName}");
                    Console.WriteLine($"Employee Salary :{employees.BasicPay}");
                    Console.WriteLine($"Employee startdate :{employees.StartDate}");
                    Console.WriteLine($"Employee Deductions :{employees.Deductions}");
                    Console.WriteLine($"Employee Taxable Pay :{employees.TaxablePay}");
                    Console.WriteLine($"Tax:{employees.Tax}");
                    Console.WriteLine($"Net pay:{employees.NetPay}");

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex);
            }
        }
        public static void RetrievingGroupedDataByGender()
        {
            EmployeeRepository employeeRepository = new EmployeeRepository();
            List<EmployeeModel> list = new List<EmployeeModel>();
            try
            {
                list = employeeRepository.GetGroupedData();
                foreach (EmployeeModel employees in list)
                {
                    Console.WriteLine($"Gender: {employees.Gender} totalSum: {employees.totalSalary} MaxSalary: {employees.maxSalary} ");

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex);
            }
        }


        public static void AddingDataInMultipleTable()
        {
            //initializing employeemodel
            EmployeeModel employeeModel = new EmployeeModel();
            //adding values to variables
            employeeModel.EmployeeID = 32;
            employeeModel.EmployeeName = "Tushar";
            employeeModel.Gender = "M";
            employeeModel.PhoneNumber = 9585858585;
            employeeModel.StartDate = Convert.ToDateTime("2022-07-15");
            employeeModel.BasicPay = 500000;
            employeeModel.Deductions = 50000;
            employeeModel.TaxablePay = 450000;
            employeeModel.Tax = 50000;
            employeeModel.NetPay = 400000;
            employeeModel.Address = "Pune";
            employeeModel.companyId = 107;
            employeeModel.salaryid = 16;
            employeeModel.companyName = "Xceedance";
            employeeModel.departmentid = 11;
            employeeModel.Department = "Angular Developer";
            employeeModel.headOfDepartment = "Mr. Pratik";
            employeeModel.noOfEmployees = 44;


            EmployeeRepository employeeRepository = new EmployeeRepository();
            bool result = employeeRepository.InsertingDataIntoMultipleTables(employeeModel);
            
            Console.WriteLine(result == true ? "data writtern in database" : "data is not written in database");
        }
    }
}