class CreateHumanRightsViolationCategories < ActiveRecord::Migration
  def self.up
    create_table :human_rights_violation_categories do |t|
      t.string :name, :null => false
    end
  end

  def self.down
    drop_table :human_rights_violations
  end
end
