class Identification < ActiveRecord::Base
  belongs_to :person
  belongs_to :identification_type
end
