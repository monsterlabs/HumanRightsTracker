class AddParentIdAndNotesToActPlaces < ActiveRecord::Migration
  def self.up
    add_column :act_places, :parent_id, :integer
    add_column :act_places, :notes, :text
  end

  def self.down
    remove_column :act_places, :parent_id
    remove_column :act_places, :notes
  end
end
