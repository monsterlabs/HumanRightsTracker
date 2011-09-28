# encoding: utf-8
require File.expand_path("../machinist_helper", __FILE__)
include MachinistHelper

PersonAct.destroy_all
Act.destroy_all
Case.destroy_all

Case.blueprint do
  name { Faker::Lorem.words(2).join(" ")}
  start_date  { 24.years.ago }
  start_date_type_id  { DateType.all.sample.id }
  acts(rand(3))
end

Act.blueprint do
  human_rights_violation_id { HumanRightsViolation.all.sample.id }
  human_rights_violation_category_id { HumanRightsViolationCategory.all.sample.id }
  start_date  { 24.years.ago }
  start_date_type_id  { DateType.all.sample.id }
  end_date  { 23.years.ago }
  end_date_type_id  { DateType.all.sample.id }
  @country = Country.find_by_name('México')
  country_id { @country.id }
  
  @state = @country.states[random_item(@country.states.size)]
  state_id { @state.id }
  
  @city = @state.cities[random_item(@state.cities.size)]
  city_id { @city.id }
  
  settlement { Faker::Address.city }
  
  affected_people_number { rand(10) }
  
  summary { Faker::Lorem.sentence(1)}
  narrative_information { Faker::Lorem.paragraph(2)}
  comments { Faker::Lorem.paragraph(1)}
  person_acts(rand(5))
end

PersonAct.blueprint do
  role_id { Role.all.sample.id }
  person_id { Person.all.sample.id }
end