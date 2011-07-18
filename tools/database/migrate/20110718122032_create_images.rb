class CreateImages < ActiveRecord::Migration
  def self.up
    create_table :images do |t|
      t.binary :original
      t.binary :thumbnail
      t.integer :imageable_id
      t.string :imageable_type
    end
  end

  def self.down
    drop_table :images
  end
end
