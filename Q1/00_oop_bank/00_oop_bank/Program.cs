using System;

namespace _oop_bank {
  class MainClass {

    public struct Konto {

      // @param kN 			konto Nr
      // @param ks 			Kontostand
      // @param i 			Inhaber
      public Konto(int kN, double ks, Kunde i) {

      }
    }

    public class Kunde {

      // @param kN 			Konto Nr
      // @param n 			Name
      // @param K				Konto
      public Kunde(int kN, string n, Konto k) {

      }
    }

    public static void Main (string[] args) {
      Console.WriteLine ("Hi");
    }

  }
}
