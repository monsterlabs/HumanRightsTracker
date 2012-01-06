using System;
using HumanRightsTracker.Models;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class TrackingRow : Gtk.Bin
    {
        TrackingInformation track_info;
        bool isEditable;
        public new event EventHandler Removed;

        public TrackingRow ()
        {
            this.Build ();
        }

        public TrackingRow (TrackingInformation t, EventHandler removed)
        {
            this.Build ();
            this.TrackInfo = t;
            this.Removed = removed;
        }

        public TrackingInformation TrackInfo {
            get { return this.track_info; }
            set {
                this.track_info = value;
                DateTime? date = this.track_info.DateOfReceipt;
                if (date.HasValue) {
                    date_of_receipt.Text = date.Value.ToShortDateString ();
                } else {
                    date_of_receipt.Text = "";
                }
                status.Text = this.track_info.CaseStatus.Name;
            }
        }

        public bool IsEditable {
            get {
                return this.isEditable;
            }
            set {
                this.isEditable = value;
                deleteButton.Visible = value;
            }
        }
    }
}

