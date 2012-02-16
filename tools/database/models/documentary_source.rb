class DocumentarySource < ActiveRecord::Base
  belongs_to :case
  belongs_to :source_information_type
  belongs_to :language
  belongs_to :indigenous_language
  belongs_to :reported_person, :class_name => 'Person'
  belongs_to :reported_affiliation_type, :class_name => 'AffiliationType'
  belongs_to :reported_institution, :class_name => 'Institution'
  belongs_to :reliability_level
end
