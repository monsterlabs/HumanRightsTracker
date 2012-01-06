using System;
using HumanRightsTracker.Models;
using Mono.Unix;
namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class CaseShow : Gtk.Bin
    {
        public Case mycase;
        protected TrackingInformation tracking_info;
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

                    SetTrackingInformationWidget ();

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


        protected void SetTrackingInformationWidget()
        {
            Console.WriteLine(mycase.TrackingInformation.Count);
            if (mycase.TrackingInformation.Count == 0)
            {
                mycase.TrackingInformation.Add(new TrackingInformation());
                tracking_info = mycase.TrackingInformation[0] as TrackingInformation;
                tracking_info.Comments = "";
                tracking_info.Records = "";
            }
                tracking_info = mycase.TrackingInformation[0] as TrackingInformation;
                date_of_receipt.setDate(tracking_info.DateOfReceipt);
                date_of_receipt.setDateType(tracking_info.DateType);
                Console.WriteLine(tracking_info.Comments);
                comments.Text = tracking_info.Comments;
                case_status.Active = tracking_info.CaseStatus;
                records.Text = tracking_info.Records;

        }

        protected void TrackingInformationSave()
        {
            tracking_info.DateOfReceipt = date_of_receipt.SelectedDate ();
            tracking_info.DateType = date_of_receipt.SelectedDateType ();
            tracking_info.Comments = comments.Text;
            tracking_info.CaseStatus = case_status.Active as CaseStatus;
            tracking_info.Records = records.Text;
            tracking_info.Case = mycase;
            if (tracking_info.IsValid()) {
                tracking_info.Save();
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

                TrackingInformationSave ();

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
