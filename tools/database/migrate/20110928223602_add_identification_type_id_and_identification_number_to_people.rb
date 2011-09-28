class AddIdentificationTypeIdAndIdentificationNumberToPeople < ActiveRecord::Migration
  def self.up
    add_column :people, :identification_type_id, :integer
    add_column :people, :identification_number, :string
  end

  def self.down
    remove_column :people, :identification_number
    remove_column :people, :identification_type_id
  end
end
