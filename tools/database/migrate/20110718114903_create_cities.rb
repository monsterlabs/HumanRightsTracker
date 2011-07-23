class CreateCities < ActiveRecord::Migration
  def self.up
    create_table :cities do |t|
      t.string :name, :null => false
      t.references :state
    end
  end

  def self.down
    drop_table :cities
  end
end
