# encoding: utf-8
require File.expand_path("../machinist_helper", __FILE__)
include MachinistHelper

Person.destroy_all
PersonDetail.destroy_all
ImmigrationAttempt.destroy_all
Address.destroy_all

Image.where(:imageable_type => 'Person').destroy_all

images = 60.times.inject ([]) do |array|
    array.push person_image; array 
end

Person.blueprint do
  firstname { Faker::Name.first_name}
  lastname  { Faker::Name.first_name}
  gender    { false }
  birthday  { 24.years.ago }
  marital_status_id { MaritalStatus.first.id }

  @country = Country.find_by_name('México')
  country_id { @country.id }
  @state = @country.states.sample
  state_id { @state.id }
  city_id { @state.cities.sample.id }
  settlement { Faker::Address.city }
  email { Faker::Internet.email }

  person_detail
  if rand(2) > 0
    is_immigrant { true }
    immigration_attempt
  else
    is_immigrant { false }  
  end
  image { images.pop }
  address
end

PersonDetail.blueprint do
  number_of_sons   { rand(10) }
  ethnic_group_id  { EthnicGroup.all.sample.id } 
  religion_id { Religion.all.sample.id }
  scholarity_level_id { ScholarityLevel.all.sample.id }
  job_id  { Job.all.sample.id }
  indigenous_group {Faker::Lorem.words(3).join(" ")}
end

ImmigrationAttempt.blueprint do
    transit_country_id { Country.find_by_code('MX').id }
    destination_country_id { Country.find_by_code('US').id }
    traveling_reason_id { TravelingReason.all.sample.id}
    cross_border_attempts_transit_country { rand(5) }
    expulsions_from_destination_country { rand(3) }
    expulsions_from_transit_country { rand(3) }
end

Address.blueprint do
  location { Faker::Address.street_address }
  phone  { Faker::PhoneNumber.phone_number }
  mobile { Faker::PhoneNumber.phone_number }
  @country = Country.find_by_name('México')
  country_id { @country.id }
  @state = @country.states.sample
  state_id { @state.id }
  city_id { @state.cities.sample.id }
  zipcode { Faker::Address.zip_code }
end