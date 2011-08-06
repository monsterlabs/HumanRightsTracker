using System;
using HumanRightsTracker.Models;

namespace Views
{
    public partial class NewActWindow : Gtk.Window
    {
        public event EventHandler OnActSaved = null;

        public NewActWindow (int case_id, EventHandler onSave, Gtk.Window parent) : base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.Modal = true;
            show.Act = new Act();
            show.Act.CaseId = case_id;
            show.IsEditing = true;
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
            this.Destroy ();
        }
    }
}

