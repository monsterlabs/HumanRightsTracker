using System;
using HumanRightsTracker.Models;

namespace Views
{
    public class VictimEventArgs : EventArgs
    {
        private Victim victim;

        public Victim Victim {
            get { return victim; }
        }

        public VictimEventArgs (Victim victim)
        {
            this.victim = victim;
        }
    }

    public partial class VictimWindow : Gtk.Window
    {
        public delegate void VictimEventHandler (object sender, VictimEventArgs args);

        public event VictimEventHandler OnSaveOrUpdate = null;

        public VictimWindow () : 
                base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
        }

        public VictimWindow (Victim victim, VictimEventHandler handler, Gtk.Window parent) : base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.Modal = true;
            show.Victim = victim;
            show.IsEditing = false;
            OnSaveOrUpdate = handler;
            this.TransientFor = parent;
        }

        public VictimWindow (Act act, VictimEventHandler handler, Gtk.Window parent) : base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.Modal = true;
            show.Victim = new Victim();
            show.Victim.Act = act;
            show.IsEditing = true;
            OnSaveOrUpdate = handler;
            this.TransientFor = parent;
        }

        protected void OnSave (object sender, System.EventArgs e)
        {
            Victim v = sender as Victim;
            OnSaveOrUpdate (this, new VictimEventArgs(v));
            this.Destroy ();
        }
    }
}

