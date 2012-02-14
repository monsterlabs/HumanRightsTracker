using System;
using HumanRightsTracker.Models;

namespace Views
{
    public partial class ActDetailWindow : Gtk.Window
    {
        public event EventHandler OnSaved = null;

        public ActDetailWindow (Case c, EventHandler OnSave, Gtk.Window parent) : base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.Modal = true;
            this.OnSaved = OnSave;
            this.TransientFor = parent;

            show.Act = new Act();
            show.Act.Case = c;
            show.IsEditable= true;
        }

        public ActDetailWindow (Act act, EventHandler OnSave, Gtk.Window parent) : base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.Modal = true;
            this.OnSaved = OnSave;
            this.TransientFor = parent;

            show.Act = act;
            show.IsEditable = false;
        }

        public ActDetailWindow (Act act, Gtk.Window parent) : base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.Modal = true;
            this.TransientFor = parent;
            show.Act = act;
            show.IsEditable = false;
            show.HideActionButtons ();
        }

        protected void OnShowSaved (object sender, System.EventArgs e)
        {
            OnSaved (sender, e);
            //this.Destroy ();
        }

        protected void OnShowCanceled (object sender, System.EventArgs e)
        {
             if (show.Act.Id < 1)
                this.Destroy ();
        }
    }
}
