class AddFieldsToImmigrationAttempts < ActiveRecord::Migration
  def self.up
    add_column :immigration_attempts, :origin_country_id, :integer
    add_column :immigration_attempts, :origin_state_id, :integer
    add_column :immigration_attempts, :origin_city_id, :integer
    add_column :immigration_attempts, :travel_companions, :integer
    add_column :immigration_attempts, :cross_border_attempts_destination_country, :integer
  end
  
  def self.down
    remove_column :immigration_attempts, :origin_country_id
    remove_column :immigration_attempts, :origin_state_id
    remove_column :immigration_attempts, :origin_city_id
    remove_column :immigration_attempts, :travel_companions
    remove_column :immigration_attempts, :cross_border_attempts_destination_country
  end
end