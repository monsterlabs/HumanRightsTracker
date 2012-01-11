using System;
using HumanRightsTracker.Models;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class TrackingRow : Gtk.Bin
    {
        TrackingInformation trackingInfo;
        bool isEditable;
        public new event EventHandler Removed;

        public TrackingRow ()
        {
            this.Build ();
        }

        public TrackingRow (TrackingInformation t, EventHandler removed)
        {
            this.Build ();
            this.TrackingInformation = t;
            this.Removed = removed;
        }

        public TrackingInformation TrackingInformation {
            get { return this.trackingInfo; }
            set {
                this.trackingInfo = value;
                DateTime? date = this.trackingInfo.DateOfReceipt;
                if (date.HasValue) {
                    date_of_receipt.Text = date.Value.ToShortDateString ();
                } else {
                    date_of_receipt.Text = "";
                }
                status.Text = this.trackingInfo.CaseStatus.Name;
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

        protected void OnDetail (object sender, System.EventArgs e)
        {
            new TrackingDetailWindow(this.TrackingInformation, OnDetailReturned, (Gtk.Window) this.Toplevel);
        }

        protected void OnDetailReturned (object sender, System.EventArgs e)
        {
            this.TrackingInformation = sender as TrackingInformation;
        }

        protected void OnDelete (object sender, System.EventArgs e)
        {
            if (Removed != null) {
                Removed(this, e);
            }
        }
    }
}

