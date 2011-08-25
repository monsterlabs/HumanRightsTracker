class Person < ActiveRecord::Base
  belongs_to :marital_status
  belongs_to :country
  belongs_to :state
  belongs_to :city
  has_one :person_detail
  has_one :image, :as => :imageable, :dependent => :destroy
end