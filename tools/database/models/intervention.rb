class Intervention < ActiveRecord::Base
  belongs_to :intervention_type

  belongs_to :interventor, :class_name => 'Person'
  belongs_to :interventor_institution, :class_name => 'Institution'
  belongs_to :interventor_job, :class_name => 'Job'

  belongs_to :supporter, :class_name => 'Person'
  belongs_to :supporter_institution, :class_name => 'Institution'
  belongs_to :supporter_job, :class_name => 'Job'
  has_many :intervention_affected_people
end
