using System;
using HumanRightsTracker.Models;
using Mono.Unix;
namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class CaseShow : Gtk.Bin
    {
        public Case mycase;
        protected bool isEditing;

        public event EventHandler CaseSaved;

        public CaseShow ()
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
                    affectedPeople.Text = mycase.AffectedPeople.ToString();
                    startDateSelector.setDate(mycase.start_date);
                    startDateSelector.setDateType(mycase.StartDateType);

                    endDateSelector.setDate(mycase.end_date);
                    endDateSelector.setDateType(mycase.EndDateType);
                    actslist1.Case = value;
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
                nameEntry.IsEditable = value;
                affectedPeople.IsEditable = value;
                startDateSelector.IsEditable = value;
                endDateSelector.IsEditable = value;
            }
        }


        protected void OnSaveButtonClicked (object sender, System.EventArgs e)
        {
            mycase.Name = nameEntry.Text;
            mycase.start_date = startDateSelector.SelectedDate ();
            mycase.StartDateType = startDateSelector.SelectedDateType ();
            mycase.AffectedPeople = System.Convert.ToInt32(affectedPeople.Text);

            mycase.end_date = endDateSelector.SelectedDate ();
            mycase.EndDateType = endDateSelector.SelectedDateType ();

            if (mycase.IsValid())
            {
                mycase.Save ();

                foreach (Act a in actslist1.Acts)
                {
                    a.Case = mycase;
                    a.Save ();
                }

                this.IsEditing = false;
                if (CaseSaved != null)
                    CaseSaved (mycase, e);
            } else
            {
                Console.WriteLine( String.Join(",", mycase.ValidationErrorMessages) );
                new ValidationErrorsDialog (mycase.PropertiesValidationErrorMessages);
            }
        }

        protected void OnToggleEdit (object sender, System.EventArgs e)
        {
            IsEditing = !IsEditing;
        }

    }
}

