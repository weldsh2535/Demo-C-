using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    internal class Persons
    {
         string firstname;
         string lastname;
         string sex;
        string department;
        int age;
        public string Firstname
        {
            get { return firstname; }
            set { firstname = value; }
        }
        public string LastName
        {
            get { return lastname; }
            set { lastname = value; }
        }
        public string Sex
        {
            get { return sex; }
            set { sex = value; }
        }
        public string Department
        {
            get { return department; }
            set { department = value; }
        }
        public int Age
        {
            get { return age; }
            set { age = value; }
        }
        public Persons(string firstname, string lastname, string sex,string department,int age)
        {
            this.firstname = firstname;
            this.lastname = lastname;
            this.sex = sex;
            this.department = department;
            this.age = age;
        }
        public void character()
        {
            char[] list = { ' ','f','a', 'c', 'd','b','e' };
            Array.Sort(list);
            Console.WriteLine("List Of Characters ");
            foreach (char i in list)
            {
                Console.Write(i + " ");
            }
            string[] strings = {"Array List "+ "Alemu", "Aweke", "Ergete" };
            Array.Sort(strings);
            Console.WriteLine();
            foreach (string s in strings)
                Console.Write(s + " ");
        }
        
    }
}
