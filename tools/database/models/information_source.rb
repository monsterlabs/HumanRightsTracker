class InformationSource < ActiveRecord::Base
  belongs_to :case
  belongs_to :affiliation_type
  belongs_to :language
  belongs_to :indigenous_language
  belongs_to :reliability_level
end
