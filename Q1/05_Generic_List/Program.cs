using System;
using System.Collections.Generic;

namespace ConsoleApplication
{
    public class Program {
        public class VerketteteListe<T> where T : IComparable {
            public bool IsEqual<t>(t a, t b) { return EqualityComparer<t>.Default.Equals(a, b); }

            // Sollen Debug Texten angezeigt werden ?
            public bool debug = true;

            public string codeToString(int c) {
                if      (c ==  1) return "bigger than";
                else if (c ==  0) return "equal to";
                else if (c == -1) return "smaller than";
                else              return "you don' fucked up";
            }

            public class Node {
                public T data = default(T);
                public Node next = null;

                public Node(T t) { data = t; }
            }

            // Der Anfang der Liste heißt `ĥead`
            public Node head = new Node(default(T));
            
            // default value of T; can be used as a null placeholder / placeholder for empty values
            T defaultGenericValue = default(T);

            // Unsortierte, lineale Einfügen
            public void Add (T data) {
                /* Die Eingabe sollte Head werden, falls Head kein Wert besitzt. */ 
                if (this.IsEqual(head.data, defaultGenericValue)) {
                    this.head = new Node(data);
                    if (debug) Console.WriteLine(this.head.data + " was added as head.");
                }
                
                /* Ersetze den Knoten der nach Head kommt, falls es kein Wert besitzt. */
                else if (this.head.next == null) {
                    this.head.next = new Node(data);
                    if (debug) Console.WriteLine("Val next to head is " + data);
                }

                /* Sonst, füge den Wert am Ende der Liste. */
                else {
                    Node currNode = head.next;
                    do { currNode = currNode.next; } while (!this.IsEqual(currNode.next.data, defaultGenericValue));
                    currNode.next = new Node(data);
                }
            }

            // Unsortierte, rekursive Einfügen
            /* Die Eingabe sollte Head werden, falls kein Head gibt                 */
            /* , sonst benutze die 2. Variante von `Add_` mit Head als Argument    */            
            public void Add_ (T data) {
                if (this.IsEqual(this.head.data, defaultGenericValue)) this.head = new Node(data);
                else                                                   this.Add_(data, this.head);
            }

            /* Die Eingabe sollte in den nächsten Knoten gespeichert werden, falls es frei ist */
            /* , sonst rufe `Add_` mit den nächsten Knoten.                                   */
            public void Add_(T data, Node node) {
                if (node.next == null)  node.next = new Node(data);
                else                    Add_(data, node.next);
            }

            // Semi-iterative, sortierte add 
            public void add(T data) {
                if (debug) Console.WriteLine("\nLooking into " + data);
                Node tempNode = new Node(default(T));
                
                /* Die Eingabe sollte Head werden, falls Head kein Wert besitzt. */                 
                if (this.IsEqual(head.data, defaultGenericValue)) {
                    this.head = new Node(data);
                    if (debug) Console.WriteLine(this.head.data + " was added as head.");

                /* Ersetze den Knoten der nach Head kommt, falls es kein Wert besitzt. */
                } else if (this.head.next == null) {
                    
                    /* Falls die Eingabe größer als den Head ist, kommt es danach  */ 
                    if (this.head.data.CompareTo(data) == -1) { 
                        if (debug) Console.WriteLine("Val next to head is " + data);
                        this.head.next = new Node(data);}

                    /* , sonst ersetzt es Head und den Wert von Head kommt danach. */
                    else {
                        tempNode = this.head;
                        this.head = new Node(data);
                        this.head.next = tempNode;
                        if (debug) Console.WriteLine(this.head.data + " is now head."); }
                
                } else {
                    Node newNode  = new Node(data);                    
                    Node prevNode = new Node(defaultGenericValue);
                    Node currNode = this.head;
                    Node nextNode = this.head.next;


                    while (true) {
                        /* Schaue ob die Eingabe kleiner als `currNode` ist */
                        if (data.CompareTo(currNode.data) == -1) {

                            /* Falls `currNode` Head ist, ersetze es und verlinke  */  
                            if (currNode == this.head) {
                                if (debug) Console.WriteLine("Replaced the head " + this.head.data + " with " + data);
                                this.head = newNode;
                                this.head.next = currNode;
                                
                                this.add(currNode.data);
                                break; }
                            
                            /* Sonst verlinke den vorigen Knoten zu Eingabe     */
                            /* und die Eingabe zu den currNode.                 */
                            else {
                                prevNode.next = newNode;
                                prevNode.next.next = nextNode;
                                if (debug) Console.WriteLine("Replaced " + currNode.data + " with " + data);
                                
                                this.add(currNode.data);
                                break; } }
                            
                            /* Falls beide new- und currNode gleich sind, füge den              */
                            /* neuen Wert gleich nach currNode und verlinke es dementsprechend. */
                            else if (data.CompareTo(currNode.data) == 0) {
                                if (debug) Console.WriteLine("Equal");
                                currNode.next = newNode;
                                newNode.next = nextNode;
                                break; }
                        
                        /* Falls den Eingegebenen Wert größer als currNode ist,            */
                        /* füge es als sein Element hinzu und verlinke es dementsprechend. */
                        else if (data.CompareTo(currNode.data) == 1) {
                            prevNode = currNode;
                            currNode = nextNode;
                            nextNode = nextNode.next;
                            if (debug) Console.WriteLine(data + " is " 
                                        + codeToString(data.CompareTo(currNode.data))
                                        + " " + currNode.data); }
                        /* Edgecase */    
                        else { Console.WriteLine("Dude ... you broke teh Matrix"); break; }

                        /* Füge die Eingabe am Ende der Liste. */
                        if (data.CompareTo(currNode.data) != -1 && nextNode == null) {
                            if (debug) Console.WriteLine(data + " is now the last element.");
                            currNode.next = newNode; break; }
                    }
                }
            }
            
            // Entferne ein Knoten aus der Liste nach Node
            public void Remove(Node node) {
                Node prevNode = new Node(defaultGenericValue);
                Node currNode = this.head;
                Node nextNode = this.head.next;

                while (true) {
                    if (currNode == node) {
                        if (currNode == this.head) {
                            if (nextNode != null) this.head = nextNode;
                            else                  this.head = new Node(defaultGenericValue);
                        } else prevNode.next = nextNode;
                        break;
                    } else {
                        prevNode = currNode;
                        currNode = nextNode;
                        nextNode = nextNode.next;
                    }
                    
                    if (nextNode == null) break;
                }
            }

            // Iterative Entfernen
            /* Falls die Eingabe den Head entschpricht, ersetze Head mit den nächsten Wert in Liste */
            /* , sonst benutze die 2. Version von `Remove` mit Head als Argument.                   */
            public void Remove(T data) {
                if (data.CompareTo(head.data) == 0) head.next = head;
                else                                Remove(data, head);
            }

            public void Remove(T data, Node node) {
                Node prevNode = node;
                Node currNode = node.next;
                
                /* Falls man die Ende der Liste eingibt, breche ab. */ 
                if (currNode == null) { if (debug) Console.WriteLine(data + "was not found in your list."); return; } ;
                
                /* Iteriere durch die Liste, solange die Eingabe den currNode nicht einspricht. */
                while (!this.IsEqual(currNode.data, data)) {
                    prevNode = currNode;
                    currNode = currNode.next;
    
                    /* Falls die Ende der Liste erreicht wurde, breche ab. */ 
                    if (currNode == null) { if (debug) Console.WriteLine(data + " was not found in your list."); return; }
                };

                /* Entferne den letzten Element in der Liste */ 
                if (this.IsEqual(currNode.data, data) && currNode.next == null) prevNode.next = null;
                
                /* , sonst entferne alle verbindungen zu den Knoten. */
                else { prevNode.next = currNode.next; }
            }

            // Rekursives Entfernen
            public void Remove_(T data) {
                if (data.CompareTo(head.data) == 0) head.next = head;
                else                                this.Remove_(data, head);
            }

            public void Remove_(T data, Node node) {
                /* Falls null als nächstes vorkommt ist die Liste zu Ende. */
                if      (node.next == null) { if (debug) Console.WriteLine(data + "was not found in your list."); return;}
                
                /* Entferne alle verbindungen zu den gewüchten Knoten. */ 
                else if (data.CompareTo(node.next.data) == 0) { node.next = node.next.next; return;}
                
                /* Rufe Remove_ mit den nächsten Knoten */
                this.Remove_(data, node.next);
            }
            
            // Iteratives find.
            public Node find(T data) {
                Node tempNode = this.head;

                while (tempNode.next != null) {
                    if (IsEqual(tempNode.data, data)) return tempNode;
                    else                              tempNode = tempNode.next; 
                }

                if (debug) Console.WriteLine(data + " was not found in list.");
                return null;
            }

            // Rekursives Find
            public T Find(T data) {
                if (data.CompareTo(head.data) == 0) return data;
                else                           Find(data, head);
                
                return defaultGenericValue;
            }

            public T Find(T data, Node node) {
                // /* Gebe den Wert wieder, wenn du es Findest. */
                if (IsEqual(node.next.data, data)) return data;
                else                               Find(data, node.next);

                /* Breche ab, falls die Ende der Liste erreicht wurde. */
                if (node.next.next == null) {
                    if (debug) Console.WriteLine("Failed to find "+ data);
                    return defaultGenericValue;
                }

                // Satisfy return.w
                return defaultGenericValue;
            }

            public int count() {
                Node tempNode = this.head;
                int count = 1;

                while (tempNode != null) {
                    count++;
                    tempNode = tempNode.next; 
                } return count;
            }

            // Iteratives ChangeAtPos
            public void ChangeAtPos(int pos, T data, bool sort = false) {
                Node prevNode = new Node(defaultGenericValue);
                Node currNode = this.head;
                Node nextNode = this.head.next;

                // Edgecases
                /* Breche ab, wenn pos größer als die Länge der Liste ist, oder kleiner als 0 */   
                if (pos > this.count() || pos < 0) {
                    if (debug) Console.WriteLine("Pos " + pos + " is out of range.");
                    return; }
                
                /* Breche ab, falls pos null ist. */
                else if (pos == this.count() - 1) {
                    if (debug) Console.WriteLine("Pos " + pos + " is null. Cannot change the val of `null`.");
                    return; }

                /* Gehe durch die liste zu der `pos` position in der Liste */ 
                int cunt = 0;
                while (cunt < pos) {
                    if (debug) Console.WriteLine("\nprevNode: " + prevNode.data + "\ncurrNode: " 
                               + currNode.data + "\nnextNode: " + nextNode.data);

                    prevNode = currNode;
                    currNode = nextNode;
                    nextNode = nextNode.next;

                    cunt++;
                }

                /* Falls sort an ist, entferne alle verbindungen zu den Knoten @ pos */
                if (sort) { prevNode.next = nextNode; this.add(data); }
                
                /* , sonst ändere einfach den Wert der Knoten.                       */ 
                else currNode.data = data;
            }
            
            public override string ToString()  {
                Node tempNode = this.head;
                string tempString = "[";

                while (tempNode != null) {
                    if (tempNode.next != null) tempString += " \"" + tempNode.data + "\", ";
                    else                       tempString += " \"" + tempNode.data + "\", null ]";
                    tempNode = tempNode.next;
                }

                return tempString;
            } 
        }

        public static void Main(string[] args) {
            VerketteteListe<int> intList = new VerketteteListe<int>();
            intList.add(5);
            intList.add(7);
            intList.add(4);
            intList.add(9);
            intList.add(6);

            VerketteteListe<string> strList = new VerketteteListe<string>();
            strList.add("dick");
            strList.add("butt");
            strList.add("jiggle");
            strList.add("arse");

            Console.WriteLine("\n" + intList);            
            Console.WriteLine("\n" + strList);

            intList.ChangeAtPos(5, 99);
            intList.Remove(7);

            Console.WriteLine("\n" + intList);                        
        }
    }
}
