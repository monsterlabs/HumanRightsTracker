class CreateCases < ActiveRecord::Migration
  def self.up
    create_table :cases do |t|
      t.string :name, :null => false
      t.date  :start_date, :null => false
      t.references :start_date_type, :class => :date_type
      t.date  :end_date
      t.references :end_date_type, :class => :date_type
      t.integer :affected_persons
    end
  end

  def self.down
    drop_table :cases
  end
end
