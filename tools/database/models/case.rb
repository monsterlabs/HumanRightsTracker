class Case < ActiveRecord::Base
  has_many :acts
  has_many :interventions
  has_many :information_sources
end