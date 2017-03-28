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
        static IPAddress subnetmaszk(int suffix)
        {
            int a = 0;
            // for (int i = 0; i < suffix; i++)
                // a += Convert.ToUInt32(Math.Pow(2, 31 - i));
            
            byte[] subnetmask = BitConverter.GetBytes(a);
            Array.Reverse(subnetmask);
            return new IPAddress(subnetmask);

        }

        static void Main(string[] args)
        {
            IPAddress ip;
            string ipstring;
            Int16 subnetmask;


            while(!false)
            {
                Console.WriteLine("Loop");
                ipstring = Console.ReadLine().Trim();
                if( ipstring == "exit" || ipstring == "break" ) break;

                if (IPAddress.TryParse(ipstring, out ip))
                    ip = IPAddress.Parse(ipstring);
                else continue;

                subnetmask = Convert.ToInt16(Console.ReadLine().Trim());

                //> IPAddr -> int
                byte[] bytes = ip.GetAddressBytes();
                Array.Reverse(bytes);
                uint intAddress = BitConverter.ToUInt32(bytes, 0);
            }
        }
    }
}
