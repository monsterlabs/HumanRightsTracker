class CreateVictimStatuses < ActiveRecord::Migration
  def self.up
    create_table :victim_statuses do |t|
      t.string :name

      t.timestamps
    end
  end

  def self.down
    drop_table :victim_statuses
  end
end
