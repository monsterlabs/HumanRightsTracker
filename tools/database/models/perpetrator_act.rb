class PerpetratorAct < ActiveRecord::Base
  belongs_to :perpetrator
  belongs_to :human_right_violation
  belongs_to :act_place
end
