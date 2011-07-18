class CreateScholarityLevels < ActiveRecord::Migration
  def self.up
    create_table :scholarity_levels do |t|
      t.string :name, :null => false
    end
  end

  def self.down
    drop_table :scholarity_levels
  end
end
