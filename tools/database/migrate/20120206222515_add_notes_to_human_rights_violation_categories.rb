class AddNotesToHumanRightsViolationCategories < ActiveRecord::Migration
  def self.up
    add_column :human_rights_violation_categories, :notes, :text
  end

  def self.down
    remove_column :human_rights_violation_categories, :notes
  end
end
