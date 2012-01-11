using System;
using HumanRightsTracker.Models;

namespace Views
{
    public partial class TrackingDetailWindow : Gtk.Window
    {
        public event EventHandler OnSaved = null;

        public TrackingDetailWindow (Case c, EventHandler OnSave, Gtk.Window parent) :  base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.Modal = true;
            this.OnSaved = OnSave;
            this.TransientFor = parent;
            show.TrackingInfo = new TrackingInformation();
            show.TrackingInfo.Case = c;
            show.IsEditable = true;
        }

        public TrackingDetailWindow (TrackingInformation trackingInfo, EventHandler OnSave, Gtk.Window parent) :  base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.Modal = true;
            this.OnSaved = OnSave;
            this.TransientFor = parent;
            show.TrackingInfo = trackingInfo;
            show.IsEditable = false;
        }

        protected void OnShowSaved (object sender, System.EventArgs e)
        {
            TrackingInformation t = sender as TrackingInformation;
            OnSaved (t, e);
            this.Destroy ();
        }

        protected void OnShowCanceled (object sender, System.EventArgs e)
        {
            if (show.TrackingInfo.Id < 1) {
                this.Destroy ();
            }
        }
    }
}

