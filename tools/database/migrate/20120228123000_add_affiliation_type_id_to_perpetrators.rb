class AddAffiliationTypeIdToPerpetrators < ActiveRecord::Migration
  def self.up
    add_column :perpetrators, :affiliation_type_id, :integer
  end

  def self.down
    remove_column :perpetrators, :affiliation_type_id
  end
end
