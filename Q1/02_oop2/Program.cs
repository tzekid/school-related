using System;

namespace ConsoleApplication {
    public class Program {
        public class Fahrzeug {
            private int fahrzeugnummer;
            static int autoNummer = 0;
            private float leergewicht
                        , masseLadung
                        , _zulassigesGesamtgewicht;
            public float zulassigesGesamtgewicht {
                get { return _zulassigesGesamtgewicht; }
                set { _zulassigesGesamtgewicht = value; }
            }
            
            public Fahrzeug(float l ,float zG, float mL) {
                this.fahrzeugnummer = ++autoNummer;
                this.leergewicht = l;
                this.zulassigesGesamtgewicht = zG;
                this.masseLadung = mL;
            }

            public override string ToString() {
                return GetFahrzeugTyp()
                    + "\nFahrzeugnummer: " + fahrzeugnummer 
                    + "\nLeergewicht: " + leergewicht 
                    + "\nZulassigesGesamtgewicht: " + zulassigesGesamtgewicht 
                    + "\nMasseLadung: " + masseLadung;
            }

            public virtual void beladen(int ladung) {
                if ((leergewicht + masseLadung + ladung) < zulassigesGesamtgewicht)
                    masseLadung += ladung;
                else
                    Console.WriteLine("Das zulassigesGesamtgewicht wird durch diese beladung überschritten."
                                    + "\nSie haben " + masseLadung + " von " + zulassigesGesamtgewicht + " beladen.");
            }

            public string GetFahrzeugTyp() { return this.GetType().Name.ToString(); }
        }

        public class Kraftfahrzeug:Fahrzeug {
            private int hoechstgeschwindigkeit;
            private float leistung;

            public Kraftfahrzeug(float l, float zG, float mL, int h, float le) :base(l, zG, mL) {
                this.hoechstgeschwindigkeit = h;
                this.leistung = le;
            }

            public override string ToString() {
                return base.ToString()
                    + "\nHoechstgeschwindigkeit: " + hoechstgeschwindigkeit
                    + "\nLeistung: " + leistung;
            }

            public override void beladen(int ladung) {
                base.beladen(ladung);
            }
        }

        public class Motorrad:Kraftfahrzeug {
            public Motorrad(float l, float zG, float mL, int h, int le) :base(l, zG, mL, h, le) {}
        }

        public class Pkw:Kraftfahrzeug {
            private int anzahlSitzplaetze;

            public Pkw(float l, float zG, float mL, int h, int le, int ap) :base(l, zG, mL, le, ap) {
                this.anzahlSitzplaetze = ap;
            }

            public override string ToString() {
                return base.ToString()
                    + "AnzahlSitzplaetze: " + anzahlSitzplaetze;
            }
        }

        public class Fahrrad:Fahrzeug {
            private float _rahmenhoehe;
            public float rahmenhoehe {
                get { return _rahmenhoehe; }
                set { _rahmenhoehe = value; }
            }

            public Fahrrad(float l, float zG, float mL, float r) :base(l, zG, mL) {
                this.rahmenhoehe = r;
            }

            public override string ToString() {
                return base.ToString()
                    + "\nRahnenhoehe: " + rahmenhoehe;
            }
        }

        public static void Main(string[] args) {
            Console.WriteLine();

            Fahrzeug dick = new Fahrzeug(11, 120, 14);
            Console.WriteLine(dick+"\n");
            
            Fahrrad butt = new Fahrrad(11, 100, 13, 14);
            Console.WriteLine(butt);
            
            butt.beladen(2);
            Console.WriteLine();
            Console.WriteLine(butt);
        }
    }
}
