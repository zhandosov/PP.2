using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            int n = Convert.ToInt32(Console.ReadLine());
            string[,] arr = new string[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    arr[i, j] = "[*]";
                }
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    Console.Write(arr[i, j]);
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }
    }
}
