class CreateActs < ActiveRecord::Migration
  def self.up
    create_table :acts do |t|
      t.references :case, :human_rights_violation, :null => :false
      # TODO: Review date
      t.date  :start_date, :null => false
      t.references :start_date_type, :class => :date_type
      t.date  :end_date
      t.references :end_date_type, :class => :date_type
    end
  end

  def self.down
    drop_table :acts
  end
end
