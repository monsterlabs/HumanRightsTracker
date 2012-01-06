class RenameAdministrativeInformationToTrackingInformation < ActiveRecord::Migration
  def self.up
    rename_table :administrative_information, :tracking_information
    remove_column :tracking_information, :project_name
    remove_column :tracking_information, :project_description
  end

  def self.down
    rename_table :tracking_information, :administrative_information
    add_column :administrative_information, :project_name
    add_column :administrative_information, :project_description
  end
end
