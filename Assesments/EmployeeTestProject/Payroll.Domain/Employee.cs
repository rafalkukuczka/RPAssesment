using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTestProject.Payroll.Domain
{
    

    public class Employee
    {
        public string Name { get; private set; }
        public EmployeeType Type { get; private set; }
        public int Years { get; private set; }
        public decimal Salary { get; private set; }

        public Employee(string name, EmployeeType type, int years, decimal salary)
        {
            Name = name;
            Type = type;
            Years = years;
            Salary = salary;
        }

        public string MaskName()
        {
            var visible = Name.Substring(0, 3);
            
            StringBuilder maskedName = new StringBuilder(visible);
            return maskedName.Append('*', Name.Length - 3).ToString();

        }

        public decimal CalculateNewSalary()
        {
            decimal newSalary = 0;
            if (Type == EmployeeType.Trainee) // Trainee
            {
                newSalary = Salary / 100 + Salary;
            }
            if (Type ==  EmployeeType.Junior) // Junior
            {
                newSalary = (Salary * 5 / 100) + (Salary * Years > 5 ? 5 : Years) / 100 + Salary;
            }
            if (Type == EmployeeType.Senior) // Senior
            {
                newSalary = (Salary * Years > 5 ? 5 : Years) / 100 + 1.1M * Salary;
            }
            if (Type == EmployeeType.Manager) // Manager
            {
                newSalary = (Salary * Years > 5 ? 5 : Years) / 100 + Salary + (Salary * 15 / 100);
            }
            return newSalary;
        }
    }
}
