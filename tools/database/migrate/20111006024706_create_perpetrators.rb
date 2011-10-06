class CreatePerpetrators < ActiveRecord::Migration
  def self.up
    create_table :perpetrators do |t|
      t.references :victim
      t.references :person
      t.references :institution
      t.references :job

      t.timestamps
    end
  end

  def self.down
    drop_table :perpetrators
  end
end
