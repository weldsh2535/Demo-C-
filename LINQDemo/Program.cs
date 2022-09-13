using LINQDemo;

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
        Console.ReadKey();
    }

    public static List<Employee> GetEmployees()
    {
        List<Employee> employees = new List<Employee>();

        Employee employee = new Employee
        {
            ID = 1,
            FirstName = "Bob",
            LastName = "Jones",
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
        return employees;
    }
    public static List<Department> GetDepartments()
    {
        List<Department> departments = new List<Department>();
        Department department = new Department
        {
            Id = 1,
            ShortName = "CS",
            LongName = "Computer seicnce"
        };
        departments.Add(department);
        Department department1 = new Department
        {
            Id = 1,
            ShortName = "IT",
            LongName = "Information Technology"
        };
        departments.Add(department1);
        return departments;
    }
}