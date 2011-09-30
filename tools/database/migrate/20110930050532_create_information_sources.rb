class CreateInformationSources < ActiveRecord::Migration
  def self.up
    create_table :information_sources do |t|
      t.references :case
      t.integer :information_sourceable_id
      t.string :information_sourceable_type
      t.integer :reported_personable_id
      t.string :reported_personable_type
      t.references :affiliation_type
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
