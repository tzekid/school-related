using System;
using System.Linq;
using System.Collections.Generic;

namespace ConsoleApplication {
    public class Program {

        static public List<string> codes =
            new List<string>() { "STX", "ETX", "SYN" };

        // (even/odd affirmative acknowledgement)
        // 1: DLE STX before every ETB, ETX or SYN and DLE ETX after
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

        static public string dick(int transp = -1) {
            if (transp != 1 || transp != 2)     transp = getTrsp();

            string ASCII_string = transp == 1 ? "<DLE><DLE>" : "";  
            string intake;

loahp:      while(!false) {
                Console.Write("Enter something here dawg: ");
                intake = Console.ReadLine().Trim();

                if (intake == "" || intake == "continue" || intake == "break" || intake == "exit" || intake == "done") break;

                     if (syntaxCheck(intake) == -1)  Console.WriteLine("Sytax error");
                else if (syntaxCheck(intake) ==  1)
                {
                    List<string> dox = new List<string>(
                        intake.Split('<', '>').Where(s => !string.IsNullOrWhiteSpace(s)).ToList() );
                    
                    foreach (var x in dox)
                        if (!codes.Contains(x))
                        {
                            Console.WriteLine("You cannot enter {0}", x);
                            goto loahp; // there is not `break outer` in C# ... this fucking language, man 
                        }
                    
                    ASCII_string += transp == 1
                    ? intake
                    : "<DLE><STX>" + intake + "<DLE><ETX>";
                }

                else
                {
                    ASCII_string += transp == 1
                    ? "<STX><" + intake + "><ETX>"
                    : "<DLE><STX><STX><" + intake + "><ETX><DLE><ETX>";
                }
            }

            return transp == 1
            ? ASCII_string + "<DLE><DLE><EOT>"
            : ASCII_string + "<EOT>";
        }

        public static void Main(string[] args)
        {
            Console.WriteLine(dick());
        }
    }
}

// <SYN><SYN><STX><Hier kommt Nachricht1><ETX><STX><Hier kommt Nachricht 2><ETX>...<EOT>
// <DLE><DLE> ... <DLE><DLE>
// <DLE><stuff><DLE><DLE><stuff>
