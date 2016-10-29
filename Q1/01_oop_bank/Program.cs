using System;

namespace _oop_bank {
    class MainClass {

        public struct Konto {
            public string kontoNr;
            public double kontostand;
            public Kunde inhaber;

            public Konto(string kN, double ks, Kunde i) {
                this.kontoNr = kN;
                this.kontostand = ks;
                this.inhaber = i;
            }
        }

        public class Kunde {
            public string kundenNr;
            public string namen;
            public Konto konto;
            public bool gyro;

            // @param kN 			Konto Nr
            // @param n 			Name
            // @param g 			Gyro Konto
            public Kunde(string kN, string n, bool g) {
                this.kundenNr = kN;
                this.namen = n;
                this.konto = new Konto(kN, 0, this);
                this.gyro = g;
            }

            public double Kontostand() {
            	return this.konto.kontostand;
            }

            public void einzahlen(double butt) { 
                this.konto.kontostand += butt;
            }

            public bool abheben(double bar) {
            	if (this.gyro) {
	                if (this.konto.kontostand >= bar && bar > 0) {
	                    this.konto.kontostand -= bar;
	                    return true;
	                } else { return false; }
	            } else { 
	            	this.konto.kontostand -= bar;
	            	return true;
	            }
            }

            public bool uberweisen(double fig, Kunde aro) {
                if (this.abheben(fig)) {
                    aro.einzahlen(fig);
                    return true;
                } else { return false; }
            }
        }

        public static void Main (string[] args) {
            Kunde dick = new Kunde("000", "Dick Johnson", false);
            Kunde butt = new Kunde("001", "Jollie Butt", true);

            dick.einzahlen(500.20);
            dick.uberweisen(200, butt);
            butt.abheben(500);
            
            Console.WriteLine(dick.Kontostand());
            Console.WriteLine(butt.Kontostand());


        }
    }
}
