class Case < ActiveRecord::Base
  has_many :acts
  has_many :interventions
  has_many :information_sources
  has_many :tracking_information
  has_many :documentary_sources
end
