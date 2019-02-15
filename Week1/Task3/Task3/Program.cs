using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            int n = Convert.ToInt32(Console.ReadLine());//создание переменной и задание значение
            int[] ar1 = new int[n];//создание массива с типом integer
            int[] ar2 = new int[n * 2];//создание массива с типом integer у которого размер в 2 раза больше предыдущего массива
            string[] s = Console.ReadLine().Split();//создание стрингового массива
            for (int i = 0; i < n; i++)
            {
                ar1[i] = Convert.ToInt32(s[i]);//конвертация элементов стрингового массива и запись как элемента массива типа integer
            }
            int j = 0;
            for (int i = 0; i < ar1.Length; i++)
            {
                ar2[j++] = ar1[i];//вывод массива два раза
                ar2[j++] = ar1[i];
            }
            for (int i = 0; i < ar2.Length; i++)
            {
                Console.Write(ar2[i] + " ");//вывод элементов нового массива
            }
            Console.ReadKey();
        }
    }
}
