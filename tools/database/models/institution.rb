class Institution < ActiveRecord::Base
  belongs_to :country
  belongs_to :state
  belongs_to :city
  belongs_to :institution_type
  belongs_to :institution_category
  has_one :image, :as => :imageable, :dependent => :destroy
end