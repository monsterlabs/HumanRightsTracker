class Intervention < ActiveRecord::Base
  belongs_to :intervention_type

  belongs_to :interventor, :class_name => 'Person'
  belongs_to :interventor_institution, :class_name => 'Institution'
  belongs_to :interventor_affiliation_type, :class_name => 'AffiliationType'

  belongs_to :supporter, :class_name => 'Person'
  belongs_to :supporter_institution, :class_name => 'Institution'
  belongs_to :supporter_affiliation_type, :class_name => 'AffiliationType'
  has_many :intervention_affected_people
end
