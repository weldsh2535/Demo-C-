using LINQDemo;
using System.Collections;
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
        Console.WriteLine();
        Console.WriteLine("OfType filter operator");
        //OfType filter Operator
        ArrayList mixed = GetHetrogeneousDataCollection();

        Console.WriteLine();
        Console.WriteLine("OfType filter String arraylist");
        Console.WriteLine(" =============== ");
        var stringRes = from s in mixed.OfType<string>()
                        select s;
        foreach (var item in stringRes)
            Console.WriteLine(item);

        Console.WriteLine();
        Console.WriteLine("OfType filter operator int arraylist");
        Console.WriteLine(" =============== ");
        var intRes = from s in mixed.OfType<int>()
                        select s;
        foreach (var item in intRes)
            Console.WriteLine(item);

        Console.WriteLine();
        Console.WriteLine("OfType filter operator employee arraylist");
        Console.WriteLine(" =============== ");
        var employeeRes = from s in mixed.OfType<Employee>()
                        select s;
        foreach (var item in employeeRes)
            Console.WriteLine($"{item.FirstName,-10} {item.LastName,-10} {item.AnnualSalary}");
        //ElemnetAt,ElementAtOrDefault,First,FirstDefault,Last,LastOrDefault,Single and SingleOrDefault
        Console.WriteLine(" =============== ");
       
        var emps = employeeList.ElementAt(1);
         Console.WriteLine($"{emps.FirstName,-10} {emps.LastName,-10} {emps.AnnualSalary}");
        //First,FirstDefault,Last,LastOrDefault
        Console.WriteLine("First =============== ");
        List<int> integerList = new List<int> { 1,5, 20, 3,55 };
        int result12 = integerList.First();
        int result13 = integerList.Last();
        int result14 = integerList.First(c => c % 2==0);
        Console.WriteLine(result14);
        Console.WriteLine(result13);
        Console.WriteLine(" =============== ");
        //Single and SingleOrDefault
        Console.WriteLine(" =============== ");
        var emp12 = employeeList.Single(c=>c.ID == 2);
        Console.WriteLine($"{emp12.FirstName,-10} {emp12.LastName,-10} {emp12.AnnualSalary}");
        //Equality Operator 
        ///SequenceEqual
        Console.WriteLine(" =============== ");
        var integerList1 = new List<int> { 1,5, 20, 3,55 };
        var integerLIst2 = new List<int> { 1,5,20, 30,55 };
        var boolSequenceEqual = integerList1.SequenceEqual(integerLIst2);
        Console.WriteLine(boolSequenceEqual);

        Console.WriteLine(" =============== ");
        var employeeListCompare = GetEmployees();
        bool boolSE = employeeList.SequenceEqual(employeeListCompare,new EmployeeComparer());
        Console.WriteLine(boolSE);
        //Concatenation Operator
        ///concat
        Console.WriteLine(" =============== ");
        var integerList12 = new List<int> { 1, 2, 20, 3, 55 };
        var integerLIst22 = new List<int> { 10, 5, 0, 30, 15 };
        IEnumerable<int> integerListConcat = integerList12.Concat(integerLIst22);

        foreach (int integer in integerListConcat)
            Console.WriteLine(integer);
        Console.WriteLine(" =============== ");
        ////Aggregate Operators -Aggregate,Average,Count,Sum and Max
        ///Aggregate Operator
        decimal totalAnnualSalary = employeeList.Aggregate<Employee, decimal>(0, (totalAnnualSalary, e) =>
        {
            var bonus = (e.IsManager) ? 0.04m : 0.02m;
            totalAnnualSalary = (e.AnnualSalary + (e.AnnualSalary * bonus)) + totalAnnualSalary;
            return totalAnnualSalary;
        });
    
        Console.WriteLine($"Total Annual Salary of All employees(including bonus):{totalAnnualSalary}");
        Console.WriteLine(" =============== ");
        string data = employeeList.Aggregate<Employee, string>("Employee Annual Salaries (including bonus):",
            (s, e) =>
            {
                var bonus = (e.IsManager) ? 0.04m : 0.02m;
                s += $"{e.FirstName} {e.LastName} - {e.AnnualSalary + (e.AnnualSalary * bonus)},";
                return s;
            }
           );
        Console.WriteLine(data);
        Console.WriteLine(" =============== ");
        string data12 = employeeList.Aggregate<Employee, string ,string>("Employee Annual Salaries (including bonus):",
            (s, e) =>
            {
                var bonus = (e.IsManager) ? 0.04m : 0.02m;
                s += $"{e.FirstName} {e.LastName} - {e.AnnualSalary + (e.AnnualSalary * bonus)},";
                return s;
            }, s => s.Substring(0,s.Length - 2)
           );
        Console.WriteLine(data12);
        ////Average
        Console.WriteLine(" =============== ==============");
        decimal average = employeeList.Average(e => e.AnnualSalary);
        Console.WriteLine($"Average Annual Salary (Technology Department):{average}");

        ///Count
        Console.WriteLine(" =============== ==============");
        int count = employeeList.Count(c=>c.DepartmentId ==2);
        Console.WriteLine($"Employee numbers {count}");
        ///Sum
        Console.WriteLine(" =============== ==============");
        decimal sums = employeeList.Sum(c=>c.AnnualSalary);
        Console.WriteLine($"The sum of Employee: {sums}");
        ///Max
        Console.WriteLine(" =============== ==============");
        int max = (int)employeeList.Max(c=>c.AnnualSalary);
        Console.WriteLine($"The maximum of employee:{max}");
        ///Generation Operators
        ///DefaultEmpty
        List<int> intList = new List<int>();
        var newList = intList.DefaultIfEmpty();
        Console.WriteLine($"DefaultEmpty {newList.ElementAt(0)}");

        List<Employee> employee12 = new List<Employee>();
        var newList1 = employee12.DefaultIfEmpty( new Employee { ID=0});
        var result21 = newList1.ElementAt(0);
        if(result21.ID == 0)
        {
            Console.WriteLine($"The list is empty");
        }
        ////Range
        Console.WriteLine(" =============== ==============");
        var intCollection = Enumerable.Range(2, 9);
        foreach (var item in intCollection)
            Console.WriteLine(item);
        ////Conversion Operators - TolIst,ToDictionary,ToArray
        Console.WriteLine(" =============== ==============");
        //ToList
        List<Employee> res21 = (from emp in employeeList
                            where emp.AnnualSalary > 10000
                            select emp).ToList();
        foreach (var item in res21)
            Console.WriteLine($"{item.FirstName,-10} {item.LastName,-10} {item.AnnualSalary}");
        Console.WriteLine(" =============== ==============");
        //ToDictionary
        Dictionary<int,Employee> dictionary = (from emp in employeeList
                                where emp.AnnualSalary > 10000
                                select emp).ToDictionary<Employee,int>(e=>e.ID);
        foreach (var key in dictionary.Keys)
            Console.WriteLine($"Key:{key} ,Value {dictionary[key].FirstName,-10} {dictionary[key].LastName,-10} {dictionary[key].AnnualSalary}");

        Console.WriteLine(" =============== ==============");
        //ToArray List
        Employee[] arrayList = (from emp in employeeList
                                where emp.AnnualSalary > 10000  
                                select emp).ToArray();
        foreach (var item in arrayList)
            Console.WriteLine($"{item.FirstName,-10} {item.LastName,-10} {item.AnnualSalary}");
        ///Let
        Console.WriteLine(" =============== ==============");
        var result121 = from emp in employeeList
                      let Initials = emp.FirstName.Substring(0, 1).ToUpper() + emp.LastName.Substring(0, 1).ToUpper()
                      let AnnualSalaryBonus = (emp.IsManager) ? emp.AnnualSalary + (emp.AnnualSalary * 0.04m) : emp.AnnualSalary
                      where Initials == "LG" || Initials == "ET" && AnnualSalaryBonus > 20000
                      select new
                      {
                          Initials = Initials,
                          FullName = emp.FirstName + " " + emp.LastName,
                          AnnualSalaryPlusBouns = AnnualSalaryBonus
                      };
        foreach (var item in result121)
            Console.WriteLine($"{item.Initials,-5} {item.FullName,-20} {item.AnnualSalaryPlusBouns,10}"); ;
       
        Console.ReadKey();
    }

    public static ArrayList GetHetrogeneousDataCollection()
    {
        ArrayList arrayList = new ArrayList();
        arrayList.Add(100);
        arrayList.Add("Yosef Abebe");
        arrayList.Add(200);
        arrayList.Add(3000);
        arrayList.Add("Ermiyas Debebe");
        arrayList.Add(new Employee { ID = 9, FirstName = "Hana ", LastName = "Demeke", AnnualSalary = 30000, DepartmentId = 3, IsManager = false });
        arrayList.Add(new Employee { ID = 10, FirstName = "Hilen ", LastName = "Tadese", DepartmentId = 2, AnnualSalary = 20000, IsManager = false });
        arrayList.Add(new Department { Id = 4, LongName = "Information system", ShortName = "IS" });
        arrayList.Add(new Department { Id = 5, LongName = "Managment", ShortName = "MA" });
        return arrayList;
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
            AnnualSalary = 30000,
            IsManager = true,
            DepartmentId = 1
        };
        employees.Add(employee);
        Employee employee1 = new Employee
        {
            ID = 2,
            FirstName = "Eyob",
            LastName = "Tadese",
            AnnualSalary = 60000,
            IsManager = false,
            DepartmentId = 1
        };
        employees.Add(employee1);
        Employee employee2 = new Employee
        {
            ID = 3,
            FirstName = "Genenew",
            LastName = "Abayineh",
            AnnualSalary = 20000,
            IsManager = false,
            DepartmentId = 1
        };
        employees.Add(employee2);
        Employee employee3 = new Employee
        {
            ID = 4,
            FirstName = "Shambel",
            LastName = "Kassa",
            AnnualSalary = 20000,
            IsManager = false,
            DepartmentId = 2
        };
        employees.Add(employee3);
        Employee employee4 = new Employee
        {
            ID = 5,
            FirstName = "Gichie",
            LastName = "Kokobe",
            AnnualSalary = 20000,
            IsManager = false,
            DepartmentId = 2
        };
        employees.Add(employee4);
        Employee employee5 = new Employee
        {
            ID = 6,
            FirstName = "Hana",
            LastName = "Jed",
            AnnualSalary = 20000,
            IsManager = false,
            DepartmentId = 2
        };
        employees.Add(employee5);
        Employee employee6 = new Employee
        {
            ID = 7,
            FirstName = "Robil",
            LastName = "Demeke",
            AnnualSalary = 20000,
            IsManager = false,
            DepartmentId = 3
        };
        employees.Add(employee6);
        Employee employee7 = new Employee
        {
            ID = 8,
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