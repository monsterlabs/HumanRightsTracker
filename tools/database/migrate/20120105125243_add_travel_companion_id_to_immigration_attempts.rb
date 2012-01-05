class AddTravelCompanionIdToImmigrationAttempts < ActiveRecord::Migration
  def self.up
    add_column :immigration_attempts, :travel_companion_id, :integer
  end

  def self.down
    remove_column :immigration_attempts, :travel_companion_id
  end
end
