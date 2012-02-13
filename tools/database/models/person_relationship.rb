class PersonRelationship < ActiveRecord::Base
  belongs_to :persom
  belongs_to :related_person
  belongs_to :person_relationship_type
  belongs_to :start_date_type, :class_name => DateType, :foreign_key => 'start_date_type_id'
  belongs_to :end_date_type, :class_name => DateType, :foreign_key => 'end_date_type_id'
end
