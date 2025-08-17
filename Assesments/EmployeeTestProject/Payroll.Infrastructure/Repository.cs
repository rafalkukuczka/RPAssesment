using EmployeeTestProject.Payroll.Domain;

namespace EmployeeTestProject.Payroll.Infrastructure
{
    public interface IEmployeeRepository
    {
        List<Employee> GetAll();
        void Save(Employee employee);
    }

    public class InMemoryEmployeeRepository : IEmployeeRepository
    {
        private readonly List<Employee> _employees = new();

        public List<Employee> GetAll() => _employees;

        public void Save(Employee employee) => _employees.Add(employee);
    }
}

