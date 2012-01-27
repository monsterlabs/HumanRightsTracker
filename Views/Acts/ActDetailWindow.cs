using System;
using HumanRightsTracker.Models;

namespace Views
{
    public partial class ActDetailWindow : Gtk.Window
    {
        public event EventHandler OnSaved = null;

        public ActDetailWindow (Case c, EventHandler onSave, Gtk.Window parent) : base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.Modal = true;
            this.OnSaved = OnShowSaved;
            this.TransientFor = parent;

            show.Act = new Act();
            show.Act.Case = c;
            show.IsEditable= true;
        }

        public ActDetailWindow (Act act, EventHandler onSave, Gtk.Window parent) : base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.Modal = true;
            this.OnSaved = OnShowSaved;
            this.TransientFor = parent;

            show.Act = act;
            show.IsEditable = false;
        }

        protected void OnShowSaved (object sender, System.EventArgs e)
        {
            OnSaved (sender, e);
            this.Destroy ();
        }

        protected void OnShowCanceled (object sender, System.EventArgs e)
        {
             if (show.Act.Id < 1)
                this.Destroy ();
        }
    }
}

