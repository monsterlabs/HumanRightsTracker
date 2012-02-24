class CreateStayTypes < ActiveRecord::Migration
  def self.up
    create_table :stay_types do |t|
      t.string :name, :null => false
      t.text   :notes
    end
  end

  def self.down
    drop_table :stay_types
  end
end
