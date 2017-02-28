using System;
using System.Text;
using System.Collections.Generic;

namespace ConsoleApplication {
    public class Program {
        public static void Main(string[] args) {
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
                if (x == " ") continue;
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

/* output e.g.

Converting the message: <SYN><SYN><STX><Hier kommt Nachricht1><ETX><STX><Hier kommt Nachricht 2><ETX>...<EOT>

Converting  to
Converting SYN with 0010110
Converting  to
Converting SYN with 0010110
Converting  to
Converting STX with 0000010
Converting  to
Converting Hier kommt Nachricht1 to 01001000010110100101011001010101110010010100000010110101101011011110101101101
01011011010101110100010100000010100111001011000010101100011010110100001011100100101101001010110001101011010000101
11010001011000101
Converting  to
Converting ETX with 0000011
Converting  to
Converting STX with 0000010
Converting  to
Converting Hier kommt Nachricht 2 to 0100100001011010010101100101010111001001010000001011010110101101111010110110
10101101101010111010001010000001010011100101100001010110001101011010000101110010010110100101011000110101101000010
111010001010000001011001001
Converting  to
Converting ETX with 0000011
Converting ... to 010111001010111001010111001
Converting EOT with 0000100
Converting  to

The output is:
00010110010001011001000000100101001000010110100101011001010101110010010100000010110101101011011110101101101010110
11010101110100010100000010100111001011000010101100011010110100001011100100101101001010110001101011010000101110100
01011000101000000110100000010010100100001011010010101100101010111001001010000001011010110101101111010110110101011
01101010111010001010000001010011100101100001010110001101011010000101110010010110100101011000110101101000010111010
00101000000101100100100000011010101110010101110010101110010000010001

 */