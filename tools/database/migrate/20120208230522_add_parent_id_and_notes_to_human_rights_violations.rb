class AddParentIdAndNotesToHumanRightsViolations < ActiveRecord::Migration
  def self.up
    add_column :human_rights_violations, :parent_id, :integer
    add_column :human_rights_violations, :notes, :text
  end

  def self.down
    remove_column :human_rights_violations, :parent_id
    remove_column :human_rights_violations, :notes
  end
end
