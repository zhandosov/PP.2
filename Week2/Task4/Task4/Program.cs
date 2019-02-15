using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Task4
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            string path1 = @"C:\Users\Олжас\source\repos\Week2\Task4\dir1.txt";
            string path2 = @"C:\Users\Олжас\source\repos\Week2\Task4\Task4\dir1.txt";
            FileInfo fs = new FileInfo(path1);
            fs.CopyTo(path2);
            Console.WriteLine("{0} was copied to {1}.", path1, path2);
            fs.Delete();
            Console.WriteLine("{0} was successfully deleted.", path1);
            Console.ReadKey();
        }
    }
}