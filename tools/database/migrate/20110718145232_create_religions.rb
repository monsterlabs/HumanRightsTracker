class CreateReligions < ActiveRecord::Migration
  def self.up
    create_table :religions do |t|
      t.string :name, :null => false
    end
  end

  def self.down
    drop_table :religions
  end
end
