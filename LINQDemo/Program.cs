using LINQDemo;
using System.Diagnostics.CodeAnalysis;

class Program
{
    static void Main(string[] args)
    {
        List<Employee> employees = GetEmployees();
        var filterdEmployes = employees.Filter(emp => emp.IsManager == true);
        foreach (Employee e in filterdEmployes)
        {
            Console.WriteLine("Full Name "+e.FirstName + " " + e.LastName );
            Console.WriteLine("Annual Salary " + e.AnnualSalary);
            Console.WriteLine("Departmnet " + e.DepartmentId);
            Console.WriteLine();
        }

        List<Department> departments = GetDepartments();
        var filterdDepList = departments.Where(c => c.Id > 1);
        foreach (Department dep in departments)
        {
            Console.WriteLine("Short name " + dep.ShortName);
            Console.WriteLine("Long Name " + dep.LongName);
            Console.WriteLine();
        }
        List<Employee> employeeList = GetEmployees();
        List<Department> departmentList = GetDepartments();

        var resultList = from emp in employeeList
                         join dept in departmentList
                         on emp.DepartmentId equals dept.Id
                         select new
                         {
                             FirstName = emp.FirstName,
                             LastName = emp.LastName,
                             AnuualSalary = emp.AnnualSalary,
                            Manager = emp.IsManager,
                             Department = dept.LongName
                         };
        foreach (var e in resultList)
        {
            Console.WriteLine("Full Name " + e.FirstName + " " + e.LastName);
            Console.WriteLine("Annual Salary " + e.AnuualSalary);
            Console.WriteLine("Long Name " + e.Department);
            Console.WriteLine("Manager " + e.Manager);
            Console.WriteLine();
        }
        Console.WriteLine("select and where operators =================== ");
        
        var res = employeeList.Select(e => new
        {
            fullname = e.FirstName + " " + e.LastName,
            anuualsalary = e.AnnualSalary
        }).Where(e => e.anuualsalary >= 10000);
        foreach (var item in res)
        {
            Console.WriteLine($"{item.fullname,-20} {item.anuualsalary,10}");
        }

        Console.WriteLine("select Join method query =================== ");
        var results = departmentList.Join(employeeList,
            Department => Department.Id,
            Employee => Employee.DepartmentId,
            (Department, Employee) => new
            {
                FullName = Employee.FirstName + " " + Employee.LastName,
                AnuualSalary = Employee.AnnualSalary,
                departmentName = Department.LongName
            });
        foreach (var result in results)
        {
            Console.WriteLine($"{result.FullName,-20} {result.AnuualSalary,10}\t{result.departmentName}");
        }

        Console.WriteLine("select groupJoin method query =================== ");
        //select groupJoin method query 
        var result1 = departmentList.GroupJoin(employeeList,
            dept => dept.Id, emp => emp.DepartmentId,
            (dept, employeeGroup) => new
            {
                Employees = employeeGroup,
                DepartmentName = dept.LongName
            });

        foreach (var item in result1)
        {
            Console.WriteLine($"{item.DepartmentName}");
            foreach (var emp in item.Employees)
                Console.WriteLine($"\t{emp.FirstName} {emp.LastName}");
        }

        Console.WriteLine("GroupJoin operator Example Query Syntax =================== ");
        //GroupJoin operator Example Query Syntax
        var result2 = from dept in departmentList
                      join emp in employeeList
                      on dept.Id equals emp.DepartmentId
                      into employeeGroup
                      select new
                      {
                          Employees = employeeGroup,
                          DepartmentName = dept.LongName
                      };
        foreach (var item in result2)
        {
            Console.WriteLine($"{item.DepartmentName}");
            foreach (var emp in item.Employees)
                Console.WriteLine($"\t{emp.FirstName} {emp.LastName}");
        }

        Console.WriteLine("Method Syntax ============================== ");
        //Method Syntax 
        var res2 = employeeList.Join(departmentList,e=>e.DepartmentId,d=>d.Id,
            (emp,dept)=> new
            {
                Id = emp.ID,
                FirstName = emp.FirstName,
                LastName = emp.LastName,
                AnnualSalary = emp.AnnualSalary,
                DepartmentId = emp.DepartmentId,
                DepartmentName = dept.LongName
            }).OrderBy(e=>e.DepartmentId).ThenBy(e=>e.AnnualSalary);
        foreach (var item in res2)
        {
            Console.WriteLine($"First Name: {item.FirstName,10} Last Name: {item.LastName,10}\tAnnualSalary: {item.AnnualSalary,10} DepartmentName: {item.DepartmentName}");
        }
        Console.WriteLine("Query Syntax ========================== ");
        //Query syntax
        var res1 = from emp in employeeList
                   join dept in departmentList
                   on emp.DepartmentId equals dept.Id
                   orderby emp.DepartmentId, emp.AnnualSalary descending
                   select new
                   {
                       Id = emp.ID,
                       FirstName = emp.FirstName,
                       LastName = emp.LastName,
                       AnnualSalary = emp.AnnualSalary,
                       DepartmentId = emp.DepartmentId,
                       DepartmentName = dept.LongName
                   };
        foreach (var item in res1)
        {
            Console.WriteLine($"First Name: {item.FirstName,10} Last Name: {item.LastName,10}\tAnnualSalary: {item.AnnualSalary,10} DepartmentName: {item.DepartmentName}");
        }
        //Grouping Operators
        //GroupBy
        Console.WriteLine("GroupBy ==================================");
        var groupResult = from emp in employeeList
                          group emp by emp.DepartmentId;
        foreach (var item in groupResult)
        {
            Console.WriteLine($"Department Id: {item.Key}");
            foreach (var item2 in item)
            {
                Console.WriteLine($"\tEmployee FullName :{item2.FirstName} {item2.LastName}");
            }
        }
        Console.WriteLine();

        //ToLookup Operator 
        Console.WriteLine("ToLookup Operator ========================================");
        //var groupRes = employeeList.ToLookup(e => e.DepartmentId);
        var groupRes = employeeList.OrderBy(o=>o.FirstName).ToLookup(e => e.DepartmentId);
        foreach (var item in groupRes)
        {
            Console.WriteLine($"Department Id: {item.Key}");
            foreach (var item2 in item)
            {
                Console.WriteLine($"\tEmployee FullName :{item2.FirstName} {item2.LastName}");
            }
        }
        Console.WriteLine();

        Console.WriteLine("All , Any , Contains Quantifer Operators =============");
        //All , Any , Contains Quantifer Operators
        // All And Any Operators
        var annualSalaryCompare = 20000;
        bool isTrueAll = employeeList.All(e => e.AnnualSalary > annualSalaryCompare);
        if (isTrueAll)
        {
            Console.WriteLine($"All employe annual salaries are above {annualSalaryCompare}");
        }
        else
        {
            Console.WriteLine($"No Employee have an annual salary above{annualSalaryCompare}");
        }
        bool isTrueAny = employeeList.Any(e => e.AnnualSalary > annualSalaryCompare);
        if (isTrueAny)
        {
            Console.WriteLine($"At least one employee has an annual salary above {annualSalaryCompare}");
        }
        else
        {
            Console.WriteLine($"No Employees have an annual salary above {annualSalaryCompare}");
        }
        Console.WriteLine();
        Console.WriteLine("Contains ======================================");
        var searchEmp = new Employee
        {
            ID = 1,
            FirstName = "Lakie",
            LastName = "Getie",
            AnnualSalary = 20000,
            IsManager = true,
            DepartmentId = 1
        };
        bool containsEmployee = employeeList.Contains(searchEmp , new EmployeeComparer());
        if (containsEmployee)
            Console.WriteLine($"An employee record for {searchEmp.FirstName} {searchEmp.LastName} is found ");
        else
            Console.WriteLine($"An employee record for {searchEmp.FirstName} {searchEmp.LastName} is not found");
       Console.ReadKey();
    }

    public class EmployeeComparer : IEqualityComparer<Employee>
    {
        public bool Equals(Employee? x, Employee? y)
        {
           if(x.ID == y.ID && x.FirstName.ToLower() == y.FirstName.ToLower() && x.LastName.ToLower() == y.LastName.ToLower())
            {
                return true;
            }
           return false;
        }

        public int GetHashCode([DisallowNull] Employee obj)
        {
            return obj.ID.GetHashCode();
        }
    }
    public static List<Employee> GetEmployees()
    {
        List<Employee> employees = new List<Employee>();

        Employee employee = new Employee
        {
            ID = 1,
            FirstName = "Lakie",
            LastName = "Getie",
            AnnualSalary = 20000,
            IsManager = true,
            DepartmentId = 1
        };
        employees.Add(employee);
        Employee employee1 = new Employee
        {
            ID = 1,
            FirstName = "Eyob",
            LastName = "Tadese",
            AnnualSalary = 50000,
            IsManager = false,
            DepartmentId = 1
        };
        employees.Add(employee1);
        Employee employee2 = new Employee
        {
            ID = 1,
            FirstName = "Genenew",
            LastName = "Abayineh",
            AnnualSalary = 20000,
            IsManager = false,
            DepartmentId = 1
        };
        employees.Add(employee2);
        Employee employee3 = new Employee
        {
            ID = 1,
            FirstName = "Shambel",
            LastName = "Kassa",
            AnnualSalary = 20000,
            IsManager = false,
            DepartmentId = 2
        };
        employees.Add(employee3);
        Employee employee4 = new Employee
        {
            ID = 1,
            FirstName = "Gichie",
            LastName = "Kokobe",
            AnnualSalary = 10000,
            IsManager = false,
            DepartmentId = 2
        };
        employees.Add(employee4);
        Employee employee5 = new Employee
        {
            ID = 1,
            FirstName = "Hana",
            LastName = "Jed",
            AnnualSalary = 20000,
            IsManager = false,
            DepartmentId = 2
        };
        employees.Add(employee5);
        Employee employee6 = new Employee
        {
            ID = 1,
            FirstName = "Robil",
            LastName = "Demeke",
            AnnualSalary = 20000,
            IsManager = false,
            DepartmentId = 3
        };
        employees.Add(employee6);
        Employee employee7 = new Employee
        {
            ID = 1,
            FirstName = "Meskir",
            LastName = "Abebe",
            AnnualSalary = 20000,
            IsManager = false,
            DepartmentId = 3
        };
        employees.Add(employee7);
        return employees;
    }
    public static List<Department> GetDepartments()
    {
        List<Department> departments = new List<Department>();
        Department department = new Department
        {
            Id = 1,
            ShortName = "IT",
            LongName = "Information Technology"
        };
        departments.Add(department);
        Department department1 = new Department
        {
            Id = 2,
            ShortName = "CS",
            LongName = "Computer seicnce"
        };
        departments.Add(department1);
        Department department2 = new Department
        {
            Id = 3,
            ShortName = "SE",
            LongName = "Software engineering"
        };
        departments.Add(department2);
        return departments;
    }
}
public static class EnumerableCollectionExtensionMethods
{
    public static IEnumerable<Employee> GetHighSalariedEmployees(this IEnumerable<Employee> employees)
    {
        foreach (Employee emp in employees)
        {
            Console.WriteLine($"Accessing employee:{emp.FirstName + "" + emp.LastName}");
            if (emp.AnnualSalary >= 5000)
                yield return emp;
        }
    }
}