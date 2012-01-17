using System;
using System.Collections.Generic;
using HumanRightsTracker.Models;
using Mono.Unix;
using System.Linq;

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
            ConnectTrackingHandlers ();
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

                    editablelist1.Records = value.Acts.Cast<ListableRecord>().ToList();
                    case_relationships_editablelist.Records = value.CaseRelationships.Cast<ListableRecord>().ToList();
                    interventionlist1.Case = value;
                    documentarysourcelist.Case = value;
                    informationsourcelist1.Case = value;
                    trackinglist.Case = value;
                    List<ListableRecord> trackings = value.TrackingInformation.Cast<ListableRecord>().ToList ();
                    trackings.Sort ((x, y) => {
                        DateTime timeX = ((TrackingInformation) x).DateOfReceipt.Value;
                        DateTime timeY = ((TrackingInformation) y).DateOfReceipt.Value;
                        return timeY.CompareTo(timeX);
                    });
                    trackinglist2.Records = trackings;

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
                interventionlist1.IsEditable = value;
                documentarysourcelist.IsEditable = value;
                informationsourcelist1.IsEditable = value;
            }
        }

        public void HideEditingButtons () {
            hbuttonbox9.Hide ();
        }

        public void ConnectTrackingHandlers () {
            trackinglist2.NewButtonPressed += (sender, e) => {
                new TrackingDetailWindow (this.Case, (o, args) => {
                    List<ListableRecord> trackings = this.Case.TrackingInformation.Cast<ListableRecord>().ToList ();
                    trackings.Sort ((x, y) => {
                        DateTime timeX = ((TrackingInformation) x).DateOfReceipt.Value;
                        DateTime timeY = ((TrackingInformation) y).DateOfReceipt.Value;
                        return timeY.CompareTo(timeX);
                    });
                    trackinglist2.Records = trackings;
                }, (Gtk.Window) this.Toplevel);
            };
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

        protected void OnNew (object sender, System.EventArgs e)
        {
            new ActDetailWindow (mycase, OnNewActReturned, (Gtk.Window)this.Toplevel);
        }

        protected void OnNewActReturned (object sender, EventArgs args)
        {
            editablelist1.Records = mycase.Acts.Cast<ListableRecord>().ToList();

            return;
        }

        protected void OnDelete (object sender, System.EventArgs e)
        {
            Act a = sender as Act;

            if (a.Id >= 1)
            {
                // TODO: Confirmation.
                a.Delete ();
            }

            editablelist1.Records = mycase.Acts.Cast<ListableRecord>().ToList();

            return;
        }

        protected void OnDetail (object sender, System.EventArgs e)
        {
            Act a = sender as Act;
            new ActDetailWindow (a, OnDetailReturned, (Gtk.Window)this.Toplevel);
        }

        protected void OnDetailReturned (object sender, System.EventArgs e)
        {
            editablelist1.Records = mycase.Acts.Cast<ListableRecord>().ToList();
        }

        protected void OnNewRelationShip (object sender, System.EventArgs e)
        {
            CaseRelationship case_relationship = new CaseRelationship ();
            case_relationship.Case = mycase;
            new CaseRelationshipWindow (case_relationship, OnNewCaseRelationshipReturned, (Gtk.Window)this.Toplevel);
        }

        protected void OnNewCaseRelationshipReturned  (object sender, EventArgs args) {
            // TODO: put your implementation here
            return;
        }

    }
}
