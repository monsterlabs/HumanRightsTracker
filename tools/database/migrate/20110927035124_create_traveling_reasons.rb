class CreateTravelingReasons < ActiveRecord::Migration
  def self.up
    create_table :traveling_reasons do |t|
      t.string :name

      t.timestamps
    end
  end

  def self.down
    drop_table :traveling_reasons
  end
end
