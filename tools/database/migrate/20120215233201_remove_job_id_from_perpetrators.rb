class RemoveJobIdFromPerpetrators < ActiveRecord::Migration
   def self.up
    remove_column :perpetrators, :job_id
   end

   def self.down
     add_column :perpetrators, :job_id, :integer
   end
end
