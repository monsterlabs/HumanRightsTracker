class CreatePerpetratorActs < ActiveRecord::Migration
  def self.up
    create_table :perpetrator_acts do |t|
      t.references :perpetrator
      t.references :human_rights_violation
      t.references :act_place
      t.string :location
      t.timestamps
    end
  end

  def self.down
    drop_table :perpetrator_acts
  end
end
