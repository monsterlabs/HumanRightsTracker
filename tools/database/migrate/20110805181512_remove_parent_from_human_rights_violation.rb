class RemoveParentFromHumanRightsViolation < ActiveRecord::Migration
  def self.up
    remove_column :human_rights_violations, :parent_id
  end

  def self.down
    add_column :human_rights_violations, :parent_id, :integer
  end
end