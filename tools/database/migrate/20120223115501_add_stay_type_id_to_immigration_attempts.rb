class AddStayTypeIdToImmigrationAttempts < ActiveRecord::Migration
  def self.up
    add_column :immigration_attempts, :stay_type_id, :integer
  end

  def self.down
    remove_column :immigration_attempts, :stay_type_id
  end
end
