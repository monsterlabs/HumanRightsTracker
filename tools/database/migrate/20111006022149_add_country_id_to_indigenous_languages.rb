class AddCountryIdToIndigenousLanguages < ActiveRecord::Migration
  def self.up
    add_column :indigenous_languages, :country_id, :integer
  end

  def self.down
    remove_column :indigenous_languages, :country_id
  end
end
