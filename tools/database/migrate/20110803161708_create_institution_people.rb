class CreateInstitutionPeople < ActiveRecord::Migration
  def self.up
    create_table :institution_people do |t|
      t.references :person, :institution
    end
  end

  def self.down
    drop_table :institution_people
  end
end
