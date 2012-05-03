class AddCommentsToPeople < ActiveRecord::Migration
  def self.up
    add_column :people, :comments, :text
  end

  def self.down
    remove_column :people, :comments
  end
end
