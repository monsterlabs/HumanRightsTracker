class AddDocumentarySourceTypeIdToDocumentarySources < ActiveRecord::Migration
  def self.up
    add_column :documentary_sources, :documentary_source_type_id, :integer
  end

  def self.down
    remove_column :documentary_sources, :documentary_source_type_id 
  end
end
