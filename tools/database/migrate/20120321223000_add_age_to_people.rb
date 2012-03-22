class AddAgeToPeople < ActiveRecord::Migration
  def self.up
    add_column :people, :age, :integer
  end

  def self.down
    remove_column :people, :age    
  end
end
