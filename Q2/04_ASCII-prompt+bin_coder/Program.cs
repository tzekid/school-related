using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace ConsoleApplication {
    public class Program {


        static public List<string> codes =
            new List<string>() { "STX", "ETX", "SYN" };

        // (even/odd affirmative acknowledgement)
        // 1: DLE before every ETB, ETX or SYN
        // 2: DLE DLE at the beg. and end of package
        static public int getTrsp() {
            while(!false) {
                Console.Write("Do you want to use Transparency method one or two ? (1/2) ");
                string input = Console.ReadLine().Trim();

                if (input == "1" || input == "2")   return Convert.ToInt32(input);
                else                                Console.WriteLine("Enter a valid input");
            }
        }

        static public int syntaxCheck(string crap)
        {
            int oRepeats = 0;
            int cRepeats = 0;

            if(crap[0] == '>')                     return -1;
            if(crap[0] == '<')
            {
                while(oRepeats < 2 && cRepeats < 2)
                {
                    foreach(var x in crap)
                    {
                        {
                            if (x == '<')
                            {
                                oRepeats += 1;
                                cRepeats  = 0;
                            }
                            if (x == '>')
                            {
                                oRepeats  = 0;
                                cRepeats += 1;
                            }
                        }
                    } break;
                }
                return (oRepeats < 2 || oRepeats < 2)   ?  1
                                                        : -1;
            }
            else                                    return 0;
        }

        static public string fuck(int transp = -1) {
            if (transp != 1 || transp != 2)     transp = getTrsp();

            string ASCII_string = transp == 1 ? "<DLE><DLE>" : "";  
            string intake;

loahp:      while(!false) {
                Console.Write("Enter something here dawg: ");
                intake = Console.ReadLine().Trim();

                if (intake == "" || intake == "continue"  || intake == "done")  break;
                if (intake == "break" || intake == "exit")                      Environment.Exit(1);

                     if (syntaxCheck(intake) == -1)  Console.WriteLine("Sytax error");
                else if (syntaxCheck(intake) ==  1)
                {
                    List<string> dox = new List<string>(
                        intake.Split('<', '>').Where(s => !string.IsNullOrWhiteSpace(s)).ToList() );

                    string newString = "";
                    
                    foreach (var x in dox) {
                        if (!codes.Contains(x))
                        {
                            Console.WriteLine("You cannot enter {0}", x);
                            goto loahp; // there is not `break outer` in C# ... this fucking language, man 
                        }

                        // Version 2
                        newString += transp == 1
                        ? "<" + x + ">"
                        : "<DLE><" + x + ">";
                    }
                    
                    ASCII_string += newString;

                    // ASCII_string += transp == 1
                    // ? intake
                    // : "<DLE><STX>" + intake + "<DLE><ETX>";
                }

                else
                {
                    ASCII_string += transp == 1
                    ? "<STX><" + intake + "><ETX>"
                    : "<DLE><STX><" + intake + "><DLE><ETX>";
                }
            }

            return transp == 1
            ? ASCII_string + "<DLE><DLE><EOT>"
            : ASCII_string + "<EOT>";
        }

        public static void Main(string[] args) {
            Dictionary<string, int> dick =
                new Dictionary<string, int>() {{"STX", 2}, {"ETX", 3}, {"DLE", 20}, {"SYN", 22}, {"EOT", 4} };
            
            // Console.Write("Enter a message: ");
            // string butt = Console.ReadLine();
            
            // string butt = "<SYN><SYN><STX><Hier kommt Nachricht1><ETX><STX><Hier kommt Nachricht 2><ETX>...<EOT>";
            string butt = fuck();
            Console.WriteLine("Converting the message: " + butt);
            Console.WriteLine();            

            List<string> hoe = new List<string>( butt.Trim().Split('<', '>').Where(s => !string.IsNullOrWhiteSpace(s)).ToList() );

            string ascii = "";

            foreach (var x in hoe) {
                if (x == " ") continue;
                if (dick.ContainsKey(x)) {
                    Console.WriteLine("Converting {0} with {1}",
                                                    x, Convert.ToString(dick[x], 2).PadLeft(7, '0'));
                    ascii += "0" + Convert.ToString(dick[x], 2).PadLeft(7, '0') + "01";
                }
                
                else if ( Encoding.ASCII.GetString(Encoding.ASCII.GetBytes(x)) == x ) {
                    string message = "";
                    foreach (var y in Encoding.ASCII.GetBytes(x))
                        message += "0" + Convert.ToString(y, 2) + "01";
                    
                    Console.WriteLine("Converting '{0}' to {1}", x, message);
                    ascii += message;
                }
                    // ascii += x;
                
                else {
                    Console.WriteLine("DIIIIICK!");
                    break;
                }
            }

            Console.WriteLine();
            Console.WriteLine("The original was: {0}", butt);
            Console.WriteLine("The output is:\n{0}", ascii);
        }
    }
}


/* output e.g.

Do you want to use Transparency method one or two ? (1/2) 1                                                           
Enter something here dawg: <SYN><SYN><STX><STX>                                                                       
Enter something here dawg: Hahahaha                                                                                   
Enter something here dawg: <ETX><ETX><SYN>                                                                            
Enter something here dawg:                                                                                            
Converting the message: <DLE><DLE><SYN><SYN><STX><STX><STX><Hahahaha><ETX><ETX><ETX><SYN><DLE><DLE><EOT>              
                                                                                                                      
Converting DLE with 0010100                                                                                           
Converting DLE with 0010100                                                                                           
Converting SYN with 0010110                                                                                           
Converting SYN with 0010110                                                                                           
Converting STX with 0000010                                                                                           
Converting STX with 0000010                                                                                           
Converting STX with 0000010                                                                                           
Converting 'Hahahaha' to 01001000010110000101011010000101100001010110100001011000010101101000010110000101             
Converting ETX with 0000011                                                                                           
Converting ETX with 0000011                                                                                           
Converting ETX with 0000011                                                                                           
Converting SYN with 0010110                                                                                           
Converting DLE with 0010100                                                                                           
Converting DLE with 0010100                                                                                           
Converting EOT with 0000100                                                                                           
                                                                                                                      
The original was: <DLE><DLE><SYN><SYN><STX><STX><STX><Hahahaha><ETX><ETX><ETX><SYN><DLE><DLE><EOT>                    
The output is:                                                                                                        
0001010001000101000100010110010001011001000000100100000010010000001001010010000101100001010110100001011000010101101000
010110000101011010000101100001010000001101000000110100000011010001011001000101000100010100010000010001     

*/