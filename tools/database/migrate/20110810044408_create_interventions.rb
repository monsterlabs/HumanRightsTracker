class CreateInterventions < ActiveRecord::Migration
  def self.up
    create_table :interventions do |t|
      t.integer :intervention_type_id
      t.date :date
      t.integer :person_id
      t.integer :supporter_id
      t.text :impact
      t.integer :case_id
      t.timestamps
    end
  end

  def self.down
    drop_table :interventions
  end
end
