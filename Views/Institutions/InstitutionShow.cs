using System;
using HumanRightsTracker.Models;
using Mono.Unix;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class InstitutionShow : Gtk.Bin
    {
        public Institution institution;
        protected bool isEditing;

        public event EventHandler InstitutionSaved;

        public InstitutionShow ()
        {
            this.Build ();
            this.isEditing = false;
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
                name.IsEditable = value;
                abbrev.IsEditable = value;
                institution_type.IsEditable = value;
                institution_category.IsEditable = value;

                location.IsEditable = value;
                place.IsEditable = value;
                zipcode.IsEditable = value;

                phone.IsEditable = value;
                fax.IsEditable = value;
                email.IsEditable = value;
                url.IsEditable = value;

                imageselector.IsEditable = value;
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
                institution.Save ();
                Image photo = imageselector.Image;
                if (photo != null)
                {
                    photo.ImageableId = institution.Id;
                    photo.ImageableType = "Institution";
                    photo.Save ();
                }

                this.IsEditing = false;
                if (InstitutionSaved != null)
                    InstitutionSaved (institution, e);
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
    }
}
