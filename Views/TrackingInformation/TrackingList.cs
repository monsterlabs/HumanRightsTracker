using System;
using System.Collections.Generic;
using HumanRightsTracker.Models;
using NHibernate.Criterion;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class TrackingList : Gtk.Bin, IEditable
    {
        List<TrackingInformation> trackings;
        Case c;
        bool isEditable;

        public TrackingList ()
        {
            this.Build ();
            row.Destroy();
            trackings = new List<TrackingInformation>();
        }

        public Case Case
        {
            get { return c; }
            set
            {
                c = value;
                ReloadList ();
            }
        }

        public bool IsEditable {
            get {
                return this.isEditable;
            }
            set {
                isEditable = value;
                newButton.Visible = value;
                foreach (Gtk.Widget row in trackingsList.AllChildren) {
                    ((TrackingRow) row).IsEditable = value;
                }
            }
        }

        public void ReloadList ()
        {
            foreach (Gtk.Widget w in trackingsList.AllChildren)
            {
                w.Destroy();
            }
            if (c.Id < 1) {
                return;
            }
            trackings = new List<TrackingInformation> (TrackingInformation.FindAll (new ICriterion[] { Restrictions.Eq("Case", c) }));
            foreach (TrackingInformation t in trackings)
            {
                trackingsList.PackStart (new TrackingRow(t, OnTrackingRowRemoved));
            }
            trackingsList.ShowAll ();
        }

        protected void OnNewTracking (object sender, System.EventArgs e)
        {
            new TrackingDetailWindow(c, OnNewTrackingReturned, (Gtk.Window) this.Toplevel);
        }

        protected void OnNewTrackingReturned (object sender, System.EventArgs e)
        {
            TrackingInformation t = sender as TrackingInformation;
            trackingsList.PackStart (new TrackingRow (t, OnTrackingRowRemoved));
            trackingsList.ShowAll();
            trackings.Add(t);
        }

        protected void OnTrackingRowRemoved (object sender, EventArgs e)
        {
            TrackingRow trackingRow = sender as TrackingRow;
            TrackingInformation t = trackingRow.TrackingInformation;
            trackings.Remove(t);
            trackingsList.Remove(trackingRow);

            if (t.Id >= 1)
            {
                // TODO: Confirmation.
                t.Delete ();
            }
        }
    }
}

