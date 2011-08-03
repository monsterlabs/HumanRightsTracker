class CreatePersonActs < ActiveRecord::Migration
  def self.up
    create_table :person_acts do |t|
      t.references :person, :act, :role
    end
  end

  def self.down
    drop_table :person_acts
  end
end
