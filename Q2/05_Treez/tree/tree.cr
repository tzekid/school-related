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
      # p "Start loop" # Debug
      unless tempNode.nil? || value.nil? 
        if value.as(A) < tempNode.value.as(A)
          if tempNode.leftChild.nil?
            # p "Add #{value} as leftChild of #{tempNode.value}" # Debug
            tempNode.leftChild = Node(A).new value
            return
          else
            # p "tempNode to leftChild" # Debug
            tempNode = tempNode.leftChild
          end
        else
          if tempNode.rightChild.nil?
            # p "Add #{value} as rightChild of #{tempNode.value}" # Debug
            tempNode.rightChild = Node(A).new value
            return
          else
            # p "tempNode to rightChild" # Debug
            tempNode = tempNode.rightChild
          end
        end
      else
        # p "Breakz" # Debug
        break
      end # unless else
      end # loop
    end # add

    def del
    end

    def preOrder(node : Node(A)? = @root)
      unless node.nil?
        p node.value # Do stuff
        unless node.leftChild.nil?
          preOrder(node.leftChild)
        end

        unless node.rightChild.nil?
          preOrder(node.rightChild)
        end
      end
    end

    def inOrder(node : Node(A)? = @root)
      unless node.nil?
        unless node.leftChild.nil?
          inOrder(node.leftChild)
        end
        p node.value # Do stuff
        unless node.rightChild.nil?
          inOrder(node.rightChild)
        end
      end
    end

    def postOrder(node : Node(A)? = @root)
      unless node.nil?
        unless node.leftChild.nil?
          postOrder(node.leftChild)
        end
        unless node.rightChild.nil?
          postOrder(node.rightChild)
        end
        p node.value # Do stuff
      end
    end

    def breadthFirst(node : Node(A)? = @root, level : Number = 0)
      unless node.nil?
        queue = [] of Node(A)
        queue << node

        until queue.empty?
          p node.value # Do stuff
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

        balance
      end

      balance
    end

  end # class
end # module