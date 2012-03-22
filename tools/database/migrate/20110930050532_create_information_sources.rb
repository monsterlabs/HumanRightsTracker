class CreateInformationSources < ActiveRecord::Migration
  def self.up
    create_table :information_sources do |t|
      t.references :case

      t.integer  :source_person_id
      t.integer  :source_institution_id
      t.integer  :source_affiliation_type_id

      t.integer  :reported_person_id
      t.integer  :reported_institution_id
      t.integer  :reported_affiliation_type_id

      t.references :source_information_type
      t.references :date_type
      t.date :date
      t.references :language
      t.references :indigenous_language
      t.text :observations
      t.references :reliability_level
      t.text :comments

      t.timestamps
    end
  end

  def self.down
    drop_table :information_sources
  end
end
