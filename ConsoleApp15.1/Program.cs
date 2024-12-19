using System;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;


class Program
{
    static void Main()
    {
        using (StreamReader fileIn = new StreamReader("input.txt"))
        {
            string[] text = fileIn.ReadLine().Split(' ');
            List<double> number = new List<double>();
            foreach (string line in text)
            {
                number.Add(double.Parse(line));
            }
            Console.Write("Bigger than ");
            double low = double.Parse(Console.ReadLine());
            var nums = from n in number
                       where(n > low)
                       select n;
                       
            foreach (var x in nums.Select(n => n / 2))
            {
                Console.Write("{0} ", x);
            }
            

        }
    }
}