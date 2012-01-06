using System;
using HumanRightsTracker.Models;
using Mono.Unix;
namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class CaseShow : Gtk.Bin
    {
        public Case mycase;
        protected AdministrativeInformation admin_info;
        private EditableHelper editable_helper;
        protected bool isEditing;

        public event EventHandler CaseSaved;

        public CaseShow ()
        {
            this.Build ();
            this.editable_helper = new EditableHelper(this);
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

                    description.Text = mycase.NarrativeDescription;
                    summary.Text = mycase.Summary;
                    observations.Text = mycase.Observations;

                    SetAdministrativeInformationWidget ();

                    actslist.Case = value;
                    interventionlist1.Case = value;
                    informationsourcelist1.Case = value;
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
                    if (mycase != null && mycase.Id == 0) {
                        this.Hide();
                    }
                }

                this.editable_helper.SetAllEditable (value);
                actslist.IsEditable = value;
                interventionlist1.IsEditable = value;
                informationsourcelist1.IsEditable = value;
            }
        }


        protected void SetAdministrativeInformationWidget()
        {
            if (mycase.AdministrativeInformation == null)
            {
                mycase.AdministrativeInformation = new AdministrativeInformation[1];
                mycase.AdministrativeInformation[0] = new AdministrativeInformation();
            }
                admin_info = mycase.AdministrativeInformation[0] as AdministrativeInformation;
                date_of_receipt.setDate(admin_info.DateOfReceipt);
                date_of_receipt.setDateType(admin_info.DateType);
                project_name.Text = admin_info.ProjectName;
                project_description.Text = admin_info.ProjectDescription;
                comments.Text = admin_info.Comments;
                case_status.Active = admin_info.CaseStatus;
                records.Text = admin_info.Records;

        }

        protected void AdministrativeInformationSave()
        {
            admin_info.DateOfReceipt = date_of_receipt.SelectedDate ();
            admin_info.DateType = date_of_receipt.SelectedDateType ();
            admin_info.ProjectName = project_name.Text;
            admin_info.ProjectDescription = project_description.Text;
            admin_info.Comments = comments.Text;
            admin_info.CaseStatus = case_status.Active as CaseStatus;
            admin_info.Records = records.Text;
            admin_info.Case = mycase;
            if (admin_info.IsValid()) {
                admin_info.Save();
            }
        }


        public void HideEditingButtons () {
            hbuttonbox7.Hide ();
        }
        protected void OnSaveButtonClicked (object sender, System.EventArgs e)
        {
            mycase.Name = nameEntry.Text;
            mycase.start_date = startDateSelector.SelectedDate ();
            mycase.StartDateType = startDateSelector.SelectedDateType ();
            mycase.AffectedPeople = System.Convert.ToInt32(affectedPeople.Text);

            mycase.end_date = endDateSelector.SelectedDate ();
            mycase.EndDateType = endDateSelector.SelectedDateType ();

            mycase.NarrativeDescription = description.Text;
            mycase.Summary = summary.Text;
            mycase.Observations = observations.Text;

            if (mycase.IsValid())
            {
                mycase.Save ();

                foreach (Act a in actslist.Acts)
                {
                    a.Case = mycase;
                    a.Save ();
                }

                AdministrativeInformationSave ();

                this.IsEditing = false;
                if (CaseSaved != null)
                    CaseSaved (mycase, e);
            } else
            {
                Console.WriteLine( String.Join(",", mycase.ValidationErrorMessages) );
                new ValidationErrorsDialog (mycase.PropertiesValidationErrorMessages, (Gtk.Window)this.Toplevel);
            }
        }

        protected void OnToggleEdit (object sender, System.EventArgs e)
        {
            IsEditing = !IsEditing;
        }
    }
}
