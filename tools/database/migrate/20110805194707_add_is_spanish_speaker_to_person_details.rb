class AddIsSpanishSpeakerToPersonDetails < ActiveRecord::Migration
  def self.up
    add_column :person_details, :is_spanish_speaker, :boolean
  end

  def self.down
    remove_column :person_details, :is_spanish_speaker
  end
end
