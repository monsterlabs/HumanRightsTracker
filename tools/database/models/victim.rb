class Victim < ActiveRecord::Base
   belongs_to :act
   belongs_to :person
   has_many :perpetrators
end
