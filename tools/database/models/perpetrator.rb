class Perpetrator < ActiveRecord::Base
  belongs_to :victim
  belongs_to :person
  belongs_to :institution
  belongs_to :perpetrator_type
  belongs_to :perpetrator_status
  belongs_to :involvement_degree

  has_many :perpetrator_acts
end
