# encoding: utf-8
InstitutionPerson.destroy_all

InstitutionPerson.blueprint do 
  institution_id { Institution.all.sample.id }
  person_id      { Person.all.sample.id }
  affiliation_type_id { AffiliationType.all.sample.id }
  start_date  { 24.years.ago }
  start_date_type_id  { DateType.all.sample.id }
  end_date  { Date.today }
  start_date_type_id  { DateType.all.sample.id }
  comments { Faker::Lorem.paragraph(rand(6)) }
end