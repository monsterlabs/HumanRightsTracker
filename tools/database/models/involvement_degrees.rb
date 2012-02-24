class InvolvementDegree < ActiveRecord::Base  
  belongs_to :involvement_degree, :foreign_key => 'parent_id'
end
