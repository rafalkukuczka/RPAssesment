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
            int effectiveYears = Years > 5 ? 5 : Years;

            return Type switch
            {
                EmployeeType.Trainee => Salary * 1.01M,

                EmployeeType.Junior => Salary * (1.05M + effectiveYears / 100M),

                EmployeeType.Senior => Salary * (1.1M + effectiveYears / 100M),

                EmployeeType.Manager => Salary * (1.15M + effectiveYears / 100M),

                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
