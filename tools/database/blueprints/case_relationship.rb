# encoding: utf-8
CaseRelationship.destroy_all

CaseRelationship.blueprint do
  case_id { Case.all.sample.id }
  related_case_id { Case.all.sample.id }
  relationship_type_id { RelationshipType.all.sample.id }
  observations { Faker::Lorem.paragraph(rand(6)) }
  comments { Faker::Lorem.paragraph(rand(6)) }
end
