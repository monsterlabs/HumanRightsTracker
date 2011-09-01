class AddZipcodeToInstitutions < ActiveRecord::Migration
  def self.up
    add_column :institutions, :zipcode, :integer
  end

  def self.down
    remove_column :institutions, :zipcode
  end
end
