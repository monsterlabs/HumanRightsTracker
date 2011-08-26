# encoding: utf-8
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

def random_item(size)
  (0..(size - 1)).to_a.sample
end

def save_image
  record = Image.new
  image_path = "./seeds/images/image_#{random_item(7)}.jpg"  
  record.original = Gdk::Pixbuf.new(image_path).save_to_buffer("jpeg")
  record.thumbnail = Gdk::Pixbuf.new(image_path).scale(90,100).save_to_buffer("jpeg")
  record.icon = Gdk::Pixbuf.new(image_path).scale(43,48).save_to_buffer("jpeg")  
  record.save
  record
end

images = []
25.times do
  images.push save_image
end

Person.blueprint do
  firstname { Faker::Name.first_name}
  lastname  { Faker::Name.first_name}
  gender    { false }
  birthday  { 24.years.ago }
  marital_status_id { MaritalStatus.first.id }
  
  @country = Country.find_by_name('México')
  country_id { @country.id }
  
  @state = @country.states[random_item(@country.states.size)]
  state_id { @state.id }
  
  @city = @state.cities[random_item(@state.cities.size)]
  city_id { @city.id }

  settlement { Faker::Address.city }
  person_detail
  image { images.pop }
end

PersonDetail.blueprint do
  number_of_sons   { random_item(10) }
  ethnic_group_id  { EthnicGroup.all[random_item(EthnicGroup.all.size)].id } 
  religion_id { Religion.all[random_item(Religion.all.size)].id }
  scholarity_level_id { ScholarityLevel.all[random_item(ScholarityLevel.all.size)].id }
  job_id  { Job.all[random_item(Job.all.size)].id }
  indigenous_group {Faker::Lorem.words(3).join(" ")}
end
