class AddNotesToVictimStatuses < ActiveRecord::Migration
  def self.up
    add_column :victim_statuses, :notes, :text
  end

  def self.down
    remove_column :victim_statuses, :notes
  end
end
