class AddAliasToPeople < ActiveRecord::Migration
  def self.up
    add_column :people, :alias, :string
  end

  def self.down
    remove_column :people, :alias
  end
end
