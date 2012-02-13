class CreatePersonRelationshipTypes < ActiveRecord::Migration
  def self.up
    create_table :person_relationship_types do |t|
      t.string :name, :null => false
      t.text   :notes
    end
  end

  def self.down
    drop_table :person_relationship_types
  end
end
