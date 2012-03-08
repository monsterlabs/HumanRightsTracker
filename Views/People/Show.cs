// FIXIT: We should separate this class in two, one for each kind of actor
using System;
using System.Collections.Generic;
using HumanRightsTracker.Models;
using Mono.Unix;
using System.Linq;

namespace Views.People
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class Show : Gtk.Bin
    {
        public Person person;
        public PersonDetail person_details;
        public ImmigrationAttempt immigration_attempt;
        public Identification identification;
        public Address address;
        private EditableHelper editable_helper;

        protected bool isEditing;
        public bool isImmigrant = false;

        public event EventHandler PersonSaved;

        public Show ()
        {
            this.Build ();
            this.editable_helper = new EditableHelper(this);
            this.IsEditing = false;
            Catalog.GetString ("Male");
            Catalog.GetString ("Female");
            Catalog.GetString ("Yes");
            Catalog.GetString ("No");
            Catalog.GetString ("Companied");
            Catalog.GetString ("Alone");

            ConnectAddressesHandlers ();
            ConnectPersonRelationshipsHandlers ();
            ConnectInstitutionPeopleHandlers ();
        }

        public Person Person {
            get { return this.person; }
            set {
                this.person = value;
                if (person != null) {
                    set_person_widgets ();
                    set_address_widgets ();
                    if (this.isImmigrant == false)  {
                        migration_attempts_frame.Hide ();
                        identification_frame.Hide ();
                        if (this.person.Id < 1 ) {
                          address_frame.Hide ();
                          relationships_list_frame.Hide ();
                        }
                    } else {
                        set_person_details_widgets ();
                        set_immigration_details_widgets ();

                        if (this.person.Id < 1 ) {
                            address_frame.Hide ();
                            address_list_frame.Hide ();
                            contact_info_frame.Hide ();
                            identification_frame.Hide ();
                            place_of_birth_frame.Hide ();
                            relationships_list_frame.Hide ();
                        } else {
                            set_identification_widgets ();
                            contact_info_frame.Show ();
                            identification_frame.Show ();
                            place_of_birth_frame.Show ();
                        }
                    }

                    if (this.person.Id > 0 ) {
                        SetAffiliationList();
                        SetInstitutionPeople();
                        set_case_list();
                        SetAddressList ();
                        SetPersonRelationships ();

                    }
                }

                IsEditing = false;
            }

        }

        protected virtual void OnToggleEdit (object sender, System.EventArgs e)
        {
            IsEditing = !IsEditing;
        }

        public bool IsEditing
        {
            get { return this.isEditing; }
            set
            {
                isEditing = value;

                if (value) {
                    editButton.Label = Catalog.GetString("Cancel");
                    saveButton.Visible = true;
                    case_per_person.Hide ();
                   // institution_and_job_per_person.Hide ();
                } else {
                    editButton.Label = Catalog.GetString("Edit");
                    saveButton.Visible = false;
                    case_per_person.Show ();
                   // institution_and_job_per_person.Show ();
                }

                case_per_person.IsEditable = false;

                this.editable_helper.SetAllEditable(value);

                if (isImmigrant == false ) {
                     migration_attempts_frame.Hide();
                     identification_frame.Hide ();
                } else {
                    migration_attempts_frame.Show();
                    identification_frame.Show ();
                }

                if (this.person == null || this.person.Id < 1)   {
                    if (isImmigrant == false) {
                        address_frame.Show ();
                    } else {
                        address_frame.Hide ();
                        contact_info_frame.Hide ();
                        identification_frame.Hide ();
                    }
                    relationships_list_frame.Hide ();
                    address_list_frame.Hide ();
                } else {
                    SetAddressList();
                    SetInstitutionPeople();
                    SetPersonRelationships();
                    affiliation_list.IsEditable = false;
                }

                EnableActionButtons ();
            }
        }

        public void EnableActionButtons () {
            if (Person == null) {
                buttons.Hide ();
            } else {
                buttons.Show();
            }
        }

        public bool IsImmigrant {
            get {
                return this.isImmigrant;
            }
            set {
                this.isImmigrant = value;
            }
        }

        protected void OnSave (object sender, System.EventArgs e)
        {

            prepare_person_record ();
            if (person.IsValid())
            {
                person.SaveAndFlush ();

                this.IsEditing = false;
                if (PersonSaved != null) {
                    person_detail_save ();
                    if (this.isImmigrant == true ) {
                        immigration_attempt_save ();
                        identification_save ();
                    } else {
                        address_save ();
                    }
                    image_save ();
                    PersonSaved (person, e);
                }
            } else
            {
                Console.WriteLine( String.Join(",", person.ValidationErrorMessages) );
                new ValidationErrorsDialog (person.PropertiesValidationErrorMessages, (Gtk.Window)this.Toplevel);
            }

        }

        protected void OnBirthdayChanged (object sender, System.EventArgs e)
        {
            DateTime selectedBD = (DateTime)sender;
            age.Active = DateTime.Now.Subtract(selectedBD).Days/365;
        }

        protected void set_person_widgets ()
        {
            lastname.Text = person.Lastname == null ? "" : person.Lastname;
            firstname.Text = person.Firstname == null ? "" : person.Firstname;
            alias.Text = person.Alias == null ? "" : person.Alias;
            email.Text = person.Email == null ? "" : person.Email;
            birthday.CurrentDate = person.Birthday;
            gender.Activate = person.Id != 0 ? person.Gender : true;
            marital_status.Active = person.MaritalStatus;
            citizen.Active = person.Citizen;
            birthplace.SetPlace(person.Country, person.State, person.City);
            imageselector1.Image = person.Photo.Image;
            if (person.Birthday.Year > 1) {
                age.Active = DateTime.Now.Subtract(person.Birthday).Days/365;
            } else {
                age.Active = -1;
            }
        }

        protected void set_person_details_widgets ()
        {
            person.Refresh();
            if (person.PersonDetails == null ||person.PersonDetails.Count == 0) {
                person_details = new PersonDetail ();
            } else {
                person_details = (PersonDetail)person.PersonDetails[0];
            }
            number_of_sons.Active = person_details.NumberOfSons;
            scholarity_level.Active = person_details.ScholarityLevel;
            most_recent_job.Active = person_details.MostRecentJob;
            is_spanish_speaker.Activate = person.Id != 0 ? person_details.IsSpanishSpeaker : true;
            indigenous_group.Active = person_details.IndigenousGroup;
        }

        protected void set_immigration_details_widgets ()
        {
            person.Refresh();
            if (person.ImmigrationAttempts == null || person.ImmigrationAttempts.Count == 0 ) {
                immigration_attempt = new ImmigrationAttempt();
            } else {
                immigration_attempt = (ImmigrationAttempt)person.ImmigrationAttempts[0];
            }
            place_of_origin.SetPlace(immigration_attempt.OriginCountry,
                immigration_attempt.OriginState,
                immigration_attempt.OriginCity);

            traveling_reason.Active = immigration_attempt.TravelingReason;
            is_traveling_companied.Activate = immigration_attempt.Id != 0 ? immigration_attempt.IsTravelingCompanied : true;

            destination_country.Active = immigration_attempt.DestinationCountry as Country;
            stay_type.Active = immigration_attempt.StayType as StayType;
            expulsions_from_destination_country.Active = immigration_attempt.ExpulsionsFromDestinationCountry;
            transit_country.Active = immigration_attempt.TransitCountry as Country;
            expulsions_from_transit_country.Active = immigration_attempt.ExpulsionsFromTransitCountry;
            cross_border_attempts_transit_country.Active = immigration_attempt.CrossBorderAttemptsTransitCountry;
            cross_border_attempts_destination_country.Active = immigration_attempt.CrossBorderAttemptsDestinationCountry;
            //time_spent_in_destination_country.Text = immigration_attempt.TimeSpentInDestinationCountry ?? "";
        }

        protected void set_identification_widgets()
        {
            person.Refresh();
            if (person.Identifications == null || person.Identifications.Count == 0 ) {
                identification = new Identification ();
            } else {
                identification = (Identification)person.Identifications[0];
            }
            identification_type.Active = identification.IdentificationType;
            identification_number.Text = identification.IdentificationNumber;
        }

        protected void set_address_widgets ()
        {
            person.Refresh();
            if (person.Addresses == null || person.Addresses.Count == 0) {
                address = new Address ();
            } else {
                address = (Address)person.Addresses[0];
            }
            address_type.Active = address.AddressType;
            location.Text = address.Location ?? "";

            address_place.SetPlace(address.Country, address.State, address.City);
            zipcode.Text = address.ZipCode ?? "";
            phone.Text = address.Phone ?? "";
            mobile.Text = address.Mobile ?? "";
        }

        protected void set_case_list() {
           case_per_person.Person = person;
        }

        protected void set_institution_and_job_list() {
           //institution_and_job_per_person.Person = person;
        }

        protected void person_detail_save ()
        {
            if (person_details == null) {
                person_details = new PersonDetail();
            }
            person_details.NumberOfSons = number_of_sons.Active;
            person_details.ScholarityLevel = scholarity_level.Active as ScholarityLevel;
            //person_details.Religion = religion.Active as Religion;
            // person_details.EthnicGroup = ethnic_group.Active as EthnicGroup;
            person_details.MostRecentJob = most_recent_job.Active as Job;
            //person_details.IndigenousGroup = indigenous_group.Text;
            person_details.IndigenousGroup = indigenous_group.Active as IndigenousGroup;
            person_details.IsSpanishSpeaker = is_spanish_speaker.Value ();

            person_details.Person = person;
            if (person_details.IsValid()) {
               person_details.SaveAndFlush ();
            } else {
                Console.WriteLine( String.Join(",",  person_details.ValidationErrorMessages) );
                new ValidationErrorsDialog (person_details.PropertiesValidationErrorMessages, (Gtk.Window)this.Toplevel);
            }
        }

        protected void immigration_attempt_save ()
        {
            if (immigration_attempt == null) {
                immigration_attempt = new ImmigrationAttempt ();
            }
            immigration_attempt.OriginCountry = place_of_origin.Country;
            immigration_attempt.OriginState = place_of_origin.State;
            immigration_attempt.OriginCity = place_of_origin.City;
            immigration_attempt.CrossBorderAttemptsTransitCountry = cross_border_attempts_transit_country.Active;
            immigration_attempt.CrossBorderAttemptsDestinationCountry = cross_border_attempts_destination_country.Active;

            immigration_attempt.IsTravelingCompanied = is_traveling_companied.Value ();
            //immigration_attempt.TimeSpentInDestinationCountry = time_spent_in_destination_country.Text;
            immigration_attempt.DestinationCountry = destination_country.Active as Country;
            immigration_attempt.TransitCountry = transit_country.Active as Country;

            immigration_attempt.ExpulsionsFromDestinationCountry = expulsions_from_destination_country.Active;
            immigration_attempt.ExpulsionsFromTransitCountry = expulsions_from_transit_country.Active;
            immigration_attempt.IsTravelingCompanied = is_traveling_companied.Value ();
            immigration_attempt.StayType = stay_type.Active as StayType;
            immigration_attempt.Person = person;

            if (immigration_attempt.IsValid()) {
                    immigration_attempt.SaveAndFlush ();
            } else {
                Console.WriteLine( String.Join(",",  immigration_attempt.ValidationErrorMessages) );
                new ValidationErrorsDialog (immigration_attempt.PropertiesValidationErrorMessages, (Gtk.Window)this.Toplevel);
            }
        }

        protected void identification_save ()
        {
           if (identification == null ) {
                identification = new Identification();
           }
           identification.IdentificationType = identification_type.Active as IdentificationType;
           identification.IdentificationNumber = identification_number.Text;

           identification.Person = person;

           if (identification.IsValid()) {
                identification.SaveAndFlush();
           } else {
                Console.WriteLine( String.Join(",", identification.ValidationErrorMessages) );
                new ValidationErrorsDialog (identification.PropertiesValidationErrorMessages, (Gtk.Window)this.Toplevel);
           }
        }

        protected void address_save ()
        {
            address.AddressType = address_type.Active as AddressType;
            address.Location = location.Text;
            address.Phone = phone.Text;
            address.Mobile = mobile.Text;
            address.ZipCode = zipcode.Text;
            address.Country = address_place.Country;
            address.State = address_place.State;
            address.City = address_place.City;
            address.Person = person;

            if (address.IsValid()) {
                address.SaveAndFlush();
            } else {
                Console.WriteLine( String.Join(",", address.ValidationErrorMessages) );
                new ValidationErrorsDialog (address.PropertiesValidationErrorMessages, (Gtk.Window)this.Toplevel);
            }
        }

        protected void prepare_person_record ()
        {
            person.Lastname = lastname.Text;
            person.Firstname = firstname.Text;
            person.Alias = alias.Text;
            person.Email = email.Text;
            person.IsImmigrant = this.isImmigrant;

            if (birthday.CurrentDate.Year != 1) {
                person.Birthday = birthday.CurrentDate;
            } else {
                int numAge = age.Active;
                person.Birthday = new DateTime(DateTime.Now.Subtract(new TimeSpan(numAge*365, 0, 0, 0)).Year, 1, 1);
            }

            person.Country = birthplace.Country as Country;
            person.State = birthplace.State as State;
            person.City = birthplace.City as City;

            person.MaritalStatus = marital_status.Active as MaritalStatus;
            person.Citizen = citizen.Active as Country;
            person.Gender = gender.Value ();
        }

        protected void image_save () {
            Image photo = imageselector1.Image;
            if (photo != null)
            {
                photo.ImageableId = person.Id;
                photo.ImageableType = "Person";
                photo.SaveAndFlush ();
            }
        }

        private void SetAddressList () {
            address_list_frame.Show ();
            if (person.Addresses != null && this.person.Addresses.Count > 0 ){
                //ConnectAddressesHandlers ();
                address_list.Records = this.person.Addresses.Cast<ListableRecord>().ToList ();
                address_frame.Hide ();
            }
        }

        private void SetAffiliationList () {
            affiliation_list.AffiliableRecords = this.person.AffiliationList().Cast<AffiliableRecord>().ToList ();
        }

        private void ReloadAddresses () {
            List<ListableRecord> addresses = this.person.Addresses.Cast<ListableRecord>().ToList ();
            address_list.Records = addresses.Cast<ListableRecord>().ToList ();
        }

        public void ConnectAddressesHandlers() {
            address_list.NewButtonPressed += (sender, e) => {
                new AddressDetailWindow (this.Person, (o, args) => {
                    this.ReloadAddresses ();
                }, (Gtk.Window) this.Toplevel);
            };
            address_list.DeleteButtonPressed += (sender, e) => {
                Address record = sender as Address;
                this.Person.Addresses.Remove (record);
                if (record.Id >= 1) {
                    record.DeleteAndFlush ();
                }
                this.ReloadAddresses ();
            };
            address_list.DetailButtonPressed += (sender, e) => {
                Address record = sender as Address;
                new AddressDetailWindow(record, (o, args) => {
                    this.ReloadAddresses ();
                }, (Gtk.Window) this.Toplevel);
            };
        }

        public void SetPersonRelationships () {
            if (this.person != null && this.person.Id > 0 && this.person.PersonRelationships != null)  {
                //ConnectPersonRelationshipsHandlers();
                relationships_list_frame.Show ();
                person_relationship_list.Records = this.person.PersonRelationships.Cast<ListableRecord>().ToList ();
            }
            else
            {
                relationships_list_frame.Hide ();
            }
        }

        private void ReloadPersonRelationships () {
            List<ListableRecord> person_relationships = this.person.PersonRelationships.Cast<ListableRecord>().ToList ();
            person_relationship_list.Records = person_relationships.Cast<ListableRecord>().ToList ();
        }

        public void ConnectPersonRelationshipsHandlers() {
            person_relationship_list.NewButtonPressed += (sender, e) => {
                new PersonRelationshipDetailWindow (this.Person, (o, args) => {
                    this.ReloadPersonRelationships ();
                }, (Gtk.Window) this.Toplevel);
            };
            person_relationship_list.DeleteButtonPressed += (sender, e) => {
                PersonRelationship record = sender as PersonRelationship;
                this.Person.PersonRelationships.Remove (record);
                if (record.Id >= 1) {
                    record.DeleteAndFlush ();
                }
                this.ReloadPersonRelationships ();
            };
            person_relationship_list.DetailButtonPressed += (sender, e) => {
                PersonRelationship record = sender as PersonRelationship;
                new PersonRelationshipDetailWindow (record, (o, args) => {
                    this.ReloadPersonRelationships ();
                }, (Gtk.Window) this.Toplevel);
            };
        }

       public void SetInstitutionPeople() {
            affiliated_people_expander.Show ();
            if (this.person != null && this.person.Id > 0 && this.person.InstitutionPeople != null)  {
                affiliated_person_list.Records = this.person.InstitutionPeople.Cast<ListableRecord>().ToList ();
            }
        }

        private void ReloadInstitutionPeople () {
            List<ListableRecord> institution_people = this.person.InstitutionPeople.Cast<ListableRecord>().ToList ();
            affiliated_person_list.Records = institution_people.Cast<ListableRecord>().ToList ();
        }

        public void ConnectInstitutionPeopleHandlers() {
            affiliated_person_list.NewButtonPressed += (sender, e) => {
                new InstitutionPersonDetailWindow (this.Person, (o, args) => {
                    this.ReloadInstitutionPeople ();
                }, (Gtk.Window) this.Toplevel);
            };

            affiliated_person_list.DeleteButtonPressed += (sender, e) => {
                InstitutionPerson record = sender as InstitutionPerson;
                this.Person.InstitutionPeople.Remove (record);
                if (record.Id >= 1) {
                    record.DeleteAndFlush ();
                }
                this.ReloadInstitutionPeople ();
            };

            affiliated_person_list.DetailButtonPressed += (sender, e) => {
                InstitutionPerson record = sender as InstitutionPerson;
                new InstitutionPersonDetailWindow(record, (o, args) => {
                    this.ReloadInstitutionPeople ();
                }, (Gtk.Window) this.Toplevel, true);
            };
        }

    }
}
