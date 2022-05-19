using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChickenGames.Algorithm
{
    using Sort;

    internal class Program
    {
        static void Main(string[] args)
        {
            int[] array = new int[]
            {
                2, 1, 6, 8,6,4,3, 2, 1, 0,
            };
            array.QuickSort();
            array.Log();
        }


    }

    static class Extension
    {
        public static void Log<T>(this T[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                Console.WriteLine(values[i].ToString());
            }

        }
    }
}
