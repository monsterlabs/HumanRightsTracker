# encoding: utf-8
InstitutionPerson.destroy_all

InstitutionPerson.blueprint do 
  institution_id { Institution.all.sample.id }
  person_id      { Person.all.sample.id }
end