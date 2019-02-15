using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Task1
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            string text = File.ReadAllText(@"C:\Users\Олжас\source\repos\Week2\Task1\polindrom.txt");
            for (int i = 0, j = text.Length - 1; i < j; i++, j--)
            {
                if (text[i] != text[j])
                {
                    Console.WriteLine("No");
                    Console.ReadKey();
                    return;
                }
            }
            Console.WriteLine("Yes");
            Console.ReadKey();
        }
    }
}
