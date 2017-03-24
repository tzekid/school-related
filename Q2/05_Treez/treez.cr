require "./tree/*"

module Naive
  dick = Tree(Int32).new 4
  dick.add 2
  dick.add 3
  dick.add 1
  dick.add 5
  dick.add 7
  dick.add 6

  # p "preOrder"
  # dick.preOrder

  # p "inOrder"
  # dick.inOrder()
  # p "postOrder"
  # dick.postOrder()

  p "Nodes printed level-wise"
  dick.breadthFirst{ |x| p x.value }

  puts # Newline

  p "Balance of each individual node"
  dick.breadthFirst{ |x| p dick.nodeBalance x }

  # p dick.nodeBalance 
end
