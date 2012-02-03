class PersonDetail < ActiveRecord::Base
  belongs_to :ethnic_group
  belongs_to :religion
  belongs_to :scholarity_level
  belongs_to :job
  belongs_to :person
  belongs_to :indigenous_group
end