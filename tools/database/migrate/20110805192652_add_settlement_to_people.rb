class AddSettlementToPeople < ActiveRecord::Migration
  def self.up
    add_column :people, :settlement, :string
  end

  def self.down
    remove_column :people, :settlement
  end
end
