class AddTimeSpentInDestinationCountry < ActiveRecord::Migration
  def self.up
    add_column :immigration_attempts, :time_spent_in_destination_country, :string
  end

  def self.down
    remove_column :immigration_attempt, :time_spent_in_destination_country
  end
end
