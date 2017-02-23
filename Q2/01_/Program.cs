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
