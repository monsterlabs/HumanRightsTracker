class CreatePersonRelationships < ActiveRecord::Migration
  def self.up
    create_table :person_relationships do |t|
      t.integer :person_id
      t.integer :related_person_id
      t.integer :person_relationship_type_id      
      t.date  :start_date, :null => false
      t.references :start_date_type, :class => :date_type
      t.date  :end_date
      t.references :end_date_type, :class => :date_type
      t.text :comments
    end
  end

  def self.down
    drop_table :case_relationships
  end
end
