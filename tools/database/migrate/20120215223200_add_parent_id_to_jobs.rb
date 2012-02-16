class AddParentIdToJobs < ActiveRecord::Migration
  def self.up
    add_column :jobs, :parent_id, :integer

  end

  def self.down
    remove_column :jobs, :parent_id
  end
end
