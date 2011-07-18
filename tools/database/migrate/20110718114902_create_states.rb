class CreateStates < ActiveRecord::Migration
  def self.up
    create_table :states do |t|
      t.string :name, :null => false
      t.references :country
    end
  end

  def self.down
    drop_table :states
  end
end
