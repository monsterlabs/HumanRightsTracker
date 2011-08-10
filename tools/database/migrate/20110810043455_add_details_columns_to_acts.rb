class AddDetailsColumnsToActs < ActiveRecord::Migration
  def self.up
    add_column :acts, :act_status_id, :integer
    add_column :acts, :victim_status_id, :integer
    add_column :acts, :affiliation_type_id, :integer
    add_column :acts, :affiliation_group, :string
    add_column :acts, :location_type_id, :integer
    add_column :acts, :victim_observations, :string
  end

  def self.down
    remove_column :acts, :victim_observations
    remove_column :acts, :location_type_id
    remove_column :acts, :affiliation_group
    remove_column :acts, :affiliation_type_id
    remove_column :acts, :victim_status_id
    remove_column :acts, :act_status_id
  end
end
