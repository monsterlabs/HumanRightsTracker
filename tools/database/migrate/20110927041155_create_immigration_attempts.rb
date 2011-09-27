class CreateImmigrationAttempts < ActiveRecord::Migration
  def self.up
    create_table :immigration_attempts do |t|
      t.references :person
      t.references :traveling_reason
      t.references :destination_country, :class_name => 'Country'
      t.references :transit_country, :class_name => 'Country'
      t.integer :cross_border_attempts_transit_country
      t.integer :expulsions_from_destination_country
      t.integer :expulsions_from_transit_country

      t.timestamps
    end
  end

  def self.down
    drop_table :immigration_attempts
  end
end
