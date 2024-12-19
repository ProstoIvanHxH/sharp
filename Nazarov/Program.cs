using System;
using System.Collections;

namespace CSh_Sundaram
{
    class Program
    {
        static void Main()
        {
            Sundaram test=new Sundaram(100000);
            Console.WriteLine(test.dt);
        }
    }
    public class Sundaram
    {
        public double dt;          // время просеивания (сек)
        private long t1;           // начало просеивания
        private long t2;           // окончание просеивания
        private uint limit;        // верхняя граница диапазона просеивания
        private int arrlength;     // размер массива
        private BitArray Prim;     // массив результатов просеивания
        private int counter;

        public Sundaram(uint _limit)
        {
            limit = _limit;
            if (limit % 2 == 0) limit -= 1;
            arrlength = (int)(limit / 2);
            Prim = new BitArray(arrlength);
            t1 = DateTime.Now.Ticks;
            Sieve();                 // Просеивание
            t2 = DateTime.Now.Ticks;
            dt = (double)(t2 - t1) / 10000000D;
            counter = -1;
        }

        //public uint NextPrime
        //{
        //    get
        //    {
        //        while (counter < arrlength - 1)
        //        {
        //            counter++;
        //            if (!Prim[counter])
        //                return (uint)(2 * counter + 3);
        //        }
        //        return 0;
        //    }
        //}

        private void Sieve()
        {
            int imax = ((int)Math.Sqrt(limit) - 1) / 2;
            for (int i = 1; i <= imax; i++)
            {
                int jmax = (arrlength - i) / (2 * i + 1);
                for (int j = i; j <= jmax; j++)
                {
                    Prim[2 * i * j + i + j - 1] = true;
                }
            }
        }
    }
}