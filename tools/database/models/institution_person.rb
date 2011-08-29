class InstitutionPerson < ActiveRecord::Base
  belongs_to :institution
  belongs_to :person
end