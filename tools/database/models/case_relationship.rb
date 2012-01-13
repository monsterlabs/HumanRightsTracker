class CaseRelationship < ActiveRecord::Base
  belongs_to :case
  belongs_to :related_case
  belongs_to :relationship_type
end
