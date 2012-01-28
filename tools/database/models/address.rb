class Address < ActiveRecord::Base
  belongs_to :person
  belongs_to :country
  belongs_to :state
  belongs_to :city
  belongs_to :address_type
end