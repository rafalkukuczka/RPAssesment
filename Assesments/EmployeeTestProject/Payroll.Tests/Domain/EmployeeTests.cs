using EmployeeTestProject.Payroll.Domain;
using Xunit;

namespace Payroll.Tests.Domain
{
    public class EmployeeTests
    {
        [Fact]
        public void MaskName_ShouldMaskAllButFirstThreeLetters()
        {
            var employee = new Employee("Jonathan", EmployeeType.Junior, 3, 3000);
            var masked = employee.MaskName();
            Assert.Equal("Jon*****", masked);
        }

        [Theory]
        [InlineData(EmployeeType.Trainee, 0, 1000, 1010)]
        [InlineData(EmployeeType.Junior, 3, 2000, 2100)]
        [InlineData(EmployeeType.Senior, 7, 3000, 3300)] // 10% + 5% max
        [InlineData(EmployeeType.Manager, 2, 4000, 4600)] // 15% + 2%
        public void CalculateNewSalary_ShouldReturnExpected(EmployeeType type, int years, decimal salary, decimal expected)
        {
            var employee = new Employee("Alice", type, years, salary);
            var newSalary = employee.CalculateNewSalary();
            Assert.Equal(expected, newSalary);
        }
    }
}
