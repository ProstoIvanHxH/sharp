using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics; //подключение пространства имен для использования
namespace Example //класса Stopwatch
{
    class Program
    {
        static void Main()
        {

            double x = 2;
            double xn = x; //сохраняем исходное значение x
            Console.Write("end=");
            double n = double.Parse(Console.ReadLine());
            
            Stopwatch timer = new Stopwatch(); //создаем экземпляр класса Stopwatch
            timer.Start(); //включаем таймер
            List<double> list = new List<double>();
            list.Add(2);
            for (int i = 3; i <= 10; i++)
            {
                int k = 0;
                foreach (double d in list)
                {

                    if (i % d == 0)
                    {
                        k++;
                        break;
                    }

                }
                if (k == 0)
                {
                    list.Add(i);
                }
            }
            timer.Stop(); //выключаем таймер
            //foreach (double d in list)
            //{
            //    Console.WriteLine(d);
            //}
            //выводим время выполнения алгоритма
            Console.WriteLine("время выполнения {0} такта", timer.ElapsedTicks);
            timer.Restart();
            timer.Start(); //включаем таймер
            
            int arrlength = (int)((n+1)/2);
            BitArray Data = new BitArray(arrlength);
            Data.SetAll(true);

            for (int i = 1; i + i + 2 * i * i < Data.Length; i++)
            {
                for (int j = i; i + j + 2 * i * j < Data.Length; j++)
                {
                    Data[i + j + 2 * i * j] = false;
                }
            }
            timer.Stop(); //выключаем таймер
            //for (int i = 1; i < Data.Length; i++)
            //{
            //    if (Data[i])
            //        Console.WriteLine(i * 2 + 1);
            //}
            Console.WriteLine("время выполнения {0} такта", timer.ElapsedTicks);
        }
    }

}