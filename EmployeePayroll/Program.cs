namespace EmployeePayroll
{
    class program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Employee Payroll Program on which we are doing operation on SQL data using C#");

            EmployeeModel employeemodel = new EmployeeModel();
            EmployeeRepository employeeRepository = new EmployeeRepository();

            employeemodel.EmployeeName = "Shubham";
            employeemodel.PhoneNumber = "2904578563";
            employeemodel.Address = "Akola, Maharastra";
            employeemodel.Department = "Angular Developer";
            employeemodel.Gender = 'M';
            employeemodel.BasicPay = 350000;
            employeemodel.Deduction = 1500;
            employeemodel.TaxablePay = 250;
            employeemodel.Tax = 250;
            employeemodel.NetPay = 250000;
            employeemodel.City = "Pune";
            employeemodel.Country = "India";

            employeeRepository.AddEmployeeModel(employeemodel);

            employeeRepository.GetAllemployee();
        }
    }
}