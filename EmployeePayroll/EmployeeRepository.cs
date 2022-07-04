namespace EmployeePayroll
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq.Expressions;
    using System.Text;
    /// <summary>
    /// employee repository class to connect to database
    /// </summary>
    public class EmployeeRepository
    {
        List<EmployeeModel> employeeDetailsList = new List<EmployeeModel>();
        //connectionstring defines connection to be made to database
        public static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=payroll_service_ADONET;Integrated Security=True";
        //connection is made
        SqlConnection connection = new SqlConnection(connectionString);
        public List<EmployeeModel> GetAllemployee()
        {
            using (connection)
            {
                string query = "select * from employee e join payroll p on e.salaryid = p.salary_Id join EmployeeDepartment ed on ed.employeeID = e.id join company c on c.company_id = e.company_id join departments d on d.departmentID = ed.departmentID ";
                SqlCommand sqlCommand = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader dr = sqlCommand.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        EmployeeModel employeeModel = new EmployeeModel();
                        employeeModel.EmployeeID = dr.GetInt32(0);
                        employeeModel.EmployeeName = dr.GetString(1);
                        employeeModel.StartDate = dr.GetDateTime(2);
                        employeeModel.Gender = dr.GetString(3);
                        employeeModel.PhoneNumber = dr.GetInt64(4);
                        employeeModel.Address = dr.GetString(5);
                        employeeModel.companyId = dr.GetInt32(6);
                        employeeModel.BasicPay = dr.GetDecimal(8);
                        employeeModel.Deductions = dr.GetDecimal(9);
                        employeeModel.TaxablePay = dr.GetDecimal(10);
                        employeeModel.Tax = dr.GetDecimal(11);
                        employeeModel.NetPay = dr.GetDecimal(12);
                        employeeModel.companyName = dr.GetString(17);
                        employeeModel.Department = dr.GetString(19);

                        employeeDetailsList.Add(employeeModel);


                    }
                    dr.Close();
                    connection.Close();
                    return employeeDetailsList;
                }
                else
                {
                    throw new Exception("No data found");
                }

            }
        }
        public bool AddEmployee(EmployeeModel employeeModel)
        {
            try
            {
                using (this.connection)
                {
                    SqlCommand command = new SqlCommand("spAddEmployeeDetails", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Employeename", employeeModel.EmployeeName);
                    command.Parameters.AddWithValue("@phoneNumber", employeeModel.PhoneNumber);
                    command.Parameters.AddWithValue("@address", employeeModel.Address);
                    command.Parameters.AddWithValue("@Gender", employeeModel.Gender);
                    command.Parameters.AddWithValue("@companyid", employeeModel.companyId);
                    command.Parameters.AddWithValue("@start", employeeModel.StartDate);
                    command.Parameters.AddWithValue("@salaryid", employeeModel.salaryid);
                    connection.Open();
                    var result = command.ExecuteNonQuery();
                    connection.Close();
                    if (result != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }
        public bool UpdatingSalaryInDataBase(EmployeeModel employeeModel)
        {
            try
            {
                using (this.connection)
                {
                    SqlCommand sqlCommand = new SqlCommand("spUpdatingSalary", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@id", employeeModel.EmployeeID);
                    sqlCommand.Parameters.AddWithValue("@salary", employeeModel.BasicPay);
                    sqlCommand.Parameters.AddWithValue("@name", employeeModel.EmployeeName);
                    connection.Open();
                    int result = sqlCommand.ExecuteNonQuery();
                    if (result != 0)
                    {
                        return true;
                    }
                    return false;

                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public decimal ReadingUpdatedSalaryFromDataBase(EmployeeModel employeeModel)
        {
            using (this.connection)
            {
                decimal salary;
                EmployeeModel model = new EmployeeModel();
                SqlCommand sqlCommand = new SqlCommand("Select * from employee_payroll", connection);
                this.connection.Open();
                SqlDataReader dr = sqlCommand.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        model.EmployeeID = Convert.ToInt32(dr["id"]);
                        model.EmployeeName = dr["name"].ToString();
                        model.BasicPay = Convert.ToDecimal(dr["salary"]);
                    }
                    Console.WriteLine($"employeeId :{model.EmployeeID}, employeename: {model.EmployeeName}, salary :{model.BasicPay}");
                    salary = model.BasicPay;

                }
                else
                {
                    throw new Exception("no data found");
                }
                dr.Close();
                connection.Close();
                return salary;
            }

        }


        public List<EmployeeModel> GetAllemployeeInDateRange()
        {
            using (connection)
            {
                string query = "select * from employee_payroll where start between cast('2019-03-03' as date) and getdate();";
                SqlCommand sqlCommand = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader dr = sqlCommand.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        EmployeeModel employeeModel = new EmployeeModel();
                        employeeModel.EmployeeID = dr.GetInt32(0);
                        employeeModel.EmployeeName = dr.GetString(1);
                        employeeModel.BasicPay = dr.GetDecimal(2);
                        employeeModel.StartDate = dr.GetDateTime(3);
                        employeeModel.Gender = dr.GetString(4);
                        employeeModel.Deductions = dr.GetDecimal(5);
                        employeeModel.TaxablePay = dr.GetDecimal(6);
                        employeeModel.Tax = dr.GetDecimal(7);
                        employeeModel.NetPay = dr.GetDecimal(8);

                        employeeDetailsList.Add(employeeModel);


                    }
                    dr.Close();
                    connection.Close();
                    return employeeDetailsList;
                }
                else
                {
                    throw new Exception("No data found");
                }

            }
        }


        public List<EmployeeModel> GetGroupedData()
        {
            using (connection)
            {
                string query = "select gender, sum(salary) total_sum, max(salary) max_salary, min(salary) min_salary, AVG(salary) avg_salary, count(salary) CountOfGenders from employee_payroll group by gender";
                SqlCommand sqlCommand = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader dr = sqlCommand.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        EmployeeModel employeeModel = new EmployeeModel();
                        employeeModel.Gender = dr["Gender"].ToString();
                        employeeModel.totalSalary = dr.GetDecimal(1);
                        employeeModel.maxSalary = Convert.ToDecimal(dr["max_salary"]);

                        employeeDetailsList.Add(employeeModel);


                    }
                    dr.Close();
                    connection.Close();
                    return employeeDetailsList;
                }
                else
                {
                    throw new Exception("No data found");
                }

            }
        }


        public bool InsertingDataIntoMultipleTables(EmployeeModel employeeModel)
        {
            try
            {
                using (this.connection)
                {
                    SqlCommand command = new SqlCommand("insertingdata", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Employeeid", employeeModel.EmployeeID);
                    command.Parameters.AddWithValue("@phone_number", employeeModel.PhoneNumber);
                    command.Parameters.AddWithValue("@address", employeeModel.Address);
                    command.Parameters.AddWithValue("@Gender", employeeModel.Gender);
                    command.Parameters.AddWithValue("@company_id", employeeModel.companyId);
                    command.Parameters.AddWithValue("@start", employeeModel.StartDate);
                    command.Parameters.AddWithValue("@salaryid", employeeModel.salaryid);
                    command.Parameters.AddWithValue("@basepay", employeeModel.BasicPay);
                    command.Parameters.AddWithValue("@deductions", employeeModel.Deductions);
                    command.Parameters.AddWithValue("@taxable_pay", employeeModel.TaxablePay);
                    command.Parameters.AddWithValue("@tax", employeeModel.Tax);
                    command.Parameters.AddWithValue("@netpay", employeeModel.NetPay);
                    command.Parameters.AddWithValue("@name", employeeModel.EmployeeName);
                    command.Parameters.AddWithValue("@departmentid", employeeModel.departmentid);
                    command.Parameters.AddWithValue("@departmentname", employeeModel.Department);
                    command.Parameters.AddWithValue("@noOfEmployees", employeeModel.noOfEmployees);
                    command.Parameters.AddWithValue("@headofdepartment", employeeModel.headOfDepartment);
                    command.Parameters.AddWithValue("@companyname", employeeModel.companyName);
                    connection.Open();
                    var result = command.ExecuteNonQuery();
                    connection.Close();
                    if (result != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                this.connection.Close();
            }

        }
    }

}