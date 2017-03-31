#!/bin/crystal  
require "socket"
require "readline"

alias IP = Socket::IPAddress

puts "BOP"

struct Char
  def hex_to_byte()
    if '0' <= self <= '9'
      (self - '0').to_u8
    elsif 'a' <= self <= 'f'
      (self - 'a' + 10).to_u8
    else
      raise "#{self} not a hex number"
    end
  end
end


class String
  def hex_to_bytes : Bytes
    bytes = Bytes.new(self.size / 2)
    bytes.size.times do |i|
      bytes[i] = self.char_at(i * 2).hex_to_byte * 16 + self.char_at(i * 2 + 1).hex_to_byte
    end
    bytes
  end

  def to_a
    self.to_slice.to_a
  end

  def to_ip
    bytes = Bytes.new(4)
    
    IP.new(self.rstrip, 80)                    # x.y.z.c:80
      .to_s.split(/:/)[0]                      # x.y.z.c
      .split('.').map_with_index do |byte, i|  # ["x", "y", "z", "c"]

      bytes[i] = byte.to_u8
    end
    bytes

  rescue
    nil
  end # def
end # class


struct Number
  def to_bytes
    bytes = Bytes.new 4
    tmp = 0
    pos = 0

    self.times do |i|
      tmp += 1

      bytes[pos] += 2**(8-tmp)

      tmp =  0 if tmp % 8 == 0
      pos += 1 if tmp % 8 == 0
    end

    bytes
  end

  def first_last_bait
    i = 31
    j = 0
    until i == 0
      j += 1
      break if self.bit(i) == 1
      i -= 1
    end

    j
  end
end


struct Slice(T)
  def ^(other : Slice(T))
    nil unless self.size == other.size
    new_slice = Slice(T).new self.size

    self.size.times{ |i| new_slice[i] = self[i] ^ other[i] }

    new_slice
  end

  def |(other : Slice(T))
    nil unless self.size == other.size
    new_slice = Slice(T).new self.size

    self.size.times{ |i| new_slice[i] = self[i] | other[i] }

    new_slice
  end

  def &(other : Slice(T))
    nil unless self.size == other.size
    new_slice = Slice(T).new self.size

    self.size.times{ |i| new_slice[i] = self[i] & other[i] }

    new_slice
  end

  def to_ip_s
    ip_s = ""
    self.each{ |x| ip_s += x.to_s + "."}

    ip_s.rchop # string w/o last char
  end

  def to_i(powa = 256)
    result : UInt64 = 0_u64
    self.map_with_index{ |x, i| result += x.to_u * powa**(3 - i) }
    
    result
  end
end


def generate_subnets(network : Bytes, sbnmsk : Bytes, n : UInt32)
  subnetz = {} of Bytes => UInt8
  numba : UInt64 = 0_u64
  
  pre = 0
  pos = 0
  sop = 3
  until pos == 7 || sop == 0
    until sop == 0 || sbnmsk[sop] != 0
      sop -= 1
      pre += 9_u64
    end
    
    if sbnmsk[sop].bit(pos)
      pre += 1
      numba = 2_u64 ** (pre)
      # puts "Da numba => #{numba}"
      break
    end

    pos += 1
  end

  sbnet = (network & sbnmsk).to_i

  n.times do
    subnetz[sbnet.to_s(16).hex_to_bytes] = numba.first_last_bait.to_u8
    # puts sbnet
    # puts sbnet.to_s(16)
    # puts sbnet.to_s(16).hex_to_bytes

    sbnet += numba
  end

  subnetz
end


def get_input(prompt : String, *, newline = false)
  puts  prompt if     newline
  print prompt unless newline

  gets.as(String).rstrip
end


input = " "
ip : Bytes

sfx : UInt8 # suffx for da
sbnmsk : Bytes = Bytes.new(4) # subnetmask
wildcard : Bytes = Bytes.new(4)

full_of_ffz : Bytes = Bytes.new(4)
full_of_ffz.size.times{ |i| full_of_ffz[i] = 255_u8 } # fill it with ff'z

max_range : Bytes = Bytes.new(4)
min_range : Bytes = Bytes.new(4)

n_of_subnetz : UInt32


until input == "exit" || input == "break"
  puts # Newline

  input = get_input("Enter an IP: ")
  input = "97.197.0.0" if input.rstrip == ""
  ip = input.to_ip.nil? ? next : input.to_ip.as(Bytes)

  input = get_input("Enter a suffix for the subnetmask: ")
  input = "10" if input == ""
  sfx = input.to_u8? ? input.to_u8 : next

  puts # Newline

  sbnmsk = sfx.to_bytes
  puts "Subnetmask is #{sbnmsk.to_ip_s}"

  wildcard = sbnmsk ^ full_of_ffz
  puts "Wildcard is #{wildcard.to_ip_s}"

  min_range = ip & sbnmsk
  max_range = ip | wildcard
  puts "Address range: #{min_range.to_ip_s} - #{max_range.to_ip_s}"

  # puts min_range.to_i

  puts # Newline
  input = get_input("How many subnetworkz do yer need ?: ")
  input = "7" if input == ""
  n_of_subnetz = input.to_u32 ? input.to_u32 : next

  subnetz = generate_subnets(ip, sbnmsk, n_of_subnetz)

  puts # Newline
  subnetz.map_with_index{ |(x, y) i| puts "#{i}: #{x.to_ip_s}/#{y}" }

  puts # Newline
end

puts "\nEOP"
