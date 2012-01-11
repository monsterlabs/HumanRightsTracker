class CreateDocuments < ActiveRecord::Migration
  def self.up
    create_table :documents do |t|
      t.binary :content
      t.integer :documentable_id
      t.string :documentable_type
      t.string :content_type

      t.timestamps
    end
  end

  def self.down
    drop_table :documents
  end
end
