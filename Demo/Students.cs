using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    internal class Students : Persons
    {
        public Students(string firstname, string lastname, string sex , string department,int age) : base(firstname, lastname, sex,department,age)
        {
        }
        public string getFullName()
        {
            return this.Firstname + " " + this.LastName;
        }
    }
}
