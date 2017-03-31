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


    ## TODO: right syntax for alias
    # alias del = remove

    def del(value : A)
      node = find_node value
      del node.as(Node) unless node.nil?
    end


    def del(node : Node(A))
      parent = get_parent node
      return nil if parent.nil?

      node_is_leaf = leaf? node
      is_left_child = parent.left_child == node ? true : false

      if node_is_leaf
        is_left_child ? parent.left_child  = nil
                      : parent.right_child = nil

        return true
      end 

      node_children = children_of(node)
      if node_children.size == 1
        unless node.left_child.nil?; parent.left_child  = node.left_child; return false; end
        unless node.right_child.nil?; parent.right_child  = node.right_child; return true; end
      end

      return false
      # left_child
      # right_child
    end


    def children_of(node : Node(A)? = @root)
      children : Array(Node(A)) = [] of Node(A)

      unless node.nil?
        children << node.left_child.as(Node) unless node.left_child.nil?
        children << node.right_child.as(Node) unless node.right_child.nil?
      end # if 

      children
    end # def


    def leaf?(node : Node(A)? = @root)
      children_of(node).size == 0 unless node.nil?
    end


    def get_parent(value : A)
      node = find_node value
      return get_parent node unless node.nil?
    end

    def get_parent(node : Node(A))
      return @root if node == @root

      in_order.each do |parent|
        if parent.left_child == node || parent.right_child == node
          return parent
        end # if
      end # do
    end # get_parent


    def find_node(value = A)
      in_order.each do |x|
        return x if x.value == value
      end

      raise "Could not find a node with the value: #{value}"
    end

### TRAVERSAL

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


### AVL Tree
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


    def refresh_node_balance
      in_order do |node|
        node.balance = node_balance node
      end
    end

  end
end # module