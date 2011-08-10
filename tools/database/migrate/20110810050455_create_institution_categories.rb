class CreateInstitutionCategories < ActiveRecord::Migration
  def self.up
    create_table :institution_categories do |t|
      t.string :name

      t.timestamps
    end
  end

  def self.down
    drop_table :institution_categories
  end
end
