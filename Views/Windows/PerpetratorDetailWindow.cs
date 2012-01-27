using System;
using HumanRightsTracker.Models;

namespace Views
{
    public partial class PerpetratorDetailWindow : Gtk.Window
    {
        public event EventHandler OnSaved = null;

        public PerpetratorDetailWindow (Perpetrator perpetrator, EventHandler OnSave, Gtk.Window parent) : base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.Modal = true;
            this.OnSaved = OnSave;
            this.TransientFor = parent;
            show.Perpetrator = perpetrator;
            show.IsEditable = false;
        }

        public PerpetratorDetailWindow (Victim victim, EventHandler OnSave, Gtk.Window parent) : base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.Modal = true;
            this.OnSaved = OnSave;
            this.TransientFor = parent;
            show.Perpetrator = new Perpetrator();
            show.Perpetrator.Victim = victim;
            show.IsEditable = true;
        }

        protected void OnShowSaved (object sender, System.EventArgs e)
        {
            OnSaved (sender, e);
            this.Destroy ();
        }

        protected void OnShowCanceled (object sender, System.EventArgs e)
        {
            if (show.Perpetrator.Id < 1) {
                this.Destroy ();
            }
        }
    }
}

