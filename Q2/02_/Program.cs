using System;
using System.Text;
using System.Collections.Generic;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Dictionary<string, int> dick =
                new Dictionary<string, int>() {{"STX", 2}, {"ETX", 3}, {"DLE", 20}, {"SYN", 22}, {"EOT", 4} };
            
            // Console.Write("Enter a message: ");
            // string butt = Console.ReadLine();
            
            string butt = "<SYN><SYN><STX><Hier kommt Nachricht1><ETX><STX><Hier kommt Nachricht 2><ETX>...<EOT>";
            Console.WriteLine("Converting the message: " + butt);
            Console.WriteLine();            

            List<string> hoe = new List<string>( butt.Trim().Split('<', '>') );

            string ascii = "";

            foreach (var x in hoe) {
                if (dick.ContainsKey(x)) {
                    Console.WriteLine("Converting {0} with {1}",
                                                    x, Convert.ToString(dick[x], 2).PadLeft(7, '0'));
                    ascii += "0" + Convert.ToString(dick[x], 2).PadLeft(7, '0') + "01";
                }
                
                else if ( Encoding.ASCII.GetString(Encoding.ASCII.GetBytes(x)) == x) {
                    string message = "";
                    foreach (var y in Encoding.ASCII.GetBytes(x))
                        message += "0" + Convert.ToString(y, 2) + "01";
                    
                    Console.WriteLine("Converting {0} to {1}", x, message);
                    ascii += message;
                }
                    // ascii += x;
                
                else {
                    Console.WriteLine("DIIIIICK!");
                    break;
                }
            }

            Console.WriteLine();
            Console.WriteLine("The output is:\n{0}", ascii);
        }
    }
}
