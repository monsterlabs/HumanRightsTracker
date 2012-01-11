class TrackingInformation < ActiveRecord::Base
  belongs_to :case
  has_one :document, :as => :documentable, :dependent => :destroy
end
