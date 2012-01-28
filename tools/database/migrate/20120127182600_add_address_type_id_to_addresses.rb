class AddAddressTypeIdToAddresses < ActiveRecord::Migration
  def self.up
    add_column :addresses, :address_type_id, :integer

  end

  def self.down
    remove_column :addresses, :address_type_id
  end
end
