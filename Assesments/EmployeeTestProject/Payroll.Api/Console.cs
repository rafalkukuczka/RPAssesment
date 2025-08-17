using EmployeeTestProject.Payroll.Domain;
using EmployeeTestProject.Payroll.Infrastructure;
using EmployeeTestProject.Payroll.Application;

var repo = new InMemoryEmployeeRepository();
var service = new EmployeeService();

repo.Save(new Employee("Alice", EmployeeType.Junior, 3, 3000));
repo.Save(new Employee("Bob", EmployeeType.Senior, 7, 6000));

Console.WriteLine(service.GenerateReport(repo.GetAll()));
