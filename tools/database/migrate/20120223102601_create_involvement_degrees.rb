class CreateInvolvementDegrees < ActiveRecord::Migration
  def self.up
    create_table :involvement_degrees do |t|
      t.string :name
      t.text :notes
      t.references :parent
      t.timestamps
    end
  end

  def self.down
    drop_table :involvement_degrees
  end
end
