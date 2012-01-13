class TrackingInformation < ActiveRecord::Base
  belongs_to :case
  has_many :documents, :as => :documentable, :dependent => :destroy
end
