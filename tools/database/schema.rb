# encoding: UTF-8
# This file is auto-generated from the current state of the database. Instead
# of editing this file, please use the migrations feature of Active Record to
# incrementally modify your database, and then regenerate this schema definition.
#
# Note that this schema.rb definition is the authoritative source for your
# database schema. If you need to create the application database on another
# system, you should be using db:schema:load, not running all the migrations
# from scratch. The latter is a flawed and unsustainable approach (the more migrations
# you'll amass, the slower it'll run and the greater likelihood for issues).
#
# It's strongly recommended to check this file into your version control system.

ActiveRecord::Schema.define(:version => 20120110065715) do

  create_table "act_places", :force => true do |t|
    t.string   "name"
    t.datetime "created_at"
    t.datetime "updated_at"
  end

  create_table "act_statuses", :force => true do |t|
    t.string   "name"
    t.datetime "created_at"
    t.datetime "updated_at"
  end

  create_table "acts", :force => true do |t|
    t.integer "case_id"
    t.integer "human_rights_violation_id"
    t.date    "start_date",                         :null => false
    t.integer "start_date_type_id"
    t.date    "end_date"
    t.integer "end_date_type_id"
    t.integer "country_id"
    t.integer "state_id"
    t.integer "city_id"
    t.string  "settlement"
    t.integer "affected_people_number"
    t.text    "summary"
    t.text    "narrative_information"
    t.text    "comments"
    t.integer "act_status_id"
    t.integer "victim_status_id"
    t.integer "affiliation_type_id"
    t.string  "affiliation_group"
    t.integer "location_type_id"
    t.string  "victim_observations"
    t.integer "human_rights_violation_category_id"
  end

  create_table "addresses", :force => true do |t|
    t.string  "location",   :null => false
    t.integer "country_id"
    t.integer "state_id"
    t.integer "city_id"
    t.integer "person_id"
    t.string  "phone"
    t.string  "mobile"
    t.string  "zipcode"
  end

  create_table "affiliation_types", :force => true do |t|
    t.string   "name"
    t.text     "notes"
    t.integer  "parent_id"
    t.datetime "created_at"
    t.datetime "updated_at"
  end

  create_table "case_institution_people", :force => true do |t|
    t.integer "case_id"
    t.integer "institution_person_id"
  end

  create_table "case_statuses", :force => true do |t|
    t.string "name"
  end

  create_table "cases", :force => true do |t|
    t.string  "name",                  :null => false
    t.date    "start_date",            :null => false
    t.integer "start_date_type_id"
    t.date    "end_date"
    t.integer "end_date_type_id"
    t.integer "affected_persons"
    t.text    "narrative_description"
    t.text    "summary"
    t.text    "observations"
  end

  create_table "cities", :force => true do |t|
    t.string  "name",     :null => false
    t.integer "state_id"
  end

  create_table "countries", :force => true do |t|
    t.string "name"
    t.string "citizen"
    t.string "code"
  end

  create_table "date_types", :force => true do |t|
    t.string "name", :null => false
  end

  create_table "documentary_sources", :force => true do |t|
    t.integer  "case_id"
    t.string   "name"
    t.text     "additional_info"
    t.date     "date"
    t.integer  "source_information_type_id"
    t.string   "site_name"
    t.string   "url"
    t.date     "access_date"
    t.integer  "language_id"
    t.integer  "indigenous_language_id"
    t.text     "observations"
    t.integer  "reported_person_id"
    t.integer  "reported_institution_id"
    t.integer  "reported_job_id"
    t.integer  "reliability_level_id"
    t.text     "comments"
    t.datetime "created_at"
    t.datetime "updated_at"
  end

  create_table "documents", :force => true do |t|
    t.binary   "data"
    t.integer  "documentable_id"
    t.string   "documentable_type"
    t.string   "type"
    t.datetime "created_at"
    t.datetime "updated_at"
  end

  create_table "ethnic_groups", :force => true do |t|
    t.string "name", :null => false
  end

  create_table "human_rights_violation_categories", :force => true do |t|
    t.string "name", :null => false
  end

  create_table "human_rights_violations", :force => true do |t|
    t.string  "name",        :null => false
    t.integer "category_id"
  end

  create_table "identification_types", :force => true do |t|
    t.string   "name"
    t.datetime "created_at"
    t.datetime "updated_at"
  end

  create_table "identifications", :force => true do |t|
    t.integer  "person_id"
    t.integer  "identification_type_id"
    t.string   "identification_number"
    t.datetime "created_at"
    t.datetime "updated_at"
  end

  create_table "images", :force => true do |t|
    t.binary  "original"
    t.binary  "thumbnail"
    t.integer "imageable_id"
    t.string  "imageable_type"
    t.binary  "icon"
  end

  create_table "immigration_attempts", :force => true do |t|
    t.integer  "person_id"
    t.integer  "traveling_reason_id"
    t.integer  "destination_country_id"
    t.integer  "transit_country_id"
    t.integer  "cross_border_attempts_transit_country"
    t.integer  "expulsions_from_destination_country"
    t.integer  "expulsions_from_transit_country"
    t.datetime "created_at"
    t.datetime "updated_at"
    t.string   "time_spent_in_destination_country"
    t.integer  "origin_country_id"
    t.integer  "origin_state_id"
    t.integer  "origin_city_id"
    t.integer  "cross_border_attempts_destination_country"
    t.integer  "travel_companion_id"
  end

  create_table "indigenous_languages", :force => true do |t|
    t.string   "name"
    t.string   "notes"
    t.datetime "created_at"
    t.datetime "updated_at"
    t.integer  "country_id"
  end

  create_table "information_sources", :force => true do |t|
    t.integer  "case_id"
    t.integer  "source_person_id"
    t.integer  "source_institution_id"
    t.integer  "source_job_id"
    t.integer  "reported_person_id"
    t.integer  "reported_institution_id"
    t.integer  "reported_job_id"
    t.integer  "affiliation_type_id"
    t.integer  "date_type_id"
    t.date     "date"
    t.integer  "language_id"
    t.integer  "indigenous_language_id"
    t.text     "observations"
    t.integer  "reliability_level_id"
    t.text     "comments"
    t.datetime "created_at"
    t.datetime "updated_at"
  end

  create_table "institution_categories", :force => true do |t|
    t.string   "name"
    t.datetime "created_at"
    t.datetime "updated_at"
    t.text     "notes"
  end

  create_table "institution_people", :force => true do |t|
    t.integer "person_id"
    t.integer "institution_id"
  end

  create_table "institution_types", :force => true do |t|
    t.string "name", :null => false
  end

  create_table "institutions", :force => true do |t|
    t.string  "name",                    :null => false
    t.string  "abbrev"
    t.string  "location"
    t.string  "phone"
    t.string  "fax"
    t.string  "url"
    t.string  "email"
    t.integer "institution_type_id"
    t.integer "country_id",              :null => false
    t.integer "state_id"
    t.integer "city_id"
    t.integer "institution_category_id"
    t.integer "zipcode"
  end

  create_table "intervention_affected_people", :force => true do |t|
    t.integer  "intervention_id"
    t.integer  "person_id"
    t.datetime "created_at"
    t.datetime "updated_at"
  end

  create_table "intervention_types", :force => true do |t|
    t.string   "name"
    t.datetime "created_at"
    t.datetime "updated_at"
  end

  create_table "interventions", :force => true do |t|
    t.integer  "intervention_type_id"
    t.date     "date"
    t.integer  "interventor_id"
    t.integer  "interventor_institution_id"
    t.integer  "interventor_job_id"
    t.integer  "supporter_id"
    t.integer  "supporter_institution_id"
    t.integer  "supporter_job_id"
    t.text     "impact"
    t.text     "response"
    t.integer  "case_id"
    t.datetime "created_at"
    t.datetime "updated_at"
  end

  create_table "jobs", :force => true do |t|
    t.string   "name"
    t.text     "notes"
    t.datetime "created_at"
    t.datetime "updated_at"
  end

  create_table "languages", :force => true do |t|
    t.string   "name"
    t.datetime "created_at"
    t.datetime "updated_at"
  end

  create_table "location_types", :force => true do |t|
    t.string   "name"
    t.datetime "created_at"
    t.datetime "updated_at"
  end

  create_table "marital_statuses", :force => true do |t|
    t.string "name", :null => false
  end

  create_table "people", :force => true do |t|
    t.string  "firstname",         :null => false
    t.string  "lastname",          :null => false
    t.boolean "gender",            :null => false
    t.date    "birthday",          :null => false
    t.integer "marital_status_id", :null => false
    t.integer "country_id",        :null => false
    t.integer "state_id"
    t.integer "city_id"
    t.string  "settlement"
    t.string  "alias"
    t.boolean "is_immigrant"
    t.string  "email"
    t.integer "citizen_id"
  end

  create_table "perpetrator_acts", :force => true do |t|
    t.integer  "perpetrator_id"
    t.integer  "human_rights_violation_id"
    t.integer  "act_place_id"
    t.string   "location"
    t.datetime "created_at"
    t.datetime "updated_at"
  end

  create_table "perpetrators", :force => true do |t|
    t.integer  "victim_id"
    t.integer  "person_id"
    t.integer  "institution_id"
    t.integer  "job_id"
    t.datetime "created_at"
    t.datetime "updated_at"
  end

  create_table "person_details", :force => true do |t|
    t.integer "person_id"
    t.integer "number_of_sons"
    t.integer "ethnic_group_id"
    t.integer "religion_id"
    t.integer "scholarity_level_id"
    t.integer "job_id"
    t.string  "indigenous_group"
    t.boolean "is_spanish_speaker"
  end

  create_table "reliability_levels", :force => true do |t|
    t.string   "name"
    t.text     "notes"
    t.datetime "created_at"
    t.datetime "updated_at"
  end

  create_table "religions", :force => true do |t|
    t.string "name", :null => false
  end

  create_table "scholarity_levels", :force => true do |t|
    t.string "name", :null => false
  end

  create_table "source_information_types", :force => true do |t|
    t.string   "name"
    t.integer  "parent_id"
    t.datetime "created_at"
    t.datetime "updated_at"
  end

  create_table "states", :force => true do |t|
    t.string  "name",       :null => false
    t.integer "country_id"
  end

  create_table "tracking_information", :force => true do |t|
    t.integer "case_id"
    t.integer "date_type_id"
    t.date    "date_of_receipt"
    t.text    "comments"
    t.integer "case_status_id"
    t.integer "record_id",       :default => 0
    t.string  "title"
  end

  create_table "travel_companions", :force => true do |t|
    t.string   "name"
    t.datetime "created_at"
    t.datetime "updated_at"
  end

  create_table "traveling_reasons", :force => true do |t|
    t.string   "name"
    t.datetime "created_at"
    t.datetime "updated_at"
  end

  create_table "users", :force => true do |t|
    t.string "login",    :null => false
    t.string "password", :null => false
    t.string "salt",     :null => false
  end

  create_table "victim_statuses", :force => true do |t|
    t.string   "name"
    t.datetime "created_at"
    t.datetime "updated_at"
  end

  create_table "victims", :force => true do |t|
    t.integer "person_id"
    t.integer "act_id"
    t.string  "characteristics"
    t.integer "victim_status_id"
  end

end
