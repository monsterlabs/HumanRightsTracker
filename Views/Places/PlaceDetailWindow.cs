using System;
using HumanRightsTracker.Models;

namespace Views
{
    public partial class PlaceDetailWindow : Gtk.Window
    {
        public event EventHandler OnSaved = null;

        public PlaceDetailWindow (Case c, EventHandler OnSave, Gtk.Window parent) :  base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.Modal = true;
            this.OnSaved = OnSave;
            this.TransientFor = parent;
            Place p = new Place ();
            p.Case = c;
            show.Place = p;
            show.IsEditable = true;
        }

        public PlaceDetailWindow (Place place, EventHandler OnSave, Gtk.Window parent) :  base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.Modal = true;
            this.OnSaved = OnSave;
            this.TransientFor = parent;
            show.Place = place;
            show.IsEditable = false;
        }

        public PlaceDetailWindow (Place place, Gtk.Window parent) :  base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.Modal = true;
            this.TransientFor = parent;
            show.Place = place;
            show.IsEditable = false;
            show.HideActionButtons ();
        }

        protected void OnShowSaved (object sender, System.EventArgs e)
        {
            OnSaved (sender, e);
            this.Destroy ();
        }

        protected void OnShowCanceled (object sender, System.EventArgs e)
        {
            if (show.Place.Id < 1) {
                this.Destroy ();
            }
        }
    }
}

