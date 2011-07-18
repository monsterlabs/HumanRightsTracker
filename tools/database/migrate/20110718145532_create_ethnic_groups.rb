class CreateEthnicGroups < ActiveRecord::Migration
  def self.up
    create_table :ethnic_groups do |t|
      t.string :name, :null => false
    end
  end

  def self.down
    drop_table :ethnic_groups
  end
end
