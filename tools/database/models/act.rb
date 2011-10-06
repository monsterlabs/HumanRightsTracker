class Act < ActiveRecord::Base
   belongs_to :case
   has_many :victims
end
