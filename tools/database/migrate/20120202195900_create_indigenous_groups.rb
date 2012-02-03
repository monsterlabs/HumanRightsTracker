class CreateIndigenousGroups < ActiveRecord::Migration
  def self.up
    create_table :indigenous_groups do |t|
      t.string :name
      t.string :notes
      t.timestamps
    end
  end

  def self.down
    drop_table :indigenous_groups
  end
end
