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
            
            var nums = number.Where(n => n> 9 && n < 100).Select(n => n +5);
            foreach (var x in nums)
            {
                Console.Write("{0} ", x);
            }


        }
    }
}