using System;
using HumanRightsTracker.Models;

namespace Views
{
    public partial class InterventionDetailWindow : Gtk.Window
    {
        public event EventHandler OnInterventionSaved = null;

        public InterventionDetailWindow (Case c, EventHandler onSave, Gtk.Window parent) : base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.Modal = true;
            show.Intervention = new Intervention();
            show.Intervention.Case = c;
            show.IsEditing = true;
            this.OnInterventionSaved = onSave;
            this.TransientFor = parent;
        }

        public InterventionDetailWindow (Intervention intervention, EventHandler onSave, Gtk.Window parent) : base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.Modal = true;
            show.Intervention = intervention;
            show.IsEditing = false;
            this.OnInterventionSaved = onSave;
            this.TransientFor = parent;
        }

        protected void OnSave (object sender, System.EventArgs e)
        {
            Intervention i = sender as Intervention;
            OnInterventionSaved (i, e);
            this.Destroy ();
        }

        protected void OnCancel (object sender, System.EventArgs e)
        {
            if (show.Intervention.Id < 1)
                this.Destroy ();
        }
    }
}

