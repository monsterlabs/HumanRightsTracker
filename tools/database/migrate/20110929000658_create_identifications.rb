class CreateIdentifications < ActiveRecord::Migration
  def self.up
    create_table :identifications do |t|
      t.references :person
      t.references :identification_type
      t.string :identification_number

      t.timestamps
    end
  end

  def self.down
    drop_table :identifications
  end
end
