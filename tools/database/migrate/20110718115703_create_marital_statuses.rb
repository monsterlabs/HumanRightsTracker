class CreateMaritalStatuses < ActiveRecord::Migration
  def self.up
    create_table :marital_statuses do |t|
      t.string :name, :null => false
    end
  end

  def self.down
    drop_table :marital_statuses
  end
end
