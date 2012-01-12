class AddFilenameToDocument < ActiveRecord::Migration
  def self.up
    add_column :documents, :filename, :string
  end

  def self.down
    remove_column :documents, :filename
  end
end
