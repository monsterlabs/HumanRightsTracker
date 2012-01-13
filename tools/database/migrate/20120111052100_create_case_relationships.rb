class CreateCaseRelationships < ActiveRecord::Migration
  def self.up
    create_table :case_relationships do |t|
      t.integer :case_id
      t.integer :relationship_type_id
      t.integer :related_case_id
    end
  end

  def self.down
    drop_table :case_relationships
  end
end
