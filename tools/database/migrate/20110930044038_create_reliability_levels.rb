class CreateReliabilityLevels < ActiveRecord::Migration
  def self.up
    create_table :reliability_levels do |t|
      t.string :name
      t.text :notes

      t.timestamps
    end
  end

  def self.down
    drop_table :reliability_levels
  end
end
