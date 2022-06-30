using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePayroll
{
    public class EmployeeRepository
    {

        //static as connection will be made only once in application
        List<EmployeeModel> employeeDetailsList = new List<EmployeeModel>();
        public static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=payroll_service_ADONET;Integrated Security=True";
        SqlConnection connection = new SqlConnection(connectionString);

        public void GetAllemployee()
        {

            try
            {
                using (this.connection)
                {
                    string query = "select * from employee_payroll";
                    SqlCommand sqlCommand = new SqlCommand(query, this.connection);
                    this.connection.Open();
                    SqlDataReader dr = sqlCommand.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            EmployeeModel employeeModel = new EmployeeModel();
                            employeeModel.EmployeeID = dr.GetInt32(0);
                            employeeModel.EmployeeName = dr.GetString(1);
                            employeeModel.PhoneNumber = dr.GetString(2);
                            employeeModel.Address = dr.GetString(3);
                            employeeModel.Department = dr.GetString(4);

                            employeeModel.Gender = Convert.ToChar(dr.GetString(5));
                            employeeModel.BasicPay = dr.GetDouble(6);
                            employeeModel.Deduction = dr.GetDouble(7);
                            employeeModel.TaxablePay = dr.GetDouble(8);
                            employeeModel.Tax = dr.GetDouble(9);
                            employeeModel.NetPay = dr.GetDouble(10);
                            employeeModel.StartDate = dr.GetDateTime(11);
                            employeeModel.City = dr.GetString(12);
                            employeeModel.Country = dr.GetString(13);


                            //display retrieved record
                            employeeDetailsList.Add(employeeModel);


                        }
                        foreach(var employee in employeeDetailsList)
                        {
                            Console.WriteLine($"{employee.EmployeeID} | {employee.EmployeeName} | {employee.PhoneNumber} | {employee.Gender} | {employee.BasicPay} | {employee.Department}");
                            
                        }
                      
                    }
                    else
                    {
                        throw new Exception("No data found");
                    }
                    dr.Close();
                    this.connection.Close();
                }
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            



}
        }
}
