using System;

// [ ] objeketn
// [ ] generische Liste

namespace DataTypes {
    public class Types {
        public class Node {
            public string value = null;
            public Node nextElement = null;
        }

        public class LinkedList {
            public Node head = new Node();
            public Node body = new Node();
            public bool debg = true;

            // Unsorted add
            public void add_ (Node node) {
                if (this.head.value == null)
                    this.head = node;
                else if (this.head.value != null && this.body.value == null) {
                    this.body = node;
                    this.head.nextElement = node;
                } else {
                    this.body.nextElement = node;
                    this.body = node;
                }
                if (this.debg) Console.WriteLine(this);
            }


            // recursive sorted add
            public void _add(Node node) {
                if (this.head.value == null) {
                    this.head = node;
                } else if (this.body.value == null) {
                    if (this.head.value.CompareTo(this.body.value) != 1) {
                        this.body = node;
                        this.head.nextElement = this.body;
                    } else {
                        this.body = this.head;
                        this.head = node;
                        this.head.nextElement = this.body;
                    }
                } else {
                    Node prevNode = this.head;
                    Node currNode = this.head.nextElement;
                    Node nextNode = currNode.nextElement;

                    while(true) {
                        if (node.value.CompareTo(currNode.value) != 1) {
                            prevNode.nextElement = node;
                            node.nextElement = nextNode;
                            this._add(currNode);
                        } else {
                            prevNode = currNode;
                            currNode = nextNode;
                            nextNode = nextNode.nextElement;
                        }

                        if (nextNode == null) break;
                    }
                }
            }


            public void add (Node node) {
                Node prevNode = new Node();
                Node currNode = new Node();
                Node nextNode = new Node();

                if (this.head.value == null) {
                    this.head = node;
                } else if (this.body.value == null) {
                    if (this.head.value.CompareTo(this.body.value) != 1) {
                        this.body = node;
                        this.head.nextElement = this.body;
                    } else {
                        this.body = this.head;
                        this.head = node;
                        this.head.nextElement = this.body;
                    }
                } else {
                     if (this.head.value.CompareTo(node.value) == 1) {
                        currNode = this.head;
                        nextNode = this.body;

                        this.head = node;
                        this.body = currNode;

                        this.head.nextElement = currNode;
                        this.body.nextElement = nextNode;
                    //} else if (this.head.value.CompareTo(node.value) != 1) {
                    } else if (this.head.value.CompareTo(node.value) == 0) {
                        prevNode = this.head;
                        nextNode = this.head.nextElement;

                        this.head.nextElement = node;
                        node.nextElement = nextNode;
                    } else {
                        prevNode = this.head;
                        currNode = this.head.nextElement;

                        while(currNode.value != null) {
                            if (currNode.value.CompareTo(node.value) == 1) {
                                prevNode.nextElement = node;
                                node.nextElement = currNode;

                                if (currNode.nextElement == null)
                                    this.body = node;

                                break;
                            } else if (currNode.nextElement == null) {
                                currNode.nextElement = node;
                                this.body = node;
                                break;
                            } else {
                                prevNode = currNode;
                                currNode = currNode.nextElement;
                            }
                        }
                    }
                }

                Console.WriteLine(this);
            }

            public int count () {
                int c = 1;
                Node tempNode = this.head;

                while(tempNode.nextElement != null) {
                    c++;
                    tempNode = tempNode.nextElement;
                }

                return c;
            }

            public Node find (string value) {
                Node tempNode = this.head;

                while (tempNode.nextElement != null) {
                    if (tempNode.value == value)
                        return tempNode;
                }

                Console.WriteLine("There is no object w/ the value: '"+ value +"' in this list.");
                return null;
            }

            public int findNodePos(Node node) {
                Node tempNode = this.head;
                int cunter = 0;

                while (tempNode != node) {
                    cunter ++;

                    tempNode = tempNode.nextElement;
                    if (tempNode == null) return -1;
                }

                return cunter;
            }

            public Node returnNodeAtPos(int pos) {
                Node tempNode = this.head;

                if (pos == 0) return tempNode;
                else if (pos >= this.count()) return null;
                else {
                    for (int i = 1; i <= pos; i++) {
                        tempNode = tempNode.nextElement;
                    }
                    return tempNode;
                }
            }

            public void changeAtPos (int pos, Node node) {
                Node tempNode = this.head;
                Node tempNextNode = new Node();
                Node tempNextNextNode = new Node();

                int cunt = this.count() - 1;
                while (cunt < this.count() - 1) {
                    tempNode = tempNode.nextElement;
                    tempNextNode = tempNode.nextElement;
                }

                tempNextNextNode = tempNextNode.nextElement;

                if (node == null) {
                    tempNode.nextElement = tempNextNextNode;
                } else if (cunt == 0) {
                    node.nextElement = this.head.nextElement;
                    this.head = node;
                } else {
                    tempNode.nextElement = node;
                    tempNextNode = node;
                    tempNextNode.nextElement = tempNextNextNode;
                }
            }

            public void remove(Node node) {
                Node oldNode = this.head;
                Node tempNode = new Node();

                int pos = findNodePos(node);

                changeAtPos(pos, null);
            }

            public override string ToString() {
                Node tempNode = this.head;
                string tempString = this.GetType().Name.ToString() + ": [";
                while (tempNode != null) {
                    if (tempNode.nextElement != null) {
                        tempString += " \"" + tempNode.value + "\", ";
                    } else {
                        tempString += " \"" + tempNode.value + "\", null ]";
                    }
                    tempNode = tempNode.nextElement;
                }

                return tempString;
            }
        }
    }
}
