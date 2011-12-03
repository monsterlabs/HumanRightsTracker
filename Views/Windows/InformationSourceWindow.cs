using System;
using HumanRightsTracker.Models;

namespace Views
{
    public partial class InformationSourceWindow : Gtk.Window
    {
        public event EventHandler OnInterventionSaved = null;

        public InformationSourceWindow () :
                base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
        }

        public InformationSourceWindow (Case c, EventHandler onSave, Gtk.Window parent) : base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.Modal = true;
            this.OnInterventionSaved = onSave;
            this.TransientFor = parent;
        }
    }
}

