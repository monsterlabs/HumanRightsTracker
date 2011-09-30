class InterventionAffectedPerson < ActiveRecord::Base
  belongs_to :intervention
  belongs_to :person
end
