# encoding: utf-8
CaseRelationship.destroy_all

CaseRelationship.blueprint do
  @size =  Case.all.size
  @half = @size / 2
  case_id { Case.all.slice(0, @half).sample.id}
  related_case_id {  Case.all.slice((@half + 1), (@size - 1)).sample.id }
  relationship_type_id { RelationshipType.all.sample.id }
  observations { Faker::Lorem.paragraph(rand(6)) }
  comments { Faker::Lorem.paragraph(rand(6)) }
end
