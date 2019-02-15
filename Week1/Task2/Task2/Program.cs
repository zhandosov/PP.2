using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    class MainClass
    {
        class Student
        {
            public string name;
            public string id;
            public int year;

            public Student(string name, string id)
            {
                this.name = name;
                this.id = id;
            }
            public Student()
            {
                name = Console.ReadLine();
                id = Console.ReadLine();
                year = Convert.ToInt32(Console.ReadLine());
            }
            public void PrintInfo()
            {
                Console.WriteLine(name);
                Console.WriteLine(id);
                Console.WriteLine(year + 1);
            }
        }
        class Program
        {
            static void Main(string[] args)
            {
                Student s = new Student();
                s.PrintInfo();
                Console.ReadKey();
            }
        }
    }
}

