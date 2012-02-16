class Perpetrator < ActiveRecord::Base
  belongs_to :victim
  belongs_to :person
  belongs_to :institution
  belongs_to :perpetrator_type
  belongs_to :perpetrator_status

  has_many :perpetrator_acts
end
