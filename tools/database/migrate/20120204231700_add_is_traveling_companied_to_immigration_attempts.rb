class AddIsTravelingCompaniedToImmigrationAttempts < ActiveRecord::Migration
  def self.up
    add_column :immigration_attempts, :is_traveling_companied, :bool
  end

  def self.down
    remove_column :immigration_attempts, :is_traveling_companied
  end
end
