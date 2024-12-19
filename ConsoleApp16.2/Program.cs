using System;
using System.IO;
using static System.Net.WebRequestMethods;
namespace MyProgram
{
    struct Car //структура наследует стандартный интерфейс
    { // IComparable<in T>, в данном случае в качестве
        public int money,years;
        public String mark, name, year;
        // обобщенного типа T будет выступать тип SPoint
        public Car(String name, String year, String mark, int money , int years)
        {
            this.mark = mark;
            this.years = years;
            this.name = name;
            this.year = year;
            this.money = money;
        }
        public String Show()
        {
            return String.Join(separator: " ", name,  year,  mark,  money,  years);

        }

    }
    class Program
    {
        static public Car[] Input() //читаем данные из файла
        {
            using (StreamReader fileIn = new StreamReader("input.txt"))
            {
                int n = int.Parse(fileIn.ReadLine());
                Car[] ar = new Car[n];
                for (int i = 0; i < n; i++)
                {
                    string[] text = fileIn.ReadLine().Split(',');
                    ar[i] = new Car(text[0], text[1], text[2], int.Parse(text[3]), int.Parse(text[4]));
                }
                return ar;
            }
        }
        static void Print(Car[] array) //выводим данные на экран
        {
            foreach (Car item in array)
            {
                item.Show();
            }
        }
        static void Main()
        {
            Car[] array = Input();
            List<Car> list = new List<Car>();

            var students = array.Where(n => n.money < 10000);


            using (StreamWriter fileOut = new StreamWriter("out.txt", false))
            {
                foreach (Car student in students)
                {
                    
                    fileOut.WriteLine(student.Show());
                }



            }

        }
    }
}