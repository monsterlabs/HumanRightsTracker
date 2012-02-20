class CreateInstitutionRelationships < ActiveRecord::Migration
  def self.up
    create_table :institution_relationships do |t|
      t.integer :institution_id
      t.integer :related_institution_id
      t.integer :institution_relationship_type_id      
      t.date  :start_date, :null => false
      t.references :start_date_type, :class => :date_type
      t.date  :end_date
      t.references :end_date_type, :class => :date_type
      t.text :comments
    end
  end

  def self.down
    drop_table :institution_relationships
  end
end
