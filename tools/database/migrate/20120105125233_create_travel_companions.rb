class CreateTravelCompanions < ActiveRecord::Migration
  def self.up
    create_table :travel_companions do |t|
      t.string :name
      t.timestamps
    end
  end
  def self.down
    drop_table :travel_companions
  end
end
