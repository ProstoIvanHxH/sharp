using System;
using System.IO;
using System.Runtime.ConstrainedExecution;
using System.Text;
using static System.Net.WebRequestMethods;
namespace MyProgram
{
    class Program
    {
        static void Main()
        {
            using (StreamReader file1 = new StreamReader("input1.txt"))
            {
                using (StreamReader file2 = new StreamReader("input2.txt"))
                {
                    using (StreamWriter fileOut = new StreamWriter("out.txt", false))
                    {

                        int[] line1 = file1.ReadLine().Split(' ').Select(int.Parse).ToArray();
                        int[] line2 = file2.ReadLine().Split(' ').Select(int.Parse).ToArray();
                        foreach (int i in line1)
                        {
                            if (i % 2 == 0)
                            {
                                fileOut.Write(i + " ");
                            }
                        }
                        foreach (int i in line2)
                        {
                            if (i % 2 != 0)
                            {
                                fileOut.Write(i + " ");
                            }
                        }

                    }


                }
            }

        }
    }
}