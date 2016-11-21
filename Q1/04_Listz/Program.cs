using System;
using System.Collections.Generic;
using DataTypes;

/*** Listz *** !!!
/** Teh code was done 
 * in a rush in T minus 30 minutes.
 * Most of the code looks functional.
 * The looks can be deceiving.
 */

namespace ConsoleApplication {
    public class Interface {
        char[] symbols = {'λ', '>'};

        public void start() {
            DataTypes.Types Types = new DataTypes.Types();

            Dictionary<string, Types.LinkedList> listz = new Dictionary<string, Types.LinkedList>();
            Dictionary<string, Types.Node> nodez = new Dictionary<string, Types.Node>();

            Console.WriteLine("Welcum ot Listz!\n");

            while (true) {
                Console.Write(symbols[0] + " ");
                string input = Console.ReadLine();
                string[] inputs = input.Split(' ');

                if (input == "help") {
                    Console.WriteLine("Nothing here.");
                } else if (input == "quit" || input == "exit") break;

                else if (inputs[0] == "create") {
                    try {
                        if (inputs[1] == "Node") {
                            if (inputs[2] != "") {
                                nodez.Add(inputs[2], new Types.Node());
                                nodez[inputs[2]].value = inputs[2];
                                Console.WriteLine("Teh node `"+ inputs[2] +"` was created.");
                            } else Console.WriteLine("usage: create {Node / List} {name}");
                        } else if (inputs[1] == "List") {
                            if (inputs[2] != "") {
                                listz.Add(inputs[2], new Types.LinkedList());
                                Console.WriteLine("Teh list `"+ inputs[2] +"` was created.");
                            } else Console.WriteLine("usage: create {Node / List} {name}");
                        } else Console.WriteLine("usage: create {Node / List} {name}");
                    } catch { Console.WriteLine("usage: create {Node / List} {name}"); }
                }

                else if (inputs[0] == "assign") {
                    foreach (var node in nodez) {
                        if (inputs[1] == node.Key) {
                            try { node.Value.value = inputs[2];
                                  Console.WriteLine("`"+ node.Key +"` now has the value `"+ inputs[2] +"`"); }
                            catch { Console.WriteLine("usage: assign {node} {value}"); }
                        } else Console.WriteLine("usage: assign {node} {value}");
                    }
                }

                else if (inputs[0] == "add") {
                    if (inputs[1] == "all") {
                        if (listz.ContainsKey(inputs[2]))
                            foreach (var node in nodez) listz[inputs[2]]._add(nodez[node.Key]);
                        else Console.WriteLine("usage: add all {list}");
                    } else {
                    try {
                        if (nodez.ContainsKey(inputs[1])) {
                            if (inputs[2] == "to") {
                                if (listz.ContainsKey(inputs[3]))
                                    listz[inputs[3]].add(nodez[inputs[1]]);
                                else { Console.WriteLine("usage: add {Node} to {List}"); }
                            } else Console.WriteLine("usage: add {Node} to {List}");
                        } else { Console.WriteLine("usage: add {Node} to {List}"); }
                    } catch { Console.WriteLine("usage: add {Node} to {List}"); } }
                }

                else if (inputs[0] == "remove") {
                    try {
                        if (nodez.ContainsKey(inputs[1])) {
                            if (inputs[2] == "from") {
                                if (listz.ContainsKey(inputs[3]))
                                    listz[inputs[3]].remove(nodez[inputs[1]]);
                                else Console.WriteLine("usage: remove Node from List");
                            } else Console.WriteLine("usage: remove Node from List");
                        } else Console.WriteLine("usage: remove Node from List");
                    } catch { Console.WriteLine("usage: remove Node from List"); }
                }

                foreach (var node in nodez) {
                    if (inputs[0] == node.Key) Console.WriteLine("Node `"+ node.Key +"` holding the value `"+ node.Value +"`");
                }

                foreach (var list in listz) {
                    if (inputs[0] == list.Key && inputs.Length == 1) Console.WriteLine(listz[list.Key]);
                    else if (inputs[0] == list.Key && inputs.Length != 1) {
                        try {
                            if (inputs[1] == "changeAtPos") {
                                if (Convert.ToInt32(inputs[2]) > 0) {
                                    if (inputs[3] == "with") {
                                        if (nodez.ContainsKey(inputs[4]))
                                            listz[list.Key].changeAtPos(Convert.ToInt32(inputs[1]), nodez[inputs[4]]);
                                        else Console.WriteLine(listz[list.Key] + "\nusage: {list} (`changeAtPos`)");
                                    } else Console.WriteLine(listz[list.Key] + "\nusage: {list} (`changeAtPos`)");
                                } else Console.WriteLine(listz[list.Key] + "\nusage: {list} (`changeAtPos`)");
                            } // else Console.WriteLine(listz[list.Key] + "\nusage: {list} (`changeAtPos`)");
                        } catch { Console.WriteLine(listz[list.Key] + "\nusage: {list} (`changeAtPos`)"); }
                    }
                }
            }
        }
    }

    public class Program {
        Types Types = new DataTypes.Types();

        public static void Main(string[] args) {
            Interface cli = new Interface();

            cli.start();
        }
    }
}
