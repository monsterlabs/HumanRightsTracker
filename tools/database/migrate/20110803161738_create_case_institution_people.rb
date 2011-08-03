class CreateCaseInstitutionPeople < ActiveRecord::Migration
  def self.up
    create_table :case_institution_people do |t|
      t.references :case, :institution_person
    end
  end

  def self.down
    drop_table :case_institution_people
  end
end
