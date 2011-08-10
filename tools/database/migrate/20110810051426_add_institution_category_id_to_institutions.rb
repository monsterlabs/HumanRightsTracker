class AddInstitutionCategoryIdToInstitutions < ActiveRecord::Migration
  def self.up
    add_column :institutions, :institution_category_id, :integer
  end

  def self.down
    remove_column :institutions, :institution_category_id
  end
end
