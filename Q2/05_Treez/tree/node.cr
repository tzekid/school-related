module Naive
  class Node(T)
    getter value : T?
    property leftChild : Node(T)?
    property rightChild : Node(T)?

    def initialize(@value : T)
    end
  end
end