using System;
using System.Collections.Generic;
using HumanRightsTracker.Models;
using Mono.Unix;
using System.Linq;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class InstitutionShow : Gtk.Bin
    {
        public Institution institution;
        protected bool isEditing;
        private EditableHelper editable_helper;
        public event EventHandler InstitutionSaved;

        public InstitutionShow ()
        {
            this.Build ();
            this.editable_helper = new EditableHelper(this);
            this.isEditing = false;
            ConnectInstitutionRelationshipsHandlers ();
            ConnectInstitutionPeopleHandlers ();
        }

        public Institution Institution {
            get { return this.institution; }
            set {
                institution = value;
                if (institution != null) {
                    imageselector.Image = institution.Photo;
                    name.Text = institution.Name == null ? "" : institution.Name;
                    abbrev.Text = institution.Abbrev == null ? "" : institution.Abbrev;
                    institution_type.Active = institution.InstitutionType;
                    institution_category.Active = institution.InstitutionCategory;

                    location.Text = institution.Location == null ? "" : institution.Location;
                    place.SetPlace(institution.Country, institution.State, institution.City);
                    zipcode.Text = institution.ZipCode.ToString();

                    phone.Text = institution.Phone == null ? "" : institution.Phone;
                    fax.Text = institution.Fax == null ? "" : institution.Fax;
                    email.Text = institution.Email == null ? "" : institution.Email;
                    url.Text = institution.Url == null ? "" : institution.Url;
                    ShowAssociatedRecordList ();
                }
                IsEditing = false;
            }
        }

        public void ShowAssociatedRecordList () {
            if (institution.Id > 0 ) {
                case_per_institution.Show ();
                case_per_institution.Institution = institution;
                SetInstitutionRelationships ();
                SetInstitutionPeople ();
                SetAffiliatedActorList ();
             }
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
                } else {
                    editButton.Label = Catalog.GetString("Edit");
                    saveButton.Visible = false;
                }

                this.editable_helper.SetAllEditable(value);
                if (institution == null || institution.Id < 1) {
                    case_per_institution.Hide ();
                    related_institutions_expander.Hide ();
                    affiliated_actors_expander.Hide ();
                    affiliated_people_expander.Hide ();
                } else {
                    ShowAssociatedRecordList ();
                }
                case_per_institution.IsEditable = false;

                EnableActionButtons ();
            }
        }

        public void EnableActionButtons () {
            if (Institution == null) {
                buttons.Hide ();
            } else {
                buttons.Show();
            }
        }

        protected void OnSaveButtonClicked (object sender, System.EventArgs e)
        {
            institution.Name = name.Text;
            institution.Abbrev = abbrev.Text;
            institution.InstitutionType = institution_type.Active as InstitutionType;
            institution.InstitutionCategory = institution_category.Active as InstitutionCategory;

            institution.Location = location.Text;
            institution.Country = place.Country as Country;
            institution.State = place.State as State;
            institution.City = place.City as City;

            int zipCode;
            bool isNum = int.TryParse(zipcode.Text, out zipCode);
            if (isNum) {
                institution.ZipCode =  zipCode;
            }

            institution.Phone = phone.Text;
            institution.Fax = fax.Text;
            institution.Email = email.Text;
            institution.Url = url.Text;

            if (institution.IsValid())
            {
                institution.SaveAndFlush ();
                Image photo = imageselector.Image;
                if (photo != null)
                {
                    photo.ImageableId = institution.Id;
                    photo.ImageableType = "Institution";
                    photo.SaveAndFlush ();
                }


                if (InstitutionSaved != null)
                    InstitutionSaved (institution, e);
                this.IsEditing = false;
            } else
            {
                Console.WriteLine( String.Join(",", institution.ValidationErrorMessages) );
                new ValidationErrorsDialog (institution.PropertiesValidationErrorMessages, (Gtk.Window)this.Toplevel);
            }

        }

        protected void OnEditButtonClicked (object sender, System.EventArgs e)
        {
            IsEditing = !IsEditing;
        }

        private void SetAffiliatedActorList () {
            affiliated_actors_expander.Show ();
            affiliated_actor_list.AffiliatedRecords = this.institution.AffiliatedPersonList().Cast<AffiliatedRecord>().ToList ();
        }

        public void SetInstitutionRelationships () {
            related_institutions_expander.Show ();
            if (this.institution != null && this.institution.Id > 0 && this.institution.InstitutionRelationships != null)  {
                related_institution_list.Records = this.institution.InstitutionRelationships.Cast<ListableRecord>().ToList ();
            }
        }

        private void ReloadInstitutionRelationships () {
            List<ListableRecord> institution_relationships = this.institution.InstitutionRelationships.Cast<ListableRecord>().ToList ();
            related_institution_list.Records = institution_relationships.Cast<ListableRecord>().ToList ();
        }

        public void ConnectInstitutionRelationshipsHandlers() {
            related_institution_list.NewButtonPressed += (sender, e) => {
                new InstitutionRelationshipDetailWindow (this.Institution, (o, args) => {
                    this.ReloadInstitutionRelationships ();
                }, (Gtk.Window) this.Toplevel);
            };
            related_institution_list.DeleteButtonPressed += (sender, e) => {
                InstitutionRelationship record = sender as InstitutionRelationship;
                this.Institution.InstitutionRelationships.Remove (record);
                if (record.Id >= 1) {
                    record.DeleteAndFlush ();
                }
                this.ReloadInstitutionRelationships ();
            };
            related_institution_list.DetailButtonPressed += (sender, e) => {
                InstitutionRelationship record = sender as InstitutionRelationship;
                new InstitutionRelationshipDetailWindow(record, (o, args) => {
                    this.ReloadInstitutionRelationships ();
                }, (Gtk.Window) this.Toplevel);
            };
        }

       public void SetInstitutionPeople() {
            affiliated_people_expander.Show ();
            if (this.institution != null && this.institution.Id > 0 && this.institution.InstitutionPeople != null)  {
                affiliated_person_list.Records = this.institution.InstitutionPeople.Cast<ListableRecord>().ToList ();
            }
        }

        private void ReloadInstitutionPeople () {
            List<ListableRecord> institution_people = this.institution.InstitutionPeople.Cast<ListableRecord>().ToList ();
             affiliated_person_list.Records = institution_people.Cast<ListableRecord>().ToList ();
        }

        public void ConnectInstitutionPeopleHandlers() {
            affiliated_person_list.NewButtonPressed += (sender, e) => {
                new InstitutionPersonDetailWindow (this.Institution, (o, args) => {
                    this.ReloadInstitutionPeople ();
                }, (Gtk.Window) this.Toplevel);
            };
            affiliated_person_list.DeleteButtonPressed += (sender, e) => {
                InstitutionPerson record = sender as InstitutionPerson;
                this.Institution.InstitutionPeople.Remove (record);
                if (record.Id >= 1) {
                    record.DeleteAndFlush ();
                }
                this.ReloadInstitutionPeople ();
            };
            affiliated_person_list.DetailButtonPressed += (sender, e) => {
                InstitutionPerson record = sender as InstitutionPerson;
                new InstitutionPersonDetailWindow(record, (o, args) => {
                    this.ReloadInstitutionPeople ();
                }, (Gtk.Window) this.Toplevel);
            };
        }

    }
}
