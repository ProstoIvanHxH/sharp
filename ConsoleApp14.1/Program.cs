using System;
using System.IO;
namespace MyProgram
{
    struct SPoint : IComparable<SPoint> //структура наследует стандартный интерфейс
    { // IComparable<in T>, в данном случае в качестве
        public int x, y,z; // обобщенного типа T будет выступать тип SPoint
        public SPoint(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public void Show()
        {
            Console.WriteLine("({0}, {1}, {2})", x, y,z);
        }
        public double Distance() //метод
        {
            return Math.Sqrt(x * x + y * y);
        }
        public double Odis(SPoint obj)
        {
            return Math.Sqrt((Math.Pow(this.x - obj.x, 2) + Math.Pow(this.y - obj.y, 2)+ Math.Pow(this.z - obj.z, 2)));
        }
        public int CompareTo(SPoint obj) //проводим переопределением метода
        { // CompareTo(T) так, чтобы сравнение элементов
            if (this.Distance() == obj.Distance()) // типа SPoint проводилось по возрастанию
            { ///расстоянию от точки до начала координат
                return 0;
            }
            else
            {
                if (this.Distance() > obj.Distance())
                {
                    return 1;
                }
                
else
                {
                    return -1;
                }
            }
        }
    }
    class Program
    {
        static public SPoint[] Input() //читаем данные из файла
        {
            using (StreamReader fileIn = new StreamReader("input.txt"))
            {
                int n = int.Parse(fileIn.ReadLine());
                SPoint[] ar = new SPoint[n];
                for (int i = 0; i < n; i++)
                {
                    string[] text = fileIn.ReadLine().Split(' ');
                    ar[i] = new SPoint(int.Parse(text[0]), int.Parse(text[1]), int.Parse(text[2]));
                }
                return ar;
            }
        }
        static void Print(SPoint[] array) //выводим данные на экран
        {
            foreach (SPoint item in array)
            {
                item.Show();
            }
        }
        static void Main()
        {
            SPoint[] array = Input();
            List<List<SPoint>> otv= new List<List<SPoint>>();
            double maks = 99999999999;
            for (int ik = 0; ik < array.Length; ik++)
            {
                SPoint i = array[ik];
                foreach (SPoint j in array[(ik+1)..array.Length])
                {
                    //j.Show();
                    if (i.Odis(j) < maks)
                    {
                        otv.Clear();
                        otv.Add(new List<SPoint> { i, j });
                        maks= i.Odis(j);
                    }
                    else if(i.Odis(j) == maks)
                    {
                        otv.Add(new List<SPoint> { i, j });
                    }
                }
            }
            foreach (var item in otv)
            {
                foreach (SPoint item2 in item)
                {
                    item2.Show();       
                }
                Console.WriteLine();
            }
            //Array.Sort(array); //Вызов стандартной сортировки для класса Array
            //Console.WriteLine("Упорядоченные данные: ");
            //Print(array);
        }
    }
}