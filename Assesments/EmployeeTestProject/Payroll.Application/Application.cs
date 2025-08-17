using EmployeeTestProject.Payroll.Domain;

namespace EmployeeTestProject.Payroll.Application
{
    public class EmployeeService
    {
        public string GenerateEmployeeReport(Employee employee)
        {
            var newSalary = employee.CalculateNewSalary();
            return $"Employee Name: {employee.MaskName()}, " +
                   $"Type: {employee.Type}, " +
                   $"Years: {employee.Years}, " +
                   $"Salary: {employee.Salary}, " +
                   $"New Salary: {newSalary}";
        }

        public string GenerateReport(List<Employee> employees)
        {
            return string.Join("\n", employees.Select(GenerateEmployeeReport));
        }
    }
}

