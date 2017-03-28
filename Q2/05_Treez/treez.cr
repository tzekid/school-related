require "./tree/*"

module Naive
  dick = Tree(Int32).new 4
  dick.add 2
  dick.add 3
  dick.add 1
  dick.add 5
  dick.add 7
  dick.add 6

  dick.preOrder.each{ |x| unless x.nil?; p x.value; end }

  puts # Newline

  dick.inOrder.each{ |x| unless x.nil?; p x.value; end }

  puts # Newline

  dick.postOrder.each{ |x| unless x.nil?; p x.value; end }
end
