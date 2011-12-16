class CreateAdministrativeInformation < ActiveRecord::Migration
  def self.up
    create_table :case_statuses do |t|
      t.string :name
    end
    
    create_table :administrative_information do |t|
      t.integer :case_id
      t.integer :date_type_id
      t.date :date_of_receipt
      t.text :project_name
      t.text :project_description
      t.text :comments
      t.integer :case_status_id
      t.text :records
    end
  end
  
  def self.down
    drop_table :administrative_information
    drop_table :case_statuses
  end
end