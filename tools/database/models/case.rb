class Case < ActiveRecord::Base
  has_many :acts
  has_many :interventions
end
