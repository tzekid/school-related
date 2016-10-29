using System;

namespace ConsoleApplication {
  public class Program {
    public abstract class spellcaster {
      private string _name;
      public string name { get { return _name; } }
      public bool casts;
      public int level
                , nr;
      public double stamina,
            hp,
            maxHp,
            mana,
            magika_power;

      public spellcaster(string n, int l, double s, double hp, double m, double mp, bool c, int nr) {
        this._name = n;
        this.level = l;
        this.stamina = s;
        this.hp = hp;
        this.maxHp = this.hp;
        this.mana = m;
        this.magika_power = mp;
        this.casts = c;
        this.nr = nr;
      }

      public abstract void main_spell(spellcaster s);
      public abstract void secondary_spell(spellcaster s);

      public string naem() { return this.GetType().Name.ToString(); }
    }


    public class mage : spellcaster {
      public mage(string n, int l, double s, double hp, double m, double mp, bool c, int nr) :base(n, l, s, hp, m, mp, c, nr) {}

      public override void main_spell(spellcaster s) {
        s.hp -= magika_power;

        Console.WriteLine(this.name + " casts _Fireball_ to " + s.name
                        + " doing " + magika_power.ToString() + " damage.");
        Console.WriteLine(s.name + " now has " + s.hp + "hp left and is burning hot.\n");
      }

      public override void secondary_spell(spellcaster s) {
        s.hp -= magika_power / 2;

        Console.WriteLine(this.name + " casts _Frostbyte_ to " + s.name
                        + " doing " + (magika_power / 2).ToString() + " damage.");
        Console.WriteLine(s.name + " now has " + s.hp + "hp left and is cool and frosty.\n");
      }

      public override string ToString() { return this.naem() + "\n1.) Fireball\n2.) Frostbyte"; }
    }


    public class druid : spellcaster {
      public druid(string n, int l, double s, double hp, double m, double mp, bool c, int nr) :base(n, l, s, hp, m, mp, c, nr) {}

      public override void main_spell(spellcaster s) {
        s.hp += magika_power;
        if (s.hp > s.maxHp) s.hp = s.maxHp;

        Console.WriteLine(this.name + " casts _Heal_ to " + s.name + ", healing " + magika_power.ToString() + "hp.");
        Console.WriteLine(s.name + " now has " + s.hp + "hp left and is glowing like a xmas tree.\n");
      }

      public override void secondary_spell(spellcaster s) {
        s.hp -= magika_power / 4;
        Console.WriteLine(this.name + " casts _Thorns_ to " + s.name
                        + " dealing " + (magika_power / 4).ToString() + " damage.");
        Console.WriteLine(s.name + " now has " + s.hp + "hp left and has got spikes stuck in all places.\n");
      }
      public override string ToString() { return this.naem() + "\n1.) Heal\n2.) Throrns"; }
    }

    static int ctr = 0;
    static spellcaster set_up_playaz (out spellcaster playa) {
      string input = "";
      bool castz;
      ++ctr;
      if (ctr == 1) castz = true;
      else castz = false;

      setup:
        Console.Write("\nPlayer "+ ctr.ToString() +" (mage / druid): ");
        input = Console.ReadLine().Trim();

        if (input == "mage")
          playa = new mage("Dick", 0, 10, 200, 300, 150, castz, ctr);
        else if (input == "druid")
          playa = new druid("Butt Nr. " + ctr, 0, 10, 250, 200, 100, castz, ctr);
        else {
          Console.WriteLine("Please enter either (mage / druid) !");
          goto setup;
        }

      return playa;
    }

    static void battle (spellcaster a, spellcaster b) {
      string input;
      while (a.hp > 0 && b.hp > 0) {
        if (a.casts) {
          Console.WriteLine("\n\n\n" + a.ToString());
          Console.Write("\n> ");
          input = Console.ReadLine();

          switch (input) {
          case "1":
            if (a.naem() == "druid") a.main_spell(a);
            else a.main_spell(b);
            a.casts = !a.casts;
            break;
          case "2":
            a.main_spell(b);
            a.casts = !a.casts;
            break;
          default:
            Console.WriteLine("You suck");
            break;
          }
        } else if (!a.casts) {
          Console.WriteLine("\n\n\n" + b.ToString());
          Console.Write("\n> ");
          input = Console.ReadLine();

          switch (input) {
          case "1":
            if (b.naem() == "druid") b.main_spell(b);
            else b.main_spell(a);
            a.casts = !a.casts;
            break;
          case "2":
            b.main_spell(a);
            a.casts = !a.casts;
            break;
          default:
            Console.WriteLine("You suck.");
            break;
          }
        }
      }

      Console.WriteLine("Player " + (b.hp < 0 ? a.nr : b.nr) + " won.");
    }


    public static void Main(string[] args) {
      spellcaster a;
      spellcaster b;
      a = set_up_playaz(out a);
      b = set_up_playaz(out b);

      battle(a, b);

      // mage butt = new mage("Butt", 0, 10, 200, 300, 150);
      // druid dick = new druid("Dick", 0, 10, 250, 200, 100);

      // butt.main_spell(dick);
      // dick.main_spell(dick);
      // dick.secondary_spell(butt);
      // butt.secondary_spell(dick);
    }
  }
}
