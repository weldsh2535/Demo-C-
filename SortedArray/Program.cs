class Program
{
    static void Main(string[] args)
    {
       // object[] arr = new object[] { 3, 2, 1, 4, 5 };
       // string[] arr = new string[] { "Abebe", "Lema", "Zeleke", "zeleke", "Zelalem" };
        Employee[] arr = new Employee[] {
            new Employee {Id=5,Name="Seife" },
            new Employee {Id=2,Name="Mogess"} ,
            new Employee {Id=3,Name="Hagos"},
            new Employee {Id=1,Name="pawolos"}
        };
        SortedArray<Employee> sortedArray = new SortedArray<Employee>();
        sortedArray.BubbleSort(arr);
        foreach (object item in arr)
            Console.WriteLine(item);
        Console.ReadKey();
    }
}
public class Employee : IComparable
{
    public int Id { get; set; }
    public string Name { get; set; }

    public int CompareTo(object? obj)
    {
        return this.Name.CompareTo(((Employee)obj).Name);
    }
    public override string ToString()
    {
        return $" {Id} {Name}";
    }
}
public class SortedArray<T>
{
    public void BubbleSort(T[] arr)
    {
        int n = arr.Length;
        for (int i = 0; i < n - 1; i++)
            for (int j = 0; j < n - i - 1; j++)
                if (((IComparable)arr[j]).CompareTo(arr[j + 1]) > 0)
                {
                    Swap(arr, j);
                }
    }
    private void Swap(T[] arry, int j)
    {
        T temp = arry[j];
        arry[j] = arry[j + 1];
        arry[j + 1] = temp;
    }
}