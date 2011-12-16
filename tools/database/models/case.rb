class Case < ActiveRecord::Base
  has_many :acts
  has_many :interventions
  has_many :information_sources
  has_many :administrative_information
end
