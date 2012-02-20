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
                    case_per_institution.Institution = institution;
                    SetInstitutionRelationships ();
                    SetAffiliatedActorList ();
                }
                IsEditing = false;
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
                if (institution == null || institution.Id == 0) {
                    case_per_institution.Hide ();
                    related_institutions_expander.Hide ();
                    affiliated_actors_expander.Hide ();
                }
                case_per_institution.IsEditable = false;
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
            institution.ZipCode =  Int32.Parse(zipcode.Text);

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
            affiliated_actor_list.AffiliatedRecords = this.institution.AffiliatedPersonList().Cast<AffiliatedRecord>().ToList ();
        }

         public void SetInstitutionRelationships () {
            if (this.institution != null && this.institution.Id > 0 && this.institution.InstitutionRelationships != null)  {
                //ConnectPersonRelationshipsHandlers();
                related_institution_list.Records = this.institution.InstitutionRelationships.Cast<ListableRecord>().ToList ();
            }
            else
            {
                related_institutions_expander.Hide ();
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

    }
}
