using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace DNZ
{
    class Program
    {
        int convertToInt(string input)
        {
            IPAddress ipAddrz;
            List<int> calculate;
            if (IPAddress.TryParse(input, out ipAddrz))
            {
                foreach (var x in input.Split('.'))
                    Console.WriteLine(x);
            } else return -1;
            return -1;
        }

        static void Main(string[] args)
        {
            string input;
            IPAddress ipadress;
            while (!false)
            {
                try
                {
                    input = Console.ReadLine().Trim();

                    if(input == "exit" || input == "break") break;

                    if (IPAddress.TryParse(input, out ipadress))
                        ipadress = IPAddress.Parse(input);
                    else
                    {
                        Console.WriteLine("Shit's wrong ...");
                        continue;
                    }

                    Console.WriteLine("plz enterz an subnetmaskz");

                    byte[] bytez = ipadress.GetAddressBytes();
                    Array.Reverse(bytez);
                    UInt32 intAddrz = BitConverter.ToUInt32(bytez, 0);


                } catch { Console.WriteLine("Something failed"); break; }
            }
        }
    }
}
