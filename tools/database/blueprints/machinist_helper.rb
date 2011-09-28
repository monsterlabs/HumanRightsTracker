require 'machinist/active_record'
require 'faker'
require 'stringio'

# Monkey Patching to avoid troubles with converted chars in the stored images
module ActiveRecord
  module ConnectionAdapters #:nodoc:
    class SQLiteColumn < Column #:nodoc:
      class <<  self
        def string_to_binary(value)
         value
        end
      end
    end
  end
end

module MachinistHelper
  
  def save_image(prefix, max_number)
    record = Image.new
    image_path = File.expand_path("../../seeds/images/#{prefix}_#{rand(max_number)}.jpg", __FILE__)
    record.original = Gdk::Pixbuf.new(image_path).save_to_buffer("jpeg")
    record.thumbnail = Gdk::Pixbuf.new(image_path).scale(90,100).save_to_buffer("jpeg")
    record.icon = Gdk::Pixbuf.new(image_path).scale(43,48).save_to_buffer("jpeg")  
    record.save
    record
  end

  def person_image
    save_image('image', 7)
  end

  def institution_image
    save_image('logo', 4)
  end
end