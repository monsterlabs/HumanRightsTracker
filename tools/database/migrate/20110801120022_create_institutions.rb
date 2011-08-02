class CreateInstitutions < ActiveRecord::Migration
  def self.up
    create_table :institutions do |t|
      t.string :name, :null => false
      t.string :abbrev, :location, :phone, :fax, :url, :email
      t.references :institution_type
      t.references :country, :null => false
      t.references :state, :city
    end
  end

  def self.down
    drop_table :institutions
  end
end
