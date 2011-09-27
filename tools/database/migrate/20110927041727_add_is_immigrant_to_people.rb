class AddIsImmigrantToPeople < ActiveRecord::Migration
  def self.up
    add_column :people, :is_immigrant, :boolean
  end

  def self.down
    remove_column :people, :is_immigrant
  end
end
