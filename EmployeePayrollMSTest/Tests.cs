using EmployeePayroll;

namespace EmployeePayrollMSTest
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void GivenSalaryDetails_AbleToUpdateSalaryDetails()
        {
            EmployeeRepository repository = new EmployeeRepository();
            EmployeeModel model = new EmployeeModel();
            model.EmployeeID = 1;
            model.EmployeeName = "Vishal";
            model.BasicPay = 2000;
            repository.UpdatingSalaryInDataBase(model);
            decimal actual = repository.ReadingUpdatedSalaryFromDataBase();
            Assert.AreEqual(model.BasicPay, actual);
        }

    }
}