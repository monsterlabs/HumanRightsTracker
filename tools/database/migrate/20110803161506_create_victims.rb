class CreateVictims < ActiveRecord::Migration
  def self.up
    create_table :victims do |t|
      t.references :person, :act
      t.string :characteristics 
      t.references :victim_status
    end
  end

  def self.down
    drop_table :victims
  end
end
