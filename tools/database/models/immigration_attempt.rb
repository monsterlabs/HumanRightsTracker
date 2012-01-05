class ImmigrationAttempt < ActiveRecord::Base
  belongs_to :person
  belongs_to :traveling_reason
  belongs_to :destination_country, :class_name => 'Country'
  belongs_to :transit_country, :class_name => 'Country'
  belongs_to :travel_companion
end
