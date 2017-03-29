require "./node.cr"

module Naive
  class Tree(A)
    property root : Node(A)?

    def initialize
      @root = nil
    end

    def initialize(value)
      @root = Node(A).new value
    end


    def add(value)
      if @root.nil?
        @root = Node(A).new value
        return
      end

      tempNode = @root

      loop do
      unless tempNode.nil? || value.nil? 
        if value.as(A) < tempNode.value.as(A)
          if tempNode.leftChild.nil?
            tempNode.leftChild = Node(A).new value
            return
          else
            tempNode = tempNode.leftChild
          end
        else
          if tempNode.rightChild.nil?
            tempNode.rightChild = Node(A).new value
            return
          else
            tempNode = tempNode.rightChild
          end
        end
      else
        break
      end # unless else
      end # loop
    end # add


# TODO
    def del
    end


    def preOrder(node : Node(A)? = @root, nodez = [] of Node(A))
      unless node.nil?
        nodez << node

        unless node.leftChild.nil?
          preOrder(node.leftChild, nodez)
        end

        unless node.rightChild.nil?
          preOrder(node.rightChild, nodez)
        end
      end

      nodez
    end


    def inOrder(node : Node(A)? = @root, nodez = [] of Node(A))
      unless node.nil?
        unless node.leftChild.nil?
          inOrder(node.leftChild, nodez)
        end
        nodez << node
        unless node.rightChild.nil?
          inOrder(node.rightChild, nodez)
        end
      end
      nodez
    end


    def postOrder(node : Node(A)? = @root, nodez = [] of Node(A))
      unless node.nil?
        unless node.leftChild.nil?
          postOrder(node.leftChild, nodez)
        end
        unless node.rightChild.nil?
          postOrder(node.rightChild, nodez)
        end
        nodez << node
      end

      nodez
    end


    def breadthFirst(node : Node(A)? = @root, nodez = [] of Node(A))
      unless node.nil?
        queue = [] of Node(A)
        queue << node

        until queue.empty?
          # yield node
          nodez << node
          unless node.leftChild.nil?
            queue << node.leftChild.as(Node)
          end

          unless node.rightChild.nil?
            queue << node.rightChild.as(Node)
          end

          queue.shift
          unless queue.empty?; node = queue[0]; end
        end
      end
      
      nodez
    end


    def nodeBalance(node : Node(A)? = @root, balance : Number = 0)
      unless node.nil?
        unless node.leftChild.nil?
          balance -= 1
          balance = nodeBalance node.leftChild, balance
        end
        
        unless node.rightChild.nil?
          balance += 1
          balance = nodeBalance node.rightChild, balance
        end
      end

      balance
    end
  end # class


  class AVL_Tree(A) < Tree(A)
    def check_balance(tree = self)

    end
  end
end # module