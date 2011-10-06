class CreateVictims < ActiveRecord::Migration
  def self.up
    create_table :victims do |t|
      t.references :person, :act
    end
  end

  def self.down
    drop_table :victims
  end
end
