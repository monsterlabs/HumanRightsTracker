class AddNotesToInstitutionCategories < ActiveRecord::Migration
  def self.up
    add_column :institution_categories, :notes, :text
  end

  def self.down
    remove_column :institution_categories, :notes
  end
end
