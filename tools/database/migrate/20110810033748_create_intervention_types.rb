class CreateInterventionTypes < ActiveRecord::Migration
  def self.up
    create_table :intervention_types do |t|
      t.string :name

      t.timestamps
    end
  end

  def self.down
    drop_table :intervention_types
  end
end
