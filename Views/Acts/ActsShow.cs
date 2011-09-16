using System;
using Mono.Unix;
using HumanRightsTracker.Models;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class ActsShow : Gtk.Bin
    {
        bool isEditing;
        Act act;

        public event EventHandler ActSaved;
        public event EventHandler Cancel;

        public ActsShow ()
        {
            this.Build ();
        }

        public Act Act {
            get { return this.act; }
            set {
                act = value;
                if (act != null) {
                    // TODO: fill the info
                    // person-acts
                    humanRightsViolation.Active = act.HumanRightsViolation;
                    initialDate.setDate (act.start_date);
                    initialDate.setDateType (act.StartDateType);
                    finalDate.setDate (act.end_date);
                    finalDate.setDateType (act.EndDateType);
                }
                IsEditing = false;
            }
        }

        protected void OnSave (object sender, System.EventArgs e)
        {
            act.HumanRightsViolation = humanRightsViolation.Active as HumanRightsViolation;
            act.end_date = finalDate.SelectedDate ();
            act.EndDateType = finalDate.SelectedDateType ();
            act.start_date = initialDate.SelectedDate ();
            act.StartDateType = initialDate.SelectedDateType ();

            if (act.Case.Id < 1 && ActSaved != null)
            {
                ActSaved (this.Act, e);
                return;
            }

            if (act.IsValid())
            {
                act.Save ();
                // TODO: Save victims and perpetrators
                this.IsEditing = false;
                if (ActSaved != null)
                    ActSaved (this.Act, e);
            } else
            {
                Console.WriteLine( String.Join(",", act.ValidationErrorMessages) );
                new ValidationErrorsDialog (act.PropertiesValidationErrorMessages);
            }
        }

        protected void OnToggleEdit (object sender, System.EventArgs e)
        {
            IsEditing = !IsEditing;
            if (!isEditing && Cancel != null)
                Cancel (sender, e);
        }

        public bool IsEditing
        {
            get { return this.isEditing; }
            set
            {
                isEditing = value;
                initialDate.IsEditable = value;
                finalDate.IsEditable = value;
                if (value) {
                    editButton.Label = Catalog.GetString("Cancel");
                    saveButton.Visible = true;
                } else {
                    editButton.Label = Catalog.GetString("Edit");
                    saveButton.Visible = false;
                }
            }
        }
    }
}

