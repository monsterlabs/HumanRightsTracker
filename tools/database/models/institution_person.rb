class InstitutionPerson < ActiveRecord::Base
  belongs_to :institution
  belongs_to :person
  belongs_to :affiliation_type
end