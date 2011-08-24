class AddHumanRightsViolationCategoryIdToActs < ActiveRecord::Migration
  def self.up
    add_column :acts, :human_rights_violation_category_id, :integer
  end

  def self.down
    remove_column :acts, :human_rights_violation_category_id
  end
end
