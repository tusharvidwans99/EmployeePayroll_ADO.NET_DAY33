using System;
using System.Collections.Generic;
using System.Data;
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
                        foreach (var employee in employeeDetailsList)
                        {
                            Console.WriteLine($"EmployeeID: {employee.EmployeeID}\nEmployeeName: {employee.EmployeeName}\nPhoneNumber: {employee.PhoneNumber}\nAddress: {employee.Address}\nDepartment: {employee.Department}\nGender: {employee.Gender}\nBasic Pay: {employee.BasicPay}\nDeduction: {employee.Deduction}\nTaxable Pay: {employee.TaxablePay}\nTax: {employee.Tax}\nNet Pay: {employee.NetPay}\nStart Date: {employee.StartDate}\nCity: {employee.City}\nCountry: {employee.Country}");
                            Console.WriteLine("---------X------------");
                            Console.WriteLine("---------X------------");

                        }

                    }
                    else
                    {
                        throw new Exception("No data found");
                    }
                    dr.Close();
                    this.connection.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }


        public bool AddEmployeeModel(EmployeeModel employee)
        {
            try
            {
                using (this.connection)
                {
                    SqlCommand command = new SqlCommand("SpAddEmployeeDetails",this.connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@EmployeeName", employee.EmployeeName);
                    command.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);
                    command.Parameters.AddWithValue("@Address", employee.Address);
                    command.Parameters.AddWithValue("@Department", employee.Department);
                    command.Parameters.AddWithValue("@Gender", employee.Gender);
                    command.Parameters.AddWithValue("@BasicPay", employee.BasicPay);
                    command.Parameters.AddWithValue("@Deduction", employee.Deduction);
                    command.Parameters.AddWithValue("@TaxablePay", employee.TaxablePay);
                    command.Parameters.AddWithValue("@Tax", employee.Tax);
                    command.Parameters.AddWithValue("@NetPay", employee.NetPay);
                    command.Parameters.AddWithValue("@StartDate", DateTime.Now);
                    command.Parameters.AddWithValue("@City", employee.City);
                    command.Parameters.AddWithValue("@Country", employee.Country);
                    this.connection.Open();
                    var result = command.ExecuteNonQuery();
                    this.connection.Close();

                    if(result != 0)
                    {
                        return true;
                    }
                    return false;

                    

                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }

    }
}
