using System;
using HumanRightsTracker.Models;

namespace Views
{
    public partial class ActDetailWindow : Gtk.Window
    {
        public event EventHandler OnActSaved = null;

        public ActDetailWindow (Case c, EventHandler onSave, Gtk.Window parent) : base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.Modal = true;
            show.Act = new Act();
            show.Act.Case = c;
            show.IsEditing = true;

            this.OnActSaved = onSave;
            this.TransientFor = parent;
        }

        public ActDetailWindow (Act act, EventHandler onSave, Gtk.Window parent) : base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.Modal = true;
            show.Act = act;
            show.IsEditing = false;
            this.OnActSaved = onSave;
            this.TransientFor = parent;
        }

        protected void OnSave (object sender, System.EventArgs e)
        {
            Act a = sender as Act;
            OnActSaved (a, e);
            this.Destroy ();
        }

        protected void OnCancel (object sender, System.EventArgs e)
        {
            if (show.Act.Id < 1)
                this.Destroy ();
        }
    }
}

