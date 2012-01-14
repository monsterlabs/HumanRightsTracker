using System;
using Mono.Unix;
using HumanRightsTracker.Models;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class TrackingShow : Gtk.Bin
    {
        TrackingInformation trackingInfo;
        bool isEditable;
        private EditableHelper editable_helper;

        public event EventHandler Saved;
        public event EventHandler Canceled;

        public TrackingShow ()
        {
            this.Build ();
            this.editable_helper = new EditableHelper(this);
        }

        public TrackingInformation TrackingInfo {
            get { return trackingInfo; }
            set {
                this.trackingInfo = value;
                if (this.trackingInfo != null) {
                    if (trackingInfo.Id < 1) {
                        record_id.Text = (trackingInfo.Case.RecordCount + 1).ToString ();
                    } else {
                        record_id.Text = trackingInfo.RecordId.ToString ();
                    }
                    date_of_receipt.setDate (trackingInfo.DateOfReceipt);
                    date_of_receipt.setDateType (trackingInfo.DateType);
                    title.Text = trackingInfo.Title;
                    status.Active = trackingInfo.CaseStatus;
                    comments.Text = trackingInfo.Comments;
                    records.TrackingInformation = value;
                }
            }
        }

        public bool IsEditable
        {
            get { return this.isEditable; }
            set {
                this.isEditable = value;
                this.editable_helper.SetAllEditable(value);

                if (value) {
                    editButton.Image = new Gtk.Image (Gtk.Stock.Cancel, Gtk.IconSize.Button);
                    editButton.Label = Catalog.GetString("Cancel");
                    saveButton.Visible = true;
                } else {
                    editButton.Image = new Gtk.Image (Gtk.Stock.Edit, Gtk.IconSize.Button);
                    editButton.Label = Catalog.GetString("Edit");
                    saveButton.Visible = false;
                }
            }
        }

        public void SaveDocuments()
        {
            foreach (Document d in records.Documents) {
                if (d.Id < 1) {
                    d.DocumentableId = trackingInfo.Id;
                    d.DocumentableType = "TrackingInformation";
                    d.Save ();
                }
            }
        }

        protected void OnToggle (object sender, System.EventArgs e)
        {
            IsEditable = !IsEditable;
            if (!isEditable && Canceled != null) {
                Canceled (sender, e);
            }
        }

        protected void OnSave (object sender, System.EventArgs e)
        {
            bool newRow = false;
            if (trackingInfo.Id < 1) {
                trackingInfo.RecordId = trackingInfo.Case.RecordCount + 1;
                newRow = true;
            }
            trackingInfo.DateOfReceipt = date_of_receipt.SelectedDate ();
            trackingInfo.DateType = date_of_receipt.SelectedDateType ();
            trackingInfo.Title = title.Text;
            trackingInfo.CaseStatus = status.Active as CaseStatus;
            trackingInfo.Comments = comments.Text;

            if (trackingInfo.IsValid()) {
                trackingInfo.Save ();
                if (newRow) {
                    TrackingInfo.Case.RecordCount += 1;
                    TrackingInfo.Case.Save ();
                }
                this.SaveDocuments ();
                this.IsEditable = false;

                if (Saved != null)
                        Saved (this.TrackingInfo, e);
            } else {
                Console.WriteLine( String.Join(",", trackingInfo.ValidationErrorMessages) );
                new ValidationErrorsDialog (trackingInfo.PropertiesValidationErrorMessages, (Gtk.Window)this.Toplevel);
            }
        }
    }
}

