class CreateInstitutionTypes < ActiveRecord::Migration
  def self.up  
    create_table :institution_types do |t|
      t.string :name, :null => false
      t.text :notes
    end
  end

  def self.down
    drop_table :institution_types
  end
end
