#!/bin/crystal
require "./tree/*"

module Naive

  puts # Newline
  dick = Tree(Int32).new 4
  dick.add 2, 1, 3, 7, 6, 8

  p dick.del 7

  puts # Newline
  dick.breadth_first.each{ |x| p x.value }

end

