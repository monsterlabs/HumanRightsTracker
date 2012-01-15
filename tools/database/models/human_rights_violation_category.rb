class HumanRightsViolationCategory < ActiveRecord::Base
  belongs_to :human_rights_violation_category, :foreign_key => 'parent_id'
end
