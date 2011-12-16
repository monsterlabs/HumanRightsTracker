class AddFieldsToCases < ActiveRecord::Migration
  def self.up
    add_column :cases, :narrative_description, :text
    add_column :cases, :summary, :text
    add_column :cases, :observations, :text
  end
  
  def self.down
    remove_column :cases, :narrative_description
    remove_column :cases, :summary
    remove_column :cases, :observations
  end
end