using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApplication {
    public class Program {
        public static void Main(string[] args) {
            string pinglog = "./pinglog";
            string iplog = "./iplog";
            

            using (StreamReader sf = File.OpenText(iplog)) {
            using (StreamReader fs = File.OpenText(pinglog)) {
                string line = String.Empty;
                string host = "";
                string inet = "";

                bool hinp = false;

                while ((line = sf.ReadLine()) != null) {
                    if (line.Contains("host"))
                        host = line.Trim().Split(' ')[1];
                    
                    if (line.Contains("inet "))
                        inet = line.Trim();
                }

                while ((line = fs.ReadLine()) != null)
                    if (line.Contains(host))
                        hinp = true;

                if (!hinp) Environment.Exit(-1);

                Console.WriteLine(hinp.ToString());
                Console.WriteLine(host);
                Console.WriteLine(inet);

                string[] idoh = inet.Split(' ')[1].Split('.');
                string[] nmsk = inet.Split(' ')[4].Split('.');
                string[] bcst = inet.Split(' ')[7].Split('.');

                byte[] ydoh = new byte[4];
                byte[] mnsk = new byte[4];
                byte[] cbst = new byte[4];

                for (int i = 0; i < idoh.Length; i++)
                    ydoh[i] = Convert.ToByte(Convert.ToInt16(idoh[i]));

                for (int i = 0; i < nmsk.Length; i++)
                    mnsk[i] = Convert.ToByte(Convert.ToInt16(nmsk[i]));

                for (int i = 0; i < bcst.Length; i++)
                    cbst[i] = Convert.ToByte(Convert.ToInt16(bcst[i]));


                Console.Write("\nidoh = ");
                foreach (string dick in idoh) Console.Write(dick + " ");
                
                Console.Write("\nnmsk = ");
                foreach (string dick in nmsk) Console.Write(dick + " ");
                
                Console.Write("\nbcst = ");
                foreach (string dick in bcst) Console.Write(dick + " ");
                Console.WriteLine();


                byte[] butt = new byte[4];
                
                for (int i = 0; i < 4; i++)
                    butt[i] = (byte)((byte)(ydoh[i] & mnsk[i]) & cbst[i]);


                Console.WriteLine();
                Console.Write("Netwerk: ");
                foreach (var dick in butt) Console.Write(dick + " ");
                Console.WriteLine();
                
            }}
        }
    }
}
