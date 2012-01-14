class AddParentIdToHumanRightsViolationCategories < ActiveRecord::Migration
  def self.up
    add_column :human_rights_violation_categories, :parent_id, :integer

  end

  def self.down
    remove_column :human_rights_violation_categories, :parent_id
  end
end
