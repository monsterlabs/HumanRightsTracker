using System;
using HumanRightsTracker.Models;

namespace Views
{
    public partial class InterventionDetailWindow : Gtk.Window
    {
        public event EventHandler OnSaved = null;

        public InterventionDetailWindow (Case c, EventHandler OnSave, Gtk.Window parent) : base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.Modal = true;
            this.OnSaved = OnSave;
            this.TransientFor = parent;
            Intervention i = new Intervention ();
            i.Case = c;
            show.Intervention = i;
            show.IsEditable = true;
        }

        public InterventionDetailWindow (Intervention intervention, EventHandler OnSave, Gtk.Window parent) : base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.Modal = true;
            this.OnSaved = OnSave;
            this.TransientFor = parent;
            show.Intervention = intervention;
            show.IsEditable = false;
        }

        protected void OnSave (object sender, System.EventArgs e)
        {
            OnSaved (sender, e);
            this.Destroy ();
        }

        protected void OnCancel (object sender, System.EventArgs e)
        {
            if (show.Intervention.Id < 1)
                this.Destroy ();
        }

        protected void OnShowSaved (object sender, System.EventArgs e)
        {
            OnSaved (sender, e);
            this.Destroy ();
        }

        protected void OnShowCanceled (object sender, System.EventArgs e)
        {
            if (show.Intervention.Id < 1)
                this.Destroy ();
        }
    }
}

