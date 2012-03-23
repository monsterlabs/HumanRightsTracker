# encoding: utf-8
InstitutionRelationship.destroy_all

InstitutionRelationship.blueprint do
  @size = Institution.all.size
  @half = @size / 2

  institution_id { Institution.all.slice(0, @half).sample.id }
  institution_relationship_type_id { InstitutionRelationshipType.all.sample.id }  
  related_institution_id { Institution.all.slice((@half + 1), (@size - 1)).sample.id }

  start_date  { 24.years.ago }
  start_date_type_id  { DateType.all.sample.id }

  end_date  { Date.today }
  start_date_type_id  { DateType.all.sample.id }

  comments { Faker::Lorem.paragraph(rand(6)) }
end
