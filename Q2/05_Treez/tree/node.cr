module Naive
  class Node(T)
    getter value : T?
    property left_child : Node(T)?
    property right_child : Node(T)?

    def initialize(@value : T)
    end

  end

  class B_Node(T) < Node(T)
    property balance : Int32?

    def initialize(@value : T, @balance : Int32)
    end
  end
end