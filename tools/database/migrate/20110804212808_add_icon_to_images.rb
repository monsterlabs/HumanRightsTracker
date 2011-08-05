class AddIconToImages < ActiveRecord::Migration
  def self.up
    add_column :images, :icon, :binary
  end

  def self.down
    remove_column :images, :icon
  end
end