  def self.up
class CreateRelationshipTypes < ActiveRecord::Migration
    create_table :relationship_types do |t|
      t.string :name, :null => false
      t.text   :notes
    end
  end

  def self.down
    drop_table :relationship_types
  end
end
