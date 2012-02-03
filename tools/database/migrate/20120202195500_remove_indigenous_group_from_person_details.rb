class RemoveIndigenousGroupFromPersonDetails < ActiveRecord::Migration
   def self.up
    remove_column :person_details, :indigenous_group
   end

   def self.down
     add_column :person_details, :indigenous_group, :integer
   end
end
