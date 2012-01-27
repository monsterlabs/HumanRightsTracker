using System;
using HumanRightsTracker.Models;

namespace Views
{
    public partial class InformationSourceDetailWindow : Gtk.Window
    {
        public event EventHandler OnSaved = null;

        public InformationSourceDetailWindow (Case c, EventHandler OnSave, Gtk.Window parent) : base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.Modal = true;
            this.OnSaved = OnSave;
            this.TransientFor = parent;
            InformationSource inf_source = new InformationSource ();
            inf_source.Case = c;
            show.InformationSource = inf_source;
            show.IsEditable = true;
        }

        public InformationSourceDetailWindow (InformationSource inf_source, EventHandler OnSave, Gtk.Window parent) : base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.Modal = true;
            this.OnSaved = OnSave;
            this.TransientFor = parent;
            show.InformationSource = inf_source;
            show.IsEditable = false;
        }

        protected void OnShowSaved (object sender, System.EventArgs e)
        {
            OnSaved (sender, e);
            this.Destroy ();
        }

        protected void OnShowCanceled (object sender, System.EventArgs e)
        {
            if (show.InformationSource.Id < 1) {
                this.Destroy ();
            }
        }
    }
}

