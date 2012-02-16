class InformationSource < ActiveRecord::Base
  belongs_to :case
  belongs_to :source_person, :class_name => 'Person'
  belongs_to :source_institution, :class_name => 'Institution'
  belongs_to :source_affiliation_type, :class_name => 'AffiliationType'

  belongs_to :reported_person, :class_name => 'Person'
  belongs_to :reported_institution, :class_name => 'Institution'
  belongs_to :reported_affiliation_type, :class_name => 'AffiliationType'
  
  belongs_to :affiliation_type
  belongs_to :language
  belongs_to :indigenous_language
  belongs_to :reliability_level
end
