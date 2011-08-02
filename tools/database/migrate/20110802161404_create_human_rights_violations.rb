class CreateHumanRightsViolations < ActiveRecord::Migration
  def self.up
    create_table :human_rights_violations do |t|
      t.string :name, :null => false
      t.references :parent
    end
  end

  def self.down
    drop_table :human_rights_violations
  end
end
