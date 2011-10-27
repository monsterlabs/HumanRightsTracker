class AddCitizenIdToPeople < ActiveRecord::Migration
  def self.up
    add_column :people, :citizen_id, :integer
  end

  def self.down
    remove_column :people, :citizen_id
  end
end
