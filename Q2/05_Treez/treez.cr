require "./tree/*"

module Naive
  dick = Tree(Int32).new 4
  dick.add 2
  dick.add 3
  dick.add 1
  dick.add 5
  dick.add 7
  dick.add 6

  # puts # Newline
  # dick.preOrder.each do |butt|
  #   unless butt.nil?
  #     p butt.value.to_s + " -> " + dick.nodeBalance(butt).to_s
  #   end
  # end

  # puts # Newline
  # dick.inOrder.each do |butt|
  #   unless butt.nil?
  #     p butt.value.to_s + " -> " + dick.nodeBalance(butt).to_s
  #   end
  # end

  # puts # Newline
  # dick.postOrder.each do |butt|
  #   unless butt.nil?
  #     p butt.value.to_s + " -> " + dick.nodeBalance(butt).to_s
  #   end
  # end

  # puts # Newline
  # dick.breadthFirst.each do |butt|
  #   unless butt.nil?
  #     p butt.value.to_s + " -> " + dick.nodeBalance(butt).to_s
  #   end
  # end

end

