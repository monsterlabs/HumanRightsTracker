# encoding: utf-8
require 'machinist/active_record'
require 'faker'
require 'stringio'
include Magick

def get_random_item(size)
  (0..(size - 1)).to_a.sample
end

def save_image
  record = Image.new
  image_path = "./seeds/images/image_#{get_random_item(7)}.jpg"
  
  #Fix it: Ruby does not save the images in correct format for GtkPixbuf
  record.original = Gdk::Pixbuf.new(image_path).pixels
  record.thumbnail = Gdk::Pixbuf.new(image_path).pixels
  record.icon = Gdk::Pixbuf.new(image_path).pixels
  record.save
  record
end

# images = []
# 25.times do
#   images.push save_image
# end

Person.blueprint do
  firstname { Faker::Name.first_name}
  lastname  { Faker::Name.first_name}
  gender    { false }
  birthday  { 24.years.ago }
  marital_status_id { MaritalStatus.first.id }
  
  @country = Country.find_by_name('México')
  country_id { @country.id }
  
  @state = @country.states[get_random_item(@country.states.size)]
  state_id { @state.id }
  
  @city = @state.cities[get_random_item(@state.cities.size)]
  city_id { @city.id }

  settlement { Faker::Address.city }
  person_detail
  #image { images.pop }
end

PersonDetail.blueprint do
  number_of_sons   { get_random_item(10) }
  ethnic_group_id  { EthnicGroup.all[get_random_item(EthnicGroup.all.size)].id } 
  religion_id { Religion.all[get_random_item(Religion.all.size)].id }
  scholarity_level_id { ScholarityLevel.all[get_random_item(ScholarityLevel.all.size)].id }
  job_id  { Job.all[get_random_item(Job.all.size)].id }
  indigenous_group {Faker::Lorem.words(3).join(" ")}
end
