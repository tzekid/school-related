using System;
using System.Text;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Write("Input something, dawg: ");
            
            string dick = Console.ReadLine();
            byte[] bytes = Encoding.ASCII.GetBytes(dick);
            
            string butt = "0";
            foreach(var x in bytes)
                butt += "0" + Convert.ToString(x, 2) + "01";
            butt += "1";

            Console.WriteLine("Your input was: " + butt);
        }
    }
}


/* output e.g.

Input something, dawg: Snappy crap
Your input was: 0010100110101101110010110000101011100000101110000010111100101010000001011000110101110010010110000
10101110000011

 */