module Naive
  class Node(T)
    getter value : T?
    property leftChild : Node(T)?
    property rightChild : Node(T)?
    property balance : Int32?

    def initialize(@value : T)
    end

  end
end