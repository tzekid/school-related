require "./tree/*"

module Naive
  dick = Tree(Int32).new 4
  dick.add 2
  dick.add 3
  dick.add 1
  dick.add 5
  dick.add 7
  dick.add 6

  puts # Newline
  dick.pre_order.each do |butt|
    unless butt.nil?
      # p butt.value.to_s + " -> " + dick.node_balance(butt).to_s
      p butt.value
    end
  end

  # puts # Newline
  # dick.in_order.each do |butt|
  #   unless butt.nil?
  #     p butt.value.to_s + " -> " + dick.node_balance(butt).to_s
  #   end
  # end

  # puts # Newline
  # dick.post_order.each do |butt|
  #   unless butt.nil?
  #     p butt.value.to_s + " -> " + dick.node_balance(butt).to_s
  #   end
  # end

  # puts # Newline
  # dick.breadth_first.each do |butt|
  #   unless butt.nil?
  #     p butt.value.to_s + " -> " + dick.node_balance(butt).to_s
  #   end
  # end

end

