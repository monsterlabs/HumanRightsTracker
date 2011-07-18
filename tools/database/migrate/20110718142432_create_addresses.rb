class CreateAddresses < ActiveRecord::Migration
  def self.up
    create_table :addresses do |t|
      t.string :location, :null => false
      t.references :country, :state, :city
      t.references :person
      t.string :phone, :mobile, :zipcode
    end
  end

  def self.down
    drop :addresses
  end
end
