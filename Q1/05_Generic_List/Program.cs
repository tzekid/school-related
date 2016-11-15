using System;
using System.Collections.Generic;

namespace ConsoleApplication {
    public class Program {
        public class GenList<T> where T : IComparable {
            public bool IsEqual<t>(t a, t b) { return EqualityComparer<t>.Default.Equals(a, b); }

            public class Node {
                public T data = default(T);
                public Node next = null;

                public Node(T t) { data = t; }
            }

            // public Node head = new Node();
            public Node head = new Node(default(T));
            
            // default value of T; can be used as a null placeholder
            T deft = default(T);

            public void add(T data) {
                Console.WriteLine("\nLooking into " + data);
                Node tempNode = new Node(default(T));
                if (this.IsEqual(head.data, deft)) {
                    this.head = new Node(data);
                    Console.WriteLine(this.head.data + " was added as head.");
                } else if (!this.IsEqual(head.data, deft) && (this.head.next == null)) {
                    if (this.head.data.CompareTo(data) == -1) { 
                        Console.WriteLine("Val next to head is " + data);
                        this.head.next = new Node(data); }
                    else {
                        tempNode = this.head;
                        this.head = new Node(data);
                        this.head.next = tempNode;
                        Console.WriteLine(this.head.data + " is now head.");
                    }
                } else {
                    Node prevNode = new Node(deft);
                    Node currNode = this.head;
                    Node nextNode = this.head.next;

                    Node newNode = new Node(data);

                    while (true) {
                        if (data.CompareTo(currNode.data) == -1) {
                            if (currNode == this.head) {
                                Console.WriteLine("Replaced the head " + this.head.data + " with " + data);
                                this.head = newNode;
                                this.head.next = currNode;
                                this.add(currNode.data);
                                break;
                            } else {
                                prevNode.next = newNode;
                                prevNode.next.next = nextNode;
                                Console.WriteLine("Replaced " + currNode.data + " with " + data);
                                this.add(currNode.data);
                                break;
                            }
                        }  else if (data.CompareTo(currNode.data) == 0) {
                                Console.WriteLine("Equal");
                                break;
                        } else if (data.CompareTo(currNode.data) == 1) {
                            prevNode = currNode;
                            currNode = nextNode;
                            nextNode = nextNode.next;
                            Console.WriteLine("dis be to that: " + data.CompareTo(currNode.data));
                        } else {
                            Console.WriteLine("Dude ... you broke teh Matrix");
                            break;
                        }

                        // If I don't doz teh lesser than (-1) check, shit will hit za fan
                        if (data.CompareTo(currNode.data) != -1 && nextNode == null) {
                            currNode.next = newNode;
                            break;
                        }
                    }
                }
            }
            
            public void remove(Node node) {
                Node prevNode = new Node(deft);
                Node currNode = this.head;
                Node nextNode = this.head.next;

                while (true) {
                    if (currNode == node) {
                        if (currNode == this.head) {
                            if (nextNode != null) this.head = nextNode;
                            else                  this.head = new Node(deft);
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
            
            public Node findNode(T data) {
                Node tempNode = this.head;

                while (tempNode.next != null) {
                    if (IsEqual(tempNode.data, data)) return tempNode;
                    else                              tempNode = tempNode.next; 
                }

                Console.WriteLine(data + " was not found in list.");
                return null;
            }

            public int count() {
                Node tempNode = this.head;
                int count = 1;

                while (tempNode != null) {
                    count++;
                    tempNode = tempNode.next; 
                }
                return count;
            }

            // public void changeAtPos(int pos, T data) {
            //     Node prevNode = this.head;
            //     Node currNode = this.head.next;
            //     Node nextNode = this.head.next.next;

            //     int cunt = this.count() - 1;
            //     while (cunt < this.count()) {
            //         prevNode = currNode;
            //         currNode = nextNode;
            //         nextNode = nextNode.next;
            //     }
            // }

            public override string ToString()  {
                Node tempNode = this.head;
                string tempString = this.GetType().Name.ToString() + ": [";
                while (tempNode != null) {
                    if (tempNode.next != null) tempString += " \"" + tempNode.data + "\", ";
                    else                       tempString += " \"" + tempNode.data + "\", null ]";
                    tempNode = tempNode.next;
                }

                return tempString;
            } 
        }

        public static void Main(string[] args) {
            GenList<int> intList = new GenList<int>();
            intList.add(5);
            intList.add(7);
            intList.add(4);
            intList.add(9);
            intList.add(6);

            // GenList<string> strList = new GenList<string>();
            // strList.add("dick");
            // strList.add("butt");
            // strList.add("head");
            // strList.add("arse");

            Console.WriteLine("\n" + intList);
            // Console.WriteLine("\n" + strList);
        }
    }
}
