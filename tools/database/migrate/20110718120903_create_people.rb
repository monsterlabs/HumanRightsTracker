class CreatePeople < ActiveRecord::Migration
  def self.up
    create_table :people do |t|
      t.string :firstname, :lastname, :null => false
      t.boolean :gender, :null => false
      t.date :birthday
      t.references :marital_status, :null => false
      t.references :country, :state, :city
    end
  end

  def self.down
    drop_table :people
  end
end

