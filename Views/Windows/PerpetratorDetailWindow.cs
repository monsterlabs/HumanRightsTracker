using System;
using HumanRightsTracker.Models;

namespace Views
{
    public partial class PerpetratorDetailWindow : Gtk.Window
    {
        public event EventHandler OnSaved = null;

        public PerpetratorDetailWindow (Victim victim, EventHandler OnSave, Gtk.Window parent) : base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.OnSaved = OnSave;
            this.TransientFor = parent;
            this.Modal = true;
            Perpetrator p = new Perpetrator ();
            p.Victim = victim;
            show.Perpetrator = p;
            show.IsEditable = true;
        }

        public PerpetratorDetailWindow (Perpetrator perpetrator, EventHandler OnSave, Gtk.Window parent) : base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.OnSaved = OnSave;
            this.TransientFor = parent;
            this.Modal = true;
            show.Perpetrator = perpetrator;
            show.IsEditable = false;
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

