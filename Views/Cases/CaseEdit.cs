using System;
using HumanRightsTracker.Models;
using Mono.Unix;
namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class CaseEdit : Gtk.Bin
    {
        public Case mycase;
        protected bool isEditing;

        public event EventHandler CaseSaved;

        public CaseEdit ()
        {
            this.Build ();
            this.isEditing = false;
        }

        public Case Case {
            get { return this.mycase; }
            set {
                mycase = value;
                if (mycase != null) {
                    nameEntry.Text = mycase.Name == null ? "" : mycase.Name;

                    startDateTypeAndDateSelector.dateTypeSelector.Active = mycase.StartDateType;
                    startDateTypeAndDateSelector.detailedDateSelector.CurrentDate = mycase.start_date;

                    endDateTypeAndDateSelector.dateTypeSelector.Active = mycase.EndDateType;
                    endDateTypeAndDateSelector.detailedDateSelector.CurrentDate = mycase.end_date;

                }
                isEditing = false;
            }
        }

        protected virtual void OnToggleEdit (object sender, System.EventArgs e)
        {
            isEditing = !isEditing;
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
                nameEntry.Visible = value;
                startDateTypeAndDateSelector.Visible = value;
                endDateTypeAndDateSelector.Visible = value;
            }
        }

        protected void OnSave (object sender, System.EventArgs e)
        {
            mycase.Name = nameEntry.Text;
            mycase.StartDateType = startDateTypeAndDateSelector.dateTypeSelector.Active as DateType;
            mycase.start_date = startDateTypeAndDateSelector.detailedDateSelector.CurrentDate;

            mycase.EndDateType = endDateTypeAndDateSelector.dateTypeSelector.Active as DateType;
            mycase.end_date = endDateTypeAndDateSelector.detailedDateSelector.CurrentDate;

            if (mycase.IsValid())
            {
                mycase.Save ();
                this.IsEditing = false;
                if (CaseSaved != null)
                    CaseSaved (mycase, e);
            } else
            {
                Console.WriteLine( String.Join(",", mycase.ValidationErrorMessages) );
                new ValidationErrorsDialog (mycase.PropertiesValidationErrorMessages);
            }

        }
    }
}

