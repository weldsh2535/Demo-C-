
using Demo;
using System.Collections;
class Program
{
    static void Main(string[] args)
    {
        Switch sw = new Switch();
        sw.opretions();
        Persons myObj = new Persons("Abebe","Lema","Males","IT",23);
        List<Students> studList = new List<Students>();
        Students[] s = new Students[2]
        {
         new Students("Abebe", "Lema", "Males", "IT", 23),
         new Students("Melkamu","Debebe","Male","CS",30)
        };
        
           foreach(Students stu in s)
        {
            studList.Add(stu);
        }
        studList.ForEach(s =>
        {
            Console.Write(s.Firstname + " " +s.LastName + " " +s.Sex + " " +s.Department + " " +s.Age);
            Console.WriteLine();
        });
        
        Console.WriteLine("Fullname is "+ s[1].getFullName());
        Console.WriteLine(myObj.Firstname);
        Console.WriteLine(myObj.LastName);
        Console.WriteLine(myObj.Sex);
        
        //To access character methods
        myObj.character();
    }
}
class Switch{
    public void opretions()
    {
        double num1, num2,res;
        char op;
        Console.WriteLine("Enter the first number : ");
        num1 = Convert.ToDouble( Console.ReadLine());
        Console.WriteLine("Enter the second number: ");
        num2 = Convert.ToDouble(Console.ReadLine());
        Console.WriteLine("Enter the oprete + / - / *//");
        op = (char)Console.Read();
        switch (op)
        {
            case '+':
                res = num1 + num2;
                Console.WriteLine("The sum is " + res);
                break;
            case '-':
                res = num1 - num2;
                Console.WriteLine("The Difference == " + res);
                break;
            case '*':
                res = num1 * num2;
                Console.WriteLine("The multiply is " + res);
                break;
            case '/':
                res = num1 / num2;
                Console.WriteLine("The Division == " + res);
                break;
            default:
                Console.WriteLine("Invalid Operator");
                break;
        }
           
             
    }
}
/*ArrayList arrayList = new ArrayList();
arrayList.Add("Abebe");
arrayList.Add("Tadese");

foreach (String i in arrayList)
    Console.WriteLine(i);
List<Int32> a = new List<Int32>();
a.Add(21);
a.Add(41);
a.Add(23);
a.Add(71);
a.Add(1);
for(int i =0;i< a.Count; i++)
{
    Console.WriteLine(a[i]);
}

Console.WriteLine("Enter the numbers");
String var1 = Console.ReadLine();
int var = Convert.ToInt32(var1);


Console.WriteLine("The Value is "+var);*/