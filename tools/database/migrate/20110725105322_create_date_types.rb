class CreateDateTypes < ActiveRecord::Migration
  def self.up
    create_table :date_types do |t|
      t.string :name, :null => false
    end
  end

  def self.down
    drop_table :date_types
  end
end
