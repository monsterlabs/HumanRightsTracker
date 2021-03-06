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

ActiveRecord::Schema.define(:version => 20120215233400) do

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

  create_table "address_types", :force => true do |t|
    t.string "name", :null => false
  end

  create_table "addresses", :force => true do |t|
    t.string  "location",        :null => false
    t.integer "country_id"
    t.integer "state_id"
    t.integer "city_id"
    t.integer "person_id"
    t.string  "phone"
    t.string  "mobile"
    t.string  "zipcode"
    t.integer "address_type_id"
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

  create_table "case_relationships", :force => true do |t|
    t.integer "case_id"
    t.integer "relationship_type_id"
    t.integer "related_case_id"
    t.text    "comments"
    t.text    "observations"
  end

  create_table "case_statuses", :force => true do |t|
    t.string "name"
  end

  create_table "cases", :force => true do |t|
    t.string  "name",                                 :null => false
    t.date    "start_date",                           :null => false
    t.integer "start_date_type_id"
    t.date    "end_date"
    t.integer "end_date_type_id"
    t.integer "affected_persons"
    t.text    "narrative_description"
    t.text    "summary"
    t.text    "observations"
    t.integer "record_count",          :default => 0
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
    t.text     "name"
    t.text     "additional_info"
    t.date     "date"
    t.integer  "source_information_type_id"
    t.text     "site_name"
    t.string   "url"
    t.date     "access_date"
    t.integer  "language_id"
    t.integer  "indigenous_language_id"
    t.text     "observations"
    t.integer  "reported_person_id"
    t.integer  "reported_institution_id"
    t.integer  "reported_affiliation_type_id"
    t.integer  "reliability_level_id"
    t.text     "comments"
    t.datetime "created_at"
    t.datetime "updated_at"
  end

  create_table "documents", :force => true do |t|
    t.binary   "content"
    t.integer  "documentable_id"
    t.string   "documentable_type"
    t.string   "content_type"
    t.datetime "created_at"
    t.datetime "updated_at"
    t.string   "filename"
  end

  create_table "ethnic_groups", :force => true do |t|
    t.string "name", :null => false
  end

  create_table "human_rights_violation_categories", :force => true do |t|
    t.string  "name",      :null => false
    t.integer "parent_id"
    t.text    "notes"
  end

  create_table "human_rights_violations", :force => true do |t|
    t.string  "name",        :null => false
    t.integer "category_id"
    t.integer "parent_id"
    t.text    "notes"
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

# Could not dump table "immigration_attempts" because of following StandardError
#   Unknown type 'bool' for column 'is_traveling_companied'

  create_table "indigenous_groups", :force => true do |t|
    t.string   "name"
    t.string   "notes"
    t.datetime "created_at"
    t.datetime "updated_at"
  end

  create_table "indigenous_languages", :force => true do |t|
    t.string   "name"
    t.text     "notes"
    t.datetime "created_at"
    t.datetime "updated_at"
    t.integer  "country_id"
  end

  create_table "information_sources", :force => true do |t|
    t.integer  "case_id"
    t.integer  "source_person_id"
    t.integer  "source_institution_id"
    t.integer  "source_affiliation_type_id"
    t.integer  "reported_person_id"
    t.integer  "reported_institution_id"
    t.integer  "reported_affiliation_type_id"
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
    t.integer  "interventor_affiliation_type_id"
    t.integer  "supporter_id"
    t.integer  "supporter_institution_id"
    t.integer  "supporter_affiliation_type_id"
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
    t.date    "birthday"
    t.integer "marital_status_id", :null => false
    t.integer "country_id"
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

  create_table "perpetrator_statuses", :force => true do |t|
    t.string   "name"
    t.text     "notes"
    t.datetime "created_at"
    t.datetime "updated_at"
  end

  create_table "perpetrator_types", :force => true do |t|
    t.string   "name"
    t.text     "notes"
    t.integer  "parent_id"
    t.datetime "created_at"
    t.datetime "updated_at"
  end

  create_table "perpetrators", :force => true do |t|
    t.integer  "victim_id"
    t.integer  "person_id"
    t.integer  "institution_id"
    t.datetime "created_at"
    t.datetime "updated_at"
    t.integer  "perpetrator_status_id"
    t.integer  "perpetrator_type_id"
  end

  create_table "person_details", :force => true do |t|
    t.integer "person_id"
    t.integer "number_of_sons"
    t.integer "ethnic_group_id"
    t.integer "religion_id"
    t.integer "scholarity_level_id"
    t.integer "job_id"
    t.boolean "is_spanish_speaker"
    t.integer "indigenous_group_id"
  end

  create_table "person_relationship_types", :force => true do |t|
    t.string "name",  :null => false
    t.text   "notes"
  end

  create_table "person_relationships", :force => true do |t|
    t.integer "person_id"
    t.integer "related_person_id"
    t.integer "person_relationship_type_id"
    t.date    "start_date",                  :null => false
    t.integer "start_date_type_id"
    t.date    "end_date"
    t.integer "end_date_type_id"
    t.text    "comments"
  end

  create_table "places", :force => true do |t|
    t.integer  "case_id"
    t.integer  "country_id"
    t.integer  "state_id"
    t.integer  "city_id"
    t.datetime "created_at"
    t.datetime "updated_at"
  end

  create_table "relationship_types", :force => true do |t|
    t.string "name", :null => false
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
    t.integer "record_id"
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
    t.text     "notes"
  end

  create_table "victims", :force => true do |t|
    t.integer "person_id"
    t.integer "act_id"
    t.string  "characteristics"
    t.integer "victim_status_id"
  end

end
