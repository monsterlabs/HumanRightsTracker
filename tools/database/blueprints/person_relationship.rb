# encoding: utf-8
PersonRelationship.destroy_all

PersonRelationship.blueprint do
  person_id { Person.all.sample.id }
  person_relationship_type_id { PersonRelationshipType.all.sample.id }
  related_person_id { Person.all.sample.id }

  start_date  { 24.years.ago }
  start_date_type_id  { DateType.all.sample.id }

  end_date  { Date.today }
  start_date_type_id  { DateType.all.sample.id }

  comments { Faker::Lorem.paragraph(rand(6)) }
end
