class CreateInterventionAffectedPeople < ActiveRecord::Migration
  def self.up
    create_table :intervention_affected_people do |t|
      t.references :intervention
      t.references :person
      t.timestamps
    end
  end

  def self.down
    drop_table :intervention_affected_people
  end
end
