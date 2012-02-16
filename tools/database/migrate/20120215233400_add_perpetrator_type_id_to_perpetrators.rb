class AddPerpetratorTypeIdToPerpetrators < ActiveRecord::Migration
  def self.up
    add_column :perpetrators, :perpetrator_type_id, :integer
  end

  def self.down
    remove_column :perpetrators, :perpetrator_type_id
  end
end
