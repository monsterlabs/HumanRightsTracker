# encoding: utf-8
include MachinistHelper
Institution.destroy_all

images = 30.times.inject ([]) do |array|
    array.push institution_image; array 
end

Institution.blueprint do 
  company_name = Faker::Company.name
  name { company_name }
  abbrev { company_name.gsub(/[^A-Z]+/,'') }
  location { Faker::Address.street_address }
  phone { Faker::PhoneNumber.phone_number }
  fax   { Faker::PhoneNumber.phone_number }
  url   { "http://" + Faker::Internet.domain_name }
  email { Faker::Internet.email }

  institution_type_id  { InstitutionType.all.sample.id } 

  institution_category_id  { InstitutionCategory.all.sample.id } 
  @country = Country.find_by_name('México')
  country_id { @country.id }
  @state = @country.states.sample
  state_id { @state.id }
  city_id { @state.cities.sample.id }
  image { images.pop }
end