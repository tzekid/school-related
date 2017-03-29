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
          if tempNode.left_child.nil?
            tempNode.left_child = Node(A).new value
            return
          else
            tempNode = tempNode.left_child
          end
        else
          if tempNode.right_child.nil?
            tempNode.right_child = Node(A).new value
            return
          else
            tempNode = tempNode.right_child
          end
        end
      else
        break
      end # unless else
      end # loop
    end # add

    ### TODO Delete
    def del(value : A)
      del find_node value
    end

    # TODO: IT EZ NOT DONE
    def del(node : Node(A))
      parent = get_parent node
      node_is_leaf = leaf? node
      
      # This part only takes place if the `if` statement is true
      left_side ? parent.left_child  = nil
                : parent.right_child = nil if node_is_leaf

      unless node_is_leaf
        left_side = parent.left_child == node ? true : false
        
        parent.left_child = node.right_child unless node.right_child.nil? if left_side
        parent.right_child = node.left_child unless node.left_child.nil? unless left_side


      end # unless
    end # del


    def children_of(node : Node(A))
      children = [] of Node
      unless node.left_child.nil?
        children << node.left_child
      end

      unless node.right_child.nil?
        children << node.right_child
      end

      children
    end


    def leaf?(node : Node(A))
      children(node).size == 0
    end


    def get_parent(value : A)
      get_parent find_node value
    end

    def get_parent(node : Node(A))
      return nil if node == @root

      if node.left_child.nil? && node.right_child.nil?
        in_order.each do |parent|
          if parent.left_child == node && parent.right_child == node
            parent
          end # if
        end # do
      end # if
    end # get_parent

    def find_node(value = A)
      in_order.each do |x|
        if x.value == value
          x.value
        end
      end
    end

    def pre_order(node : Node(A)? = @root, nodez = [] of Node(A))
      unless node.nil?
        nodez << node

        unless node.left_child.nil?
          pre_order(node.left_child, nodez)
        end

        unless node.right_child.nil?
          pre_order(node.right_child, nodez)
        end
      end

      nodez
    end 

    def in_order(node : Node(A)? = @root, nodez = [] of Node(A))
      unless node.nil?
        unless node.left_child.nil?
          in_order(node.left_child, nodez)
        end
        nodez << node
        unless node.right_child.nil?
          in_order(node.right_child, nodez)
        end
      end
      nodez
    end


    def post_order(node : Node(A)? = @root, nodez = [] of Node(A))
      unless node.nil?
        unless node.left_child.nil?
          post_order(node.left_child, nodez)
        end
        unless node.right_child.nil?
          post_order(node.right_child, nodez)
        end
        nodez << node
      end

      nodez
    end


    def breadth_first(node : Node(A)? = @root, nodez = [] of Node(A))
      unless node.nil?
        queue = [] of Node(A)
        queue << node

        until queue.empty?
          # yield node
          nodez << node
          unless node.left_child.nil?
            queue << node.left_child.as(Node)
          end

          unless node.right_child.nil?
            queue << node.right_child.as(Node)
          end

          queue.shift
          node = queue[0] unless queue.empty?
        end
      end
      
      nodez
    end
  end # class


  class AVL_Tree(A) < Tree(A)

    def node_balance(node : Node(A)? = @root, balance : Number = 0)
      unless node.nil?
        unless node.left_child.nil?
          balance -= 1
          balance = node_balance node.left_child, balance
        end
        
        unless node.right_child.nil?
          balance += 1
          balance = node_balance node.right_child, balance
        end
      end

      balance
    end


    # TODO
    def refresh_node_balance

    end
  end
end # module