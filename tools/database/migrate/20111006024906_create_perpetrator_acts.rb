class CreatePerpetratorActs < ActiveRecord::Migration
  def self.up
    create_table :perpetrator_acts do |t|
      t.references :perpetrator
      t.references :human_right_violation

      t.timestamps
    end
  end

  def self.down
    drop_table :perpetrator_acts
  end
end
