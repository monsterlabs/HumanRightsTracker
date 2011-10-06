class Perpetrator < ActiveRecord::Base
  belongs_to :victim
  belongs_to :person
  belongs_to :institution
  belongs_to :job
  has_many :perpetrator_acts
end
