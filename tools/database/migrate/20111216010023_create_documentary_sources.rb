class CreateDocumentarySources < ActiveRecord::Migration
  def self.up
    create_table :documentary_sources do |t|
      t.references :case
      t.text :name
      t.text :additional_info
      t.date :date
      t.references :source_information_type
      t.text :site_name
      t.string :url
      t.date :access_date
      t.references :language
      t.references :indigenous_language
      t.text :observations
      t.integer :reported_person_id
      t.integer :reported_institution_id
      t.integer :reported_job_id
      t.references :reliability_level
      t.text :comments

      t.timestamps
    end
  end
  
  def self.down
    drop_table :documentary_sources
  end
end
