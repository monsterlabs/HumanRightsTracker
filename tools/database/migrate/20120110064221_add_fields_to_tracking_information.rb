class AddFieldsToTrackingInformation < ActiveRecord::Migration
  def self.up
    remove_column :tracking_information, :records
    add_column :tracking_information, :record_id, :integer
    add_column :tracking_information, :title, :string
    add_column :cases, :record_count, :integer, :default => 0
  end

  def self.down
    add_column :tracking_information, :records, :string
    remove_column :tracking_information, :record_id
    remove_column :tracking_information, :title
    remove_column :cases, :record_count
  end
end
