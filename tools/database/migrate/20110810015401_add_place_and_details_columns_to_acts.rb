class AddPlaceAndDetailsColumnsToActs < ActiveRecord::Migration
  def self.up
    add_column :acts, :country_id, :integer
    add_column :acts, :state_id, :integer
    add_column :acts, :city_id, :integer
    add_column :acts, :settlement, :string
    add_column :acts, :affected_people_number, :integer
    add_column :acts, :summary, :text
    add_column :acts, :narrative_information, :text
    add_column :acts, :comments, :text
  end

  def self.down
    remove_column :acts, :comments
    remove_column :acts, :narrative_information
    remove_column :acts, :summary
    remove_column :acts, :affected_people_number
    remove_column :acts, :settlement
    remove_column :acts, :city_id
    remove_column :acts, :state_id
    remove_column :acts, :country_id
  end
end
