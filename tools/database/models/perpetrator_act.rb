class PerpetratorAct < ActiveRecord::Base
  belongs_to :perpetrator
  belongs_to :human_right_violation
end
