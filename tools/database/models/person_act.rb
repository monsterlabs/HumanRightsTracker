class PersonAct < ActiveRecord::Base
   belongs_to :act
   belongs_to :case
end
