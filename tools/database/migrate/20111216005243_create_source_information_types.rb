  def self.up
class CreateSourceInformationTypes < ActiveRecord::Migration
    create_table :source_information_types do |t|
      t.string :name
      t.integer :parent_id
      t.text    :notes

      t.timestamps
    end
  end
  
  def self.down
    drop_table :source_information_types
  end
end
