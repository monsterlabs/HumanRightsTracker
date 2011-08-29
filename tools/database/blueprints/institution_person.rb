# encoding: utf-8
require File.expand_path("../machinist_helper", __FILE__)
include MachinistHelper
InstitutionPerson.destroy_all

InstitutionPerson.blueprint do 
  institution_id { Institution.all[random_item(Institution.all.size)].id }
  person_id      { Person.all[random_item(Person.all.size)].id }
end