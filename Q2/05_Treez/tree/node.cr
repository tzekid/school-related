module Naive
  class Node(T)
    property value : T?
    property left_child : Node(T)?
    property right_child : Node(T)?
    property balance : Int32?

    def initialize(@value : T)
    end

    def initialize(@value : T, @balance : Int32)
    end

    def children
      children : Array(Node(T)) = [] of Node(T)

      unless self.nil?
        children << self.left_child.as(Node)  unless self.left_child.nil?
        children << self.right_child.as(Node) unless self.right_child.nil?
      end # if 

      children
    end
  end
end