class AddIndigenousGroupIdToPersonDetails < ActiveRecord::Migration
  def self.up
    add_column :person_details, :indigenous_group_id, :integer
  end

  def self.down
    remove_column :person_details, :indigenous_group_id
  end
end
