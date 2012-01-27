class CreateIndigenousLanguages < ActiveRecord::Migration
  def self.up
    create_table :indigenous_languages do |t|
      t.string :name
      t.text :notes
      t.timestamps
    end
  end

  def self.down
    drop_table :indigenous_languages
  end
end
