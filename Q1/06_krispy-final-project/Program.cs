using System;
using System.Linq;
using System.Collections.Generic;

// using System.Text.RegularExpressions; 
// Habe zuerst versucht den Input zu filtrieren, aber in C# regexpr sind zu umstaenig für mein Geschmack gestaltet */  


namespace ConsoleApplication {
  public class Krispy {

    // Token
    // @string  tags    ( 'NUM' | 'SYM' | 'LPAR' | 'RPAR' | 'ERR' | 'NULL' )
    // @string  data      input
    public struct Token {
      public string tag
                  , data;

      public Token(string tag, string data) {
        this.tag = tag;
        this.data = data;
      }

      // e.g. Token(  SYM, "+")
      public override string ToString() { 
        return String.Format("=> Token({0, 6}, \"{1}\")", tag, data);
      }
    }

    
    /**  Am Anfang wollte ich Node als eine Abstrakte Klasse darstellen,
     * aber es war mir zu umstaendig.
     *  Im gegensatz zu Token, Node ist eine Klasse da nicht alle Variabeln
     * werden in eine Instanz besetzt , zB. ein `Number` braucht kein `parent`.
     */
     
    // Node
    // @string  type   ( 'EXPR' | 'NUM' | 'NULL' )
    // @string  data    
    // @Node    parent   
    // @int     pos      at index of this.parent.children
    public class Node {
      public string type = "",
                    data = "";
      
      /* EXPR haben NUM und andere EXPR als `children` */
      public List<Node> children = new List<Node>();
      public void addChild(Node child) {  children.Add(child); } 

      /* Zeigt die höhere Ebene an und an welche Stelle sich der Node da befindet. */  
      public Node parent;
      public int pos;


      public Node(string type, string data) {
        // this.pos = -1;
        this.type = type;
        this.data = data;
      }
    }


    /* Der Lexer wandelt den Input in Tokens */
    // Lexer
    // @string  source   input
    // #string  symbols ( '+' | '-' | '/' | '*' )
    // @<Token>  tokens   output
    public class Lexer {
      public List<Token> tokens;
      static List<string> symbols =
        new List<string>( new string[] {"+", "-", "/", "*"} );

      /* bestimme ob den String ein Symbol enthält */
      static bool isSymbol(string isym) {
        foreach (string sym in symbols)
          if (sym == isym) return true;
        return false;
      }

      public void lex(string source) {
        /* Initializiere tokens (erneut, um die vorigen zu löschen). */
        tokens = new List<Token>();

        /* wandle den Input in eine Liste */
        List<string> words = source.Split().ToList();
        /* test_if_num wird später gebraucht, um zu sehen ob ein String als Double zergliedert werden kann */
        double test_if_num;

        /* gehe durch die Wörter schaue ob du Symbole, Nummern oder Klammer findest und erstelle ein passenden Token dazu */ 
        foreach (string word in words) {
          if (word == "" || word == " " || word == "\r")       continue;   //TODO(kid): REGEXP IF NOT NUMBER, SYMBOL OR LETTER
          else if (isSymbol(word))                             tokens.Add( new Token("SYM", word) );
          else if (double.TryParse(word, out test_if_num))     tokens.Add( new Token("NUM", word) );
          else if (word == "(")                                tokens.Add( new Token("LPAR", "(") );
          else if (word == ")")                                tokens.Add( new Token("RPAR", ")") );
          
          /* wenn der Wort nicht erkannt wird, lösche alle vorigen Tokens und zeige ein ERR Token m/ den unerkannten Wort */
          else {
            tokens.Clear();
            tokens.Add( new Token("ERR", word) );
            Console.WriteLine(tokens[0]);
            return;
          }
        }
      }

      public void printTokens() {
        foreach (Token token in tokens)
          Console.WriteLine(token);
      }

      public Lexer(string source = "") { this.lex(source); }
    }


    /**  Der Parser nimmt die Tokens von ein Lexer und bildet einen
     * AST (Abstract Structure Tree), der die Logik der Input darstelt(/-en soll).
     */
     
    // Parser
    // @Node ROOT    NODE( type: EXPR | NUM )
    public class Parser {
      public Node ROOT;


      public void parse(Lexer lex) {
        ROOT = new Node("NULL", "NULL");
        
        int pos = 0; // @ index <- currBranch

        /* Temporäre Variabeln die für die AST Logik gebraucht wurden */ 
        Node tempBranch = new Node("NULL", "NULL");;
        Node currBranch = ROOT;

        foreach (Token token in lex.tokens) {
          switch (token.tag) {
          
          /*  Eine "Klammer Auf" gekennzeichnet der Anfang einen EXPR */
          /*  Erstelle eine neue Node in ein niedriegere (abstrakte) Stuffe,
           * und definiere dessen Standpunkt in den AST.
           */
          case "LPAR":
            if (ROOT.type == "NULL") break;

            tempBranch = currBranch;
            currBranch = new Node("NULL", "NULL");
            
            /*  Definiere die Stelle des Nodes relativ
             * zu den höheren und gleich Stufigen Nodes.
             */
            currBranch.parent = tempBranch;
            currBranch.pos = pos;
            tempBranch.addChild( currBranch );

            /* Fange vom Anfang der neue Ebene */
            pos = 0;
            break;

          /* Stelle den logischen Typ des EXPRs fest */
          case "SYM":
            currBranch.type = "EXPR";
            currBranch.data = token.data;
            break;
          
          /* Füge den Zahl aus den Token hinzu und 
           * betrachte die Position auf die jetzige Ebene. */
          case "NUM":
            currBranch.addChild( new Node(token.tag, token.data) ); pos++; break;
          
          /* Steige eine Ebene höher, eine Stelle nach den EXPR */
          case "RPAR":
            if (currBranch.parent == null) break;
            
            currBranch = currBranch.parent;
            pos = currBranch.pos + 1;
            break;

          default: Console.WriteLine("ParseErr@{0}", pos); break;  } // ERR handling
        }
      }
    }


    /** Eval nimmt ein AST und evaluiert den von den niedriegste Ebene.
     *  Nach den Evaluierung einer Ebene, den EXPR wird durch den Resultat 
     * dessen ersetzt, und die höhere Ebene evaluiert.
     */
    // Eval
    // @return string   Result
    public class Eval {
      
      /* Evaluiere die zwei Zahlen gemäß den eingegebenen Symbol */ 
      public string expr(string sym, string a, string b) {
        /* Wandle die Strings zu Doubles */
        double a_, b_;
        double.TryParse(a, out a_);
        double.TryParse(b, out b_);

        if (sym == "+") return (a_ + b_).ToString();
        if (sym == "-") return (a_ - b_).ToString();
        if (sym == "*") return (a_ * b_).ToString();
        if (sym == "/") {
          if (b_ != 0)  return (a_ / b_).ToString();
          else          return "";
        }
        return "";
      }

      public string eval(Node node) {
        /* Temp Wert */
        Node branch = new Node("NULL", "NULL");
          
        /* Gehe in die tiefste Ebene, wo kein EXPR mehr zu finden ist. */
        for (int i = 0; i < node.children.Count(); i++)
          branch = node.children[i];
          
          Console.WriteLine("Branch POS is " + branch.pos);

          if (branch.type == "EXPR") {

            /* Evaluiere die Ebene */
            string result = eval(branch);

            Console.WriteLine(branch.pos);

            /* Ersetze den EXPR mit den Resultat */
            node.children.Insert(branch.pos, new Node("NUM", result));
          }

        /* Temp Wert */
        string tempdata = "";

        /* Wenn kein EXPR mehr auf die Ebene zu finden ist, evaluiere es. */
        while (true) {
          /* Wenn nur ein Argument vorhanden ist `return`e es, und wenn keine vorhanden ist, breche ab */
          if (node.children.Count() == 0) { Console.WriteLine("broke"); break; };
          if (node.children.Count() == 1) { Console.WriteLine("rtrnd"); return node.children[0].data; }

          /* Bereite die Argumenten für `expr()` und lösche die Nodes. */
          string sym = node.data;

          string a = node.children[0].data;
          node.children.RemoveAt(0);

          string b = node.children[0].data;
          node.children.RemoveAt(0);
          
          /* Ersetze die vorigen Zahlen mit die Ergebniss der Evaluirung */
          tempdata = expr(node.data, a, b);
          node.children.Insert(0, new Node("NUM", tempdata));
        }

        return tempdata;
      }

      /* "Visuelle" Darstellung von der AST */
      public void printTREE(Node node, string level = "") {
        Console.WriteLine(level+"Node("+node.type+", "+node.data+", "+node.pos+")");
        foreach (Node branch in node.children) {
          if (branch.type == "EXPR")
            printTREE(branch, level + "->");
          else
            Console.WriteLine(level+">Node("+branch.type+", "+branch.data+")");
        }
      }
    }

/* Zur funktioniert die Evaluation nicht wie ich es mir vorgennomen hatte */
// ( + 2 3 ( * 4 ( - 6 5 ) 7 ) 9 )    <-   Wird nicht richtig evaluiert
// ( + 2 3 9 ( * 4 7 ( - 6 5 ) ) )    <-   Wird auch nicht richtig evaluiert.
// ( + 2 3 9 ( * 4 7 ) )              <-   Ist OK

    /* Prompt soll als UI dienen */
    public void Prompt() {
      string input;
      Lexer  lex  = new Lexer();
      Parser par  = new Parser();
      Eval   eval = new Eval();

      Console.WriteLine("Welcome to Krispy v0.0.0.0.0.0.0.0)");
      while (true) {
        Console.Write("Krispy> ");
        input = Console.ReadLine();

        if (input == "exit")            break;
        else if ( input == "r")         lex.printTokens();
        else if ( input == "p")         par.parse(lex);
        else if ( input == "e")         eval.printTREE(par.ROOT);
        else {
          // Input -> Tokens
          lex.lex(input);
          lex.printTokens();          
          
          // Tokens -> AST
          par.parse(lex);
          
          Console.WriteLine();
          eval.printTREE(par.ROOT);
          
          // AST -> Output
          Console.WriteLine();
          Console.WriteLine(eval.eval(par.ROOT));
        }
      }
    }

    // Init. der Klasse
    public Krispy() {
      Prompt();
    }
  }

  public class Program {
    public static void Main(string[] args) {
      Krispy dick = new Krispy();
    }
  }
}