using System;
using HumanRightsTracker.Models;
using Mono.Unix;

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

        protected bool isEditing;
        public bool isImmigrant = false;

        public event EventHandler PersonSaved;

        public Show ()
        {
            this.Build ();
            this.IsEditing = false;
        }

        public Person Person {
            get { return this.person; }
            set {
                this.person = value;
                if (person != null) {
                     set_person_widgets ();
                     set_address_widgets ();
                     if (this.isImmigrant == true ) {
                        set_immigration_details_widgets ();
                        set_identification_widgets ();
                        set_person_details_widgets ();
                     } else {
                        migration_attempts_frame.Destroy ();
                        identification_frame.Destroy ();
                        person_details_frame.Destroy ();
                     }
                    set_case_list();
                    set_institution_and_job_list();
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
                    institution_and_job_per_person.Hide ();
                } else {
                    editButton.Label = Catalog.GetString("Edit");
                    saveButton.Visible = false;
                    case_per_person.Show ();
                    institution_and_job_per_person.Show ();
                }

                editable_person (value);
                editable_address (value);
                if (this.isImmigrant == true ) {
                    editable_person_details (value);
                    editable_immigration_attempts (value);
                    editable_identification (value);
                }

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
                person.Save ();

                this.IsEditing = false;
                if (PersonSaved != null) {
                    PersonSaved (person, e);

                    address_save ();
                    if (this.isImmigrant == true ) {
                        person_detail_save ();
                        immigration_attempt_save ();
                        identification_save ();
                    }
                    image_save ();
                }
            } else
            {
                Console.WriteLine( String.Join(",", person.ValidationErrorMessages) );
                new ValidationErrorsDialog (person.PropertiesValidationErrorMessages);
            }

        }

        protected void OnBirthdayChanged (object sender, System.EventArgs e)
        {
            DateTime selectedBD = (DateTime)sender;
            age.Text = "" + DateTime.Now.Subtract(selectedBD).Days/365;
        }

        protected void set_person_widgets ()
        {
            lastname.Text = person.Lastname == null ? "" : person.Lastname;
            firstname.Text = person.Firstname == null ? "" : person.Firstname;
            alias.Text = person.Alias == null ? "" : person.Alias;
            email.Text = person.Email == null ? "" : person.Email;
            birthday.CurrentDate = person.Birthday;
            gender.Activate = person.Gender;
            marital_status.Active = person.MaritalStatus;
            birthplace.SetPlace(person.Country, person.State, person.City);
            settlement.Text = person.Settlement == null ? "" : person.Settlement;
            imageselector1.Image = person.Photo;
            age.Text = "" + DateTime.Now.Subtract(person.Birthday).Days/365;
        }

        protected void set_person_details_widgets ()
        {
            if (person.PersonDetails.Count == 0) {
                person_details = new PersonDetail ();
            } else {
                person_details = (PersonDetail)person.PersonDetails[0];
                number_of_sons.Text = person_details.NumberOfSons.ToString();
                // religion.Active = person_details.Religion;
                scholarity_level.Active = person_details.ScholarityLevel;
                most_recent_job.Active = person_details.MostRecentJob;
                // ethnic_group.Active = person_details.EthnicGroup as EthnicGroup;
                indigenous_group.Text = person_details.IndigenousGroup == null ? "" : person_details.IndigenousGroup;
                is_spanish_speaker.Activate = person_details.IsSpanishSpeaker;
            }

        }

        protected void set_immigration_details_widgets ()
        {
            if (person.ImmigrationAttempts.Count == 0) {
                immigration_attempt = new ImmigrationAttempt();
            } else {
                immigration_attempt = (ImmigrationAttempt)person.ImmigrationAttempts[0];
                traveling_reason.Active = immigration_attempt.TravelingReason;
                destination_country.Active = immigration_attempt.DestinationCountry as Country;
                expulsions_from_destination_country.Text = immigration_attempt.ExpulsionsFromDestinationCountry.ToString();
                transit_country.Active = immigration_attempt.TransitCountry as Country;
                expulsions_from_transit_country.Text = immigration_attempt.ExpulsionsFromTransitCountry.ToString();
                cross_border_attempts_transit_country.Text = immigration_attempt.CrossBorderAttemptsTransitCountry.ToString();
           }
        }

        protected void set_identification_widgets()
        {
            if (person.Identifications.Count == 0) {
                identification = new Identification ();
            } else {
                identification = (Identification)person.Identifications[0];
                identification_type.Active = identification.IdentificationType;
                identification_number.Text = identification.IdentificationNumber;
            }
        }

        protected void set_address_widgets ()
        {
            if (person.Addresses.Count == 0) {
                address = new Address ();
            } else {
                address = (Address)person.Addresses[0];
                location.Text = address.Location;
                address_place.SetPlace(address.Country, address.State, address.City);
                zipcode.Text = address.ZipCode  == null ? "" : address.ZipCode;
                phone.Text = address.Phone == null ? "" : address.Phone;
                mobile.Text = address.Mobile  == null ? "" : address.Mobile;
            }
        }

        protected void set_case_list() {
           case_per_person.Person = person;
        }

        protected void set_institution_and_job_list() {
           institution_and_job_per_person.Person = person;
        }

        protected void editable_person (bool value)
        {
            lastname.IsEditable = value;
            firstname.IsEditable = value;
            alias.IsEditable = value;
            settlement.IsEditable = value;
            gender.IsEditable = value;
            marital_status.IsEditable = value;
            birthday.IsEditable = value;
            birthplace.IsEditable = value;
            imageselector1.IsEditable = value;
            age.IsEditable = value;
            email.IsEditable = value;
        }

        protected void editable_person_details (bool value)
        {
             number_of_sons.IsEditable = value;
             scholarity_level.IsEditable = value;
             most_recent_job.IsEditable = value;
             //religion.IsEditable = value;
             //ethnic_group.IsEditable = value;
             indigenous_group.IsEditable = value;
             is_spanish_speaker.IsEditable = value;
        }

        protected void editable_immigration_attempts (bool value)
        {
             traveling_reason.IsEditable = value;
             destination_country.IsEditable = value;
             expulsions_from_destination_country.IsEditable = value;
             transit_country.IsEditable = value;
             expulsions_from_transit_country.IsEditable = value;
             cross_border_attempts_transit_country.IsEditable = value;
        }

        protected void editable_identification (bool value)
        {
             identification_type.IsEditable = value;
             identification_number.IsEditable = value;
        }

        protected void editable_address (bool value)
        {
            location.IsEditable  = value;
            address_place.IsEditable  = value;
            zipcode.IsEditable  = value;
            phone.IsEditable  = value;
            mobile.IsEditable  = value;
        }

        protected void person_detail_save ()
        {
            person_details.NumberOfSons = int.Parse(number_of_sons.Text);
            person_details.ScholarityLevel = scholarity_level.Active as ScholarityLevel;
            //person_details.Religion = religion.Active as Religion;
            // person_details.EthnicGroup = ethnic_group.Active as EthnicGroup;
            person_details.MostRecentJob = most_recent_job.Active as Job;
            person_details.IndigenousGroup = indigenous_group.Text;
            person_details.IsSpanishSpeaker = is_spanish_speaker.Value ();

            person_details.Person = person;
            if (person_details.IsValid()) {
               person_details.Save ();
            }

        }

        protected void immigration_attempt_save ()
        {
            immigration_attempt.TravelingReason = traveling_reason.Active as TravelingReason;
            immigration_attempt.DestinationCountry = destination_country.Active as Country;
            immigration_attempt.ExpulsionsFromDestinationCountry = int.Parse(expulsions_from_destination_country.Text);
            immigration_attempt.TransitCountry = transit_country.Active as Country;
            immigration_attempt.CrossBorderAttemptsTransitCountry = int.Parse(cross_border_attempts_transit_country.Text);
            immigration_attempt.ExpulsionsFromTransitCountry = int.Parse(expulsions_from_transit_country.Text);

            immigration_attempt.Person = person;
            if (immigration_attempt.IsValid()) {
                    immigration_attempt.Save ();
            }
        }

        protected void identification_save ()
        {
           identification.IdentificationType = identification_type.Active as IdentificationType;
           identification.IdentificationNumber = identification_number.Text;

            identification.Person = person;
           if (identification.IsValid()) {
                identification.Save();
           }
        }

        protected void address_save ()
        {
            address.Location = location.Text;
            address.Phone = phone.Text;
            address.Mobile = mobile.Text;
            address.ZipCode = zipcode.Text;
            address.Country = address_place.Country;
            address.State = address_place.State;
            address.City = address_place.City;

            address.Person = person;
            if (address.IsValid()) {
                address.Save();
            }

        }

        protected void prepare_person_record ()
        {
            person.Lastname = lastname.Text;
            person.Firstname = firstname.Text;
            person.Alias = alias.Text;
            person.Email = email.Text;
            person.IsImmigrant = this.isImmigrant;

            if (birthday.CurrentDate.Year == 1)
            {
                person.Birthday = new DateTime(DateTime.Now.Subtract(new TimeSpan(Convert.ToInt32(age.Text)*365, 0, 0, 0)).Year, 1, 1);
            } else
            {
                person.Birthday = DateTime.Now;
            }

            person.Country = birthplace.Country as Country;
            person.State = birthplace.State as State;
            person.City = birthplace.City as City;

            person.MaritalStatus = marital_status.Active as MaritalStatus;
            person.Gender = gender.Value ();
            person.Settlement = settlement.Text;
        }

        protected void image_save () {
            Image photo = imageselector1.Image;
            if (photo != null)
            {
                photo.ImageableId = person.Id;
                photo.ImageableType = "Person";
                photo.Save ();
            }
        }
    }
}
