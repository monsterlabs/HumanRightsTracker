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
            TrackingInformation t = new TrackingInformation ();
            t.Case = c;
            show.TrackingInfo = t;
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
            OnSaved (sender, e);
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

