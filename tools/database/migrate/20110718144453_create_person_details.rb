class CreatePersonDetails < ActiveRecord::Migration
  def self.up
    create_table :person_details do |t|
      t.references :person
      t.integer :number_of_children
      t.references :ethnic_group, :religion, :scholarity_level
      t.string :indigenous_group, :most_recent_job
    end
  end

  def self.down
    drop_table :person_details
  end
end
