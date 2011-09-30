class Intervention < ActiveRecord::Base
  belongs_to :intervention_type
  has_many :intervention_affected_people
end
