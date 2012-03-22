class CreateDocumentarySourceTypes < ActiveRecord::Migration
  def self.up
    create_table :documentary_source_types do |t|
      t.string :name
      t.integer :parent_id
      t.text    :notes
      t.timestamps
    end
  end
  
  def self.down
    drop_table :documentary_source_types
  end
end
