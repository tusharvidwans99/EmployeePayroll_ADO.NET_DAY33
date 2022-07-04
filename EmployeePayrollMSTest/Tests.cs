using EmployeePayroll;

namespace EmployeePayrollMSTest
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void GivenSalaryDetails_AbleToUpdateSalaryDetails()
        {

            //Arrange
            EmployeeRepository repository = new EmployeeRepository();
            EmployeeModel model = new EmployeeModel();
            model.EmployeeID = 1;
            model.EmployeeName = "Vishal";
            model.BasicPay = 2000;

            //Act
            repository.UpdatingSalaryInDataBase(model);
            decimal actual = repository.ReadingUpdatedSalaryFromDataBase(model);

            //Assert
            Assert.AreEqual(model.BasicPay, actual);
        }

    }
}