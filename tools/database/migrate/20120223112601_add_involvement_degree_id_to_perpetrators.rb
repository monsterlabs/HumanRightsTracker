class AddInvolvementDegreeIdToPerpetrators < ActiveRecord::Migration
  def self.up
    add_column :perpetrators, :involvement_degree_id, :integer
  end

  def self.down
    remove_column :perpetrators, :involvement_degree_id
  end
end
