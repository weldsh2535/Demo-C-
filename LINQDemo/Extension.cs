using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQDemo
{
    public static class Extension
    {
       public static List<T> Filter<T>(this List<T> records,Func<T, bool> func)
        {
            List<T> filteredList = new List<T>();
            foreach (T record in records)
            {
                if (func(record))
                {
                    filteredList.Add(record);
                }
            }
            return filteredList;
        }
    }
}
