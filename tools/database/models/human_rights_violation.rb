class HumanRightsViolation < ActiveRecord::Base  
  belongs_to :human_rights_violation, :foreign_key => 'parent_id'
  belongs_to :human_rights_violation_category, :foreign_key => 'category_id'
end
