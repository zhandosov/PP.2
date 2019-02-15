using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Task2
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            string[] text = File.ReadAllText(@"C:\Users\Олжас\source\repos\Week2\Task2\input.txt").Split();
            using (StreamWriter file = new StreamWriter(@"C:\Users\Олжас\source\repos\Week2\Task2\output.txt"))
            {
                for (int i = 0; i < text.Length; i++)
                {
                    int a = Convert.ToInt32(text[i]);
                    bool prime = true;
                    for (int j = 2; j < a; j++)
                    {
                        if (a % j == 0)
                            prime = false;
                    }
                    if (prime == true && a > 1)
                    {
                        file.Write(a + " ");
                    }
                }
            }
        }
    }
}
