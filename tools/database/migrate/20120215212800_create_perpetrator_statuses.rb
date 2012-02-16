class CreatePerpetratorStatuses < ActiveRecord::Migration
  def self.up
    create_table :perpetrator_statuses do |t|
      t.string :name
      t.text   :notes
      t.timestamps
    end
  end

  def self.down
    drop_table :perpetrator_statuses
  end
end
