class AddAffiliationTypeIdToInstitutionPeople < ActiveRecord::Migration
  def self.up
    add_column :institution_people, :affiliation_type_id, :integer
    add_column :institution_people, :start_date, :date
    add_column :institution_people, :start_date_type_id, :integer
    add_column :institution_people, :end_date, :date
    add_column :institution_people, :end_date_type_id, :integer
    add_column :institution_people, :comments, :text
    
  end

  def self.down
    remove_column :institution_people, :affiliation_type_id
    remove_column :institution_people, :start_date
    remove_column :institution_people, :start_date_type_id
    remove_column :institution_people, :end_date
    remove_column :institution_people, :end_date_type_id 
    remove_column :institution_people, :comments      
  end
end
