# encoding: utf-8
PersonAct.destroy_all
Act.destroy_all
Case.destroy_all

Case.blueprint do
  name { Faker::Lorem.words(2).join(" ")}
  start_date  { 24.years.ago }
  start_date_type_id  { DateType.all.sample.id }
  
  acts(rand(3))
  interventions(rand(2))
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
  @state = @country.states.sample
  state_id { @state.id }
  city_id { @state.cities.sample.id }
  
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

Intervention.blueprint do
  intervention_type_id { InterventionType.all.sample.id }
  interventor_id { Person.all.sample.id }
  supporter_id { Person.all.sample.id }
  date  { rand(10).months.ago }
  intervention_affected_people(rand(5))
end

InterventionAffectedPerson.blueprint do
  person_id { Person.all.sample.id }
end