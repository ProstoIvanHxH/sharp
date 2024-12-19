using System;
using System.IO;
using static System.Net.WebRequestMethods;
namespace MyProgram
{
    struct Bank : IComparable<Bank> //структура наследует стандартный интерфейс
    { // IComparable<in T>, в данном случае в качестве
        public int id, money;
        public String name, year;
        // обобщенного типа T будет выступать тип SPoint
        public Bank(String name,int id, int money, String year)
        {
            this.name = name;
            this.id = id;
            this.money = money;
            this.year = year;
        }
        public String Show()
        {
            return String.Join(separator:" ", name, id, money, year);
            
        }
        
        public int CompareTo(Bank obj) //проводим переопределением метода
        { // CompareTo(T) так, чтобы сравнение элементов
            if (this.money==obj.money) // типа SPoint проводилось по возрастанию
            { ///расстоянию от точки до начала координат
                return 0;
            }
            else
            {
                if (this.money > obj.money)
                {
                    return 1;
                } else
                {
                    return -1;
                }
            }
        }
    }
    class Program
    {
        static public Bank[] Input() //читаем данные из файла
        {
            using (StreamReader fileIn = new StreamReader("input.txt"))
            {
                int n = int.Parse(fileIn.ReadLine());
                Bank[] ar = new Bank[n];
                for (int i = 0; i < n; i++)
                {
                    string[] text = fileIn.ReadLine().Split(',');
                    ar[i] = new Bank(text[0], int.Parse(text[1]), int.Parse(text[2]), text[3]);
                }
                return ar;
            }
        }
        static void Print(Bank[] array) //выводим данные на экран
        {
            foreach (Bank item in array)
            {
                item.Show();
            }
        }
        static void Main()
        {
            Bank[] array = Input();
            List<Bank> list = new List<Bank>();
            
            foreach (Bank item in array)
            {
                if (item.year.StartsWith("2024"))
                {
                    list.Add(item);
                }
            }
            list.Sort();    
            using (StreamWriter fileOut = new StreamWriter("out.txt", false))
            {
                foreach (Bank item in list ){
                    fileOut.WriteLine(item.Show());
                }
                //читаем построчно до тех пор, пока поток fileIn не пуст
                
                
            }
            
        }
    }
}