using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQDemo
{
    public class Employee
    {

         public int  ID { get; set; }
         public string FirstName {get; set; }
         public string LastName {get; set; }
          public decimal  AnnualSalary {get; set; }
           public bool IsManager {get; set; }   
           public int  DepartmentId { get; set; }
    }
}
