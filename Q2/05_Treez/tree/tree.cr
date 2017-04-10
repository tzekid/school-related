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


    def add(*values)
      values.each{ |val| add val }
    end

    def add(node : Node)
      da_value = node.value
      add(da_value) unless da_value.nil?
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
      
      node.nil? ? "#{value} not found" : del node
    end


    # TODO: refractor del
    def del(node : Node(A))
      parent = get_parent(node)
      return nil if parent.nil?
      node.children.each{ |n| puts "#{ (node.left_child == n ? "left_child" : "right_child") }: #{n.value}" } # debug

      if node == @root
            
      end
      
      children = node.children

      puts "Its parent is #{parent}"

      node_is_leaf = leaf? node
      is_left_child = parent.left_child == node ? true : false

      puts "It is a leaf"     if node_is_leaf                                          # debug
      puts "It is the " + (is_left_child ? "left" : "right") + "_child of his parent"  # debug

      if node_is_leaf
        is_left_child ? parent.left_child  = nil
                      : parent.right_child = nil

        return true
      end 


      if children.size == 1
        unless node.left_child.nil?
          parent.left_child = node.left_child
          return true
        else
          parent.right_child = node.right_child
          return true
        end

      else
        parent.right_child = nil unless is_left_child        
        parent.left_child  = nil if     is_left_child
        
        children.reverse_each{ |x| add x }
        
        return true
      end
      
      false
    end

    def change_parent_to(value1 : A, value2 : A)
      change_parent_to(( find_node(value1).as(Node) ).as(Node), value2)
    end

    # def change_parent_to(node : Node(A)?, value : A)
    #   parent = get_parent node
    #   raise "dick" if parent.nil?
    #   puts "Parent was: #{parent.value}"
    #   parent.value = value unless parent.nil?
    #   puts "Parent should be: #{parent.value}"
    # end



    def leaf?(node : Node(A)? = @root)
      node.children.size == 0 unless node.nil?
    end


    def get_parent(value : A)
      node = find_node value
      return get_parent node unless node.nil?
    end

    def get_parent(node : Node(A))
      return nil if node == @root

      breadth_first.each do |parent|
        puts "Checking #{parent.value}"
        if parent.left_child == node || parent.right_child == node
          return parent
        end # if
      end # do

      nil
    end # get_parent


    def find_node(value = A)
      breadth_first.each do |x|
        return x if x.value == value
      end

      nil
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
  end


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