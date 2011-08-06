class AddCategoryToHumanRightsViolation < ActiveRecord::Migration
  def self.up
    add_column :human_rights_violations, :category_id, :integer
  end

  def self.down
    remove_column :human_rights_violations, :category_id
  end
end