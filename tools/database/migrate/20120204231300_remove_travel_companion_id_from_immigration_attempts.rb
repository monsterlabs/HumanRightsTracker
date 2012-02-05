class RemoveTravelCompanionIdFromImmigrationAttempts < ActiveRecord::Migration
   def self.up
    remove_column :immigration_attempts, :travel_companion_id
   end

   def self.down
     add_column :immigration_attempts, :travel_companion_id, :integer
   end
end
