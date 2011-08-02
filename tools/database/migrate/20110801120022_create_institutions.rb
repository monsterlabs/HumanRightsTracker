class CreateInstitutions < ActiveRecord::Migration
  def self.up
    create_table :institutions do |t|
      t.string :name, :null => false
      t.string :abbrev, :location, :phone, :fax, :url
      t.references :institution_type
    end
  end

  def self.down
    drop_table :institutions
  end
end
