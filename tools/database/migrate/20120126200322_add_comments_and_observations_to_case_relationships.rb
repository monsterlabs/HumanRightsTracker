class AddCommentsAndObservationsToCaseRelationships < ActiveRecord::Migration
  def self.up
    add_column :case_relationships, :comments, :text
    add_column :case_relationships, :observations, :text
  end

  def self.down
    remove_column :case_relationships, :comments
    remove_column :case_relationships, :observations
  end
end
