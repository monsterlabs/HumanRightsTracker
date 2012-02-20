class InstitutionRelationship < ActiveRecord::Base
  belongs_to :institution
  belongs_to :related_institution
  belongs_to :institution_relationship_type
  belongs_to :start_date_type, :class_name => DateType, :foreign_key => 'start_date_type_id'
  belongs_to :end_date_type, :class_name => DateType, :foreign_key => 'end_date_type_id'
end
