require "./tree/*"

module Naive
  dick = Tree(Int32).new 4
  dick.add 2
  dick.add 3
  dick.add 1
  dick.add 5

  # p "preOrder"
  # dick.preOrder

  # p "inOrder"
  # dick.inOrder()
  # p "postOrder"
  # dick.postOrder()

  dick.breadthFirst()

  p dick.nodeBalance
end
