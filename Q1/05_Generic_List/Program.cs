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
                Node tempNode = new Node(default(T));

                if (this.IsEqual(head.data, deft)) {
                    this.head = new Node(data);
                    Console.WriteLine(this.head.data);
                } else if (!this.IsEqual(head.data, deft) && (this.head.next == null)) {
                    if (this.head.data.CompareTo(data) == -1) { this.head.next = new Node(data); }
                    else {
                        tempNode = this.head;
                        this.head = new Node(data);
                        this.head.next = tempNode;
                    }
                } else {
                    // TODO: Implement findNode
                    // Node prevNode = this.head;
                    // Node currNode = this.head.next;
                    // Node nextNode = currNode.next;

                    // while (true) {
                    //     if (data.CompareTo(currNode.data) != 1) {
                    //         prevNode.next = new Node(data);
                    //         findNode(data).next = currNode;
                    //         add(currNode.data);
                    //     } else {
                    //         prevNode = currNode;
                    //         currNode = nextNode;
                    //         nextNode = next.next;
                    //     }

                    //     if (!IsEqual(nextNode.data, deft.data)) break;
                    }
                }
            }
        }
            
            public void remove(Node node) { }
            
            // public Node findNodeAtPos(int pos) { }
            
            // public Node findNode(Node node) { }

            // public int count() { }

            public void changeNodePos(Node node) { }

            // public override string ToString()  { } 
        }

        public static void Main(string[] args) {
            GenList<int> list = new GenList<int>();
            list.add(20);
            list.add(5);
        }
    }
}
