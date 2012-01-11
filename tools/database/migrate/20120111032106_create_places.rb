class CreatePlaces < ActiveRecord::Migration
  def self.up
    create_table :places do |t|
      t.integer :case_id
      t.integer :country_id
      t.integer :state_id
      t.integer :city_id

      t.timestamps
    end
  end

  def self.down
    drop_table :places
  end
end
