class CreateInterventions < ActiveRecord::Migration
  def self.up
    create_table :interventions do |t|
      t.references :intervention_type
      t.date :date
      t.integer :interventor_id
      t.integer :interventor_institution_id
      t.integer :interventor_job_id
      t.integer :supporter_id
      t.integer :supporter_institution_id
      t.integer :supporter_job_id
      t.text :impact
      t.text :response
      t.integer :case_id
      t.timestamps
    end
  end

  def self.down
    drop_table :interventions
  end
end
