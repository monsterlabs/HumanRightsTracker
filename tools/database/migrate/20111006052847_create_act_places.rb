class CreateActPlaces < ActiveRecord::Migration
  def self.up
    create_table :act_places do |t|
      t.string :name

      t.timestamps
    end
  end

  def self.down
    drop_table :act_places
  end
end
