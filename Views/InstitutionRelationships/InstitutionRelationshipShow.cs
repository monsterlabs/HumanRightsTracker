using System;
using Mono.Unix;
using HumanRightsTracker.Models;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class InstitutionRelationshipShow : Gtk.Bin
    {
        InstitutionRelationship institution_relationship;
        bool isEditable;
        private EditableHelper editable_helper;

        public event EventHandler Saved;
        public event EventHandler Canceled;

        public InstitutionRelationshipShow ()
        {
            this.Build ();
            this.editable_helper = new EditableHelper(this);
        }

        public InstitutionRelationship InstitutionRelationship {
            get { return this.institution_relationship; }
            set {
                this.institution_relationship = value;
                institutionRelationshipTypeSelector.Active = institution_relationship.InstitutionRelationshipType;
                institutionSelector.Institution = institution_relationship.RelatedInstitution as Institution;
                startDateSelector.setDate(institution_relationship.start_date, institution_relationship.StartDateType);
                endDateSelector.setDate(institution_relationship.end_date, institution_relationship.EndDateType);
                comments.Text = institution_relationship.Comments;
            }
        }

        public bool IsEditable
        {
            get { return this.isEditable; }
            set {
                this.isEditable = value;
                this.editable_helper.SetAllEditable(value);

                if (value) {
                    editButton.Label = Catalog.GetString("Cancel");
                    saveButton.Visible = true;
                } else {
                    editButton.Label = Catalog.GetString("Edit");
                    saveButton.Visible = false;
                }
            }
        }

       public void HideActionButtons () {
            editButton.Visible = false;
            saveButton.Visible = false;
       }

       protected void OnSave (object sender, System.EventArgs e)
       {
            bool newRow = false;
            if (institution_relationship.Id < 1) {
                newRow = true;
            }

            institution_relationship.InstitutionRelationshipType = institutionRelationshipTypeSelector.Active as InstitutionRelationshipType;
            institution_relationship.RelatedInstitution = institutionSelector.Institution;
            institution_relationship.start_date = startDateSelector.SelectedDate ();
            institution_relationship.StartDateType = startDateSelector.SelectedDateType ();
            institution_relationship.end_date = endDateSelector.SelectedDate ();
            institution_relationship.EndDateType = endDateSelector.SelectedDateType ();
            institution_relationship.Comments = comments.Text;

            if (institution_relationship.IsValid () && institution_relationship.Institution != institution_relationship.RelatedInstitution ) {
                institution_relationship.SaveAndFlush ();
                if (newRow) {
                    institution_relationship.Institution.InstitutionRelationships.Add (InstitutionRelationship);
                    institution_relationship.Institution.SaveAndFlush ();
                }
                this.IsEditable = false;

                if (Saved != null)
                   Saved (this.InstitutionRelationship, e);
            } else {
                Console.WriteLine( String.Join(",", institution_relationship.ValidationErrorMessages) );
                new ValidationErrorsDialog (institution_relationship.PropertiesValidationErrorMessages, (Gtk.Window)this.Toplevel);
            }
        }

        protected void OnToggle (object sender, System.EventArgs e)
        {
            IsEditable = !IsEditable;
            if (!IsEditable && Canceled != null)
                Canceled (sender, e);
        }
    }
}

