class CreatePerpetratorTypes < ActiveRecord::Migration
  def self.up
    create_table :perpetrator_types do |t|
      t.string :name
      t.text :notes
      t.references :parent
      t.timestamps
    end
  end

  def self.down
    drop_table :perpetrator_types
  end
end
