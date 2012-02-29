# encoding: utf-8
include MachinistHelper

PerpetratorAct.destroy_all
Perpetrator.destroy_all
Victim.destroy_all
Act.destroy_all
Case.destroy_all
TrackingInformation.destroy_all
Document.destroy_all

counter = 0
reset = lambda { counter = 0 }
auto_increment = lambda { return counter += 1 }

documents = 240.times.inject([]) do |array|
  array.push create_document
  array
end

Case.blueprint do
  name { Faker::Lorem.words(2).join(" ")}
  start_date  { 24.years.ago }
  start_date_type_id  { DateType.all.sample.id }

  end_date  { Date.today }
  start_date_type_id  { DateType.all.sample.id }

  acts(rand(3))
  interventions(rand(2))
  places(rand(4))

  narrative_description { Faker::Lorem.paragraph(2) }
  summary { Faker::Lorem.paragraph(2) }
  observations { Faker::Lorem.paragraph(2) }
  tracking_information(rand(3))
  reset.call
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
  victims(rand(5))
end

Victim.blueprint do
  person_id { Person.all.sample.id }
  victim_status_id { VictimStatus.all.sample.id }
  characteristics {  Faker::Lorem.paragraph(1) }
  perpetrators(rand(5))
end

Perpetrator.blueprint do
  person_id { Person.all.sample.id }
  institution_id { Institution.all.sample.id }
  affiliation_type_id { AffiliationType.all.sample.id }
  perpetrator_type_id { PerpetratorType.all.sample.id }
  perpetrator_acts(rand(5))
  perpetrator_status_id { PerpetratorStatus.all.sample.id }
  involvement_degree_id { InvolvementDegree.all.sample.id }
end

PerpetratorAct.blueprint do
  human_rights_violation_id { HumanRightsViolation.all.sample.id }
  act_place_id { ActPlace.all.sample.id }
  location { Faker::Address.street_address }
end

Intervention.blueprint do
  intervention_type_id { InterventionType.all.sample.id }

  interventor_id { Person.all.sample.id }
  interventor_institution_id { Institution.all.sample.id }
  interventor_affiliation_type_id{ AffiliationType.all.sample.id }

  supporter_id { Person.all.sample.id }
  supporter_institution_id { Institution.all.sample.id }
  supporter_affiliation_type_id { AffiliationType.all.sample.id }

  date  { rand(10).months.ago }
  intervention_affected_people(rand(5))
end

InterventionAffectedPerson.blueprint do
  person_id { Person.all.sample.id }
end

TrackingInformation.blueprint do
  title { Faker::Lorem.sentence(1) }
  record_id { auto_increment.call }
  date_of_receipt  { rand(2) == 0 ? 5.years.ago : 1.years.ago }
  date_type_id  { DateType.all.sample.id }
  comments { Faker::Lorem.paragraph(3) }
  case_status_id { CaseStatus.all.sample.id }
  documents { [documents.pop,documents.pop] }
end

Place.blueprint do
  @country = Country.find_by_name('México')
  country_id { @country.id }
  @state = @country.states.sample
  state_id { @state.id }
  city_id { @state.cities.sample.id }
end
