# General knowledge

## Class or Struct
Give the main differences between a `struct` and a `class` in .Net.

## Variance
Can you explain the advantages of covariance on a Generic interface ? Give an example.
In what situation would you use contravariance on a Generic interface ? Give an example.

## Thread safe
Give at least 2 examples to increment a shared variable in a thread safe manner.

# Monad
Given the following unit tests:
```csharp
namespace Monad;

public class OptionTests
{
    private Option<SomeClass> defaultOption;

    [Fact]
    public void Empty_Option_Has_No_Value()
    {
        Assert.False(Option.Empty<SomeClass>().HasValue);
    }

    [Fact]
    public void Default_Option_IsEmpty()
    {
        Assert.Equal(Option.Empty<SomeClass>(), defaultOption);
    }

    [Fact]
    public void Non_Empty_Option_Has_Value()
    {
        Assert.True(Option.FromValue(new SomeClass()).HasValue);
    }

    [Fact]
    public void Empty_Option_Value_Throws()
    {
        Assert.Throws<InvalidOperationException>(() => Option.Empty<SomeClass>().Value);
    }

    [Fact]
    public void Non_Empty_Option_Returns_Its_Value()
    {
        var value = new SomeClass();
        Assert.Same(value, Option.FromValue(value).Value);
    }

    [Fact]
    public void Comparison_Is_Correct_If_Not_Empty()
    {
        var someClass = new SomeClass();
        Assert.True(Option.FromValue(someClass) == Option.FromValue(someClass));
        Assert.False(Option.FromValue(someClass) == Option.Empty<SomeClass>());
    }

    [Fact]
    public void Empty_Option_ValueOr_Evaluates_Else_Branch()
    {
        var expected = new SomeClass();
        Assert.Same(expected, Option.Empty<SomeClass>().ValueOr(() => expected));
    }

    [Fact]
    public void Empty_Option_Select_Returns_Empty()
    {
        var result = Option.Empty<SomeClass>().Select(v => v.GetType());
        Assert.True(result == Option.Empty<Type>());
    }

    [Fact]
    public void Non_Empty_Option_Select_Returns_ExpectedType()
    {
        var result = Option.FromValue(new SomeClass()).Select(v => v.GetType());
        Assert.True(result == Option.FromValue(typeof(SomeClass)));
    }

    [Fact]
    public void Non_Empty_Option_ValueOr_Does_Not_Evaluate_Else_Branch()
    {
        var value = new object();
        var option = Option.FromValue(value);
        Func<SomeClass> fct = () =>
        {
            // we will break on purpose here.
            Assert.True(false);
            return null;
        };
        Assert.Same(value, option.ValueOr(fct));
    }

    private class SomeClass
    {
    }
}
```
Write the `Option` and `Option<T>` types that implement the code making these tests run successfully.

# Refactoring
Given the following software code, propose a refactored solution for this piece of code.
You will be evaluated on:
* Clean code
* SOLID principles
* DDD methodology
* Unit tests creation
* General .net usage and knowledge
* Creativity
```csharp
/// <summary>
/// This EmployeeProcessor class performs several operations on employee objects.
/// It calculates the new salary of an employee, based on their type (Trainee, Junior, Senior, Manager) 
/// and the number of years spent in the company.
/// It also generates a report for the employee.
/// </summary>
public class EmployeeProcessor
{
    public decimal CalculateNewSalary(int type, int years, decimal salary)
    {
        decimal newSalary = 0;
        if (type == 1) // Trainee
        {
            newSalary = salary / 100 + salary;
        }
        if (type == 2) // Junior
        {
            newSalary = (salary * 5 / 100) + (salary * years > 5 ? 5 : years) / 100 + salary;
        }
        if (type == 3) // Senior
        {
            newSalary = (salary * years > 5 ? 5 : years) / 100 + 1.1M * salary;
        }
        if (type == 4) // Manager
        {
            newSalary = (salary * years > 5 ? 5 : years) / 100 + salary + (salary * 15 / 100);
        }
        return newSalary;
    }

    public string GenerateEmployeeReport(Employee employee)
    {
        var newSalary = CalculateNewSalary(employee.Type, employee.Years, employee.Salary);
        var report = $"Employee Name: {employee.MaskName()}, Type: {employee.Type.ToString()}, Years: {employee.Years}, Salary: {employee.Salary}, New Salary: {newSalary}";
        return reportSpan;
    }

    public string GenerateReport(List<Employee> employees)
    {
        var report = "";
        foreach (var employee in employees)
        {
            report += GenerateEmployeeReport(employee);
        }
        return report;
    }

    public class Employee
    {
        public string Name { get; set; }
        public int Type { get; set; }
        public int Years { get; set; }
        public decimal Salary { get; set; }

        public string MaskName()
        {
            var firstChars = Name.Substring(0, 3);
            var length = Name.Length - 3;

            for(int i = 0; i < length; i++)
            {
                firstChars += "*";
            }

            return firstChars;
        }
    }
}
```

