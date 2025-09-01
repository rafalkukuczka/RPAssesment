using EmployeeTestProject.Payroll.Application;
using EmployeeTestProject.Payroll.Domain;
using EmployeeTestProject.Payroll.Infrastructure;

namespace Payroll.Tests.Application
{
    public class EmployeeServiceTests
    {
        [Fact]
        public void GenerateEmployeeReport_ShouldIncludeMaskedNameAndNewSalary()
        {
            var employee = new Employee("Jonathan", EmployeeType.Junior, 3, 2000);
            var service = new EmployeeService();

            var report = service.GenerateEmployeeReport(employee);

            Assert.Contains("Jon*****", report);
            Assert.Contains("2160", report); 
        }

        [Fact]
        public void GenerateReport_ShouldReturnAllEmployeesReports()
        {
            var repo = new InMemoryEmployeeRepository();
            repo.Save(new Employee("Alice", EmployeeType.Trainee, 1, 1000));
            repo.Save(new Employee("Bob", EmployeeType.Manager, 5, 5000));

            var service = new EmployeeService();
            var report = service.GenerateReport(repo.GetAll());

            Assert.Contains("Ali*", report);
            Assert.Contains("Bob", report);
        }
    }
}
