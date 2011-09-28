class Person < ActiveRecord::Base
  belongs_to :marital_status
  belongs_to :country
  belongs_to :state
  belongs_to :city
  belongs_to :identification_type
  has_one :person_detail
  has_one :immigration_attempt
  has_one :address
  has_one :image, :as => :imageable, :dependent => :destroy
end
