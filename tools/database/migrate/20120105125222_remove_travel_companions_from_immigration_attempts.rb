class RemoveTravelCompanionsFromImmigrationAttempts < ActiveRecord::Migration
   def self.up
    remove_column :immigration_attempts, :travel_companions
   end

   def self.down
     add_column :immigration_attempts, :travel_companions, :integer
   end
end
