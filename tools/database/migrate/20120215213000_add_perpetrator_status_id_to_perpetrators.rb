class AddPerpetratorStatusIdToPerpetrators < ActiveRecord::Migration
  def self.up
    add_column :perpetrators, :perpetrator_status_id, :integer
  end

  def self.down
    remove_column :perpetrators, :perpetrator_status_id
  end
end
