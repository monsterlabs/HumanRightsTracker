using System;
using HumanRightsTracker.Models;

namespace Views
{

    public class PerpetratorEventArgs : EventArgs
    {
        private Perpetrator perpetrator;

        public Perpetrator Perpetrator {
            get { return perpetrator; }
        }

        public PerpetratorEventArgs (Perpetrator perpetrator)
        {
            this.perpetrator = perpetrator;
        }
    }

    public partial class PerpetratorWindow : Gtk.Window
    {
        public delegate void PerpetratorEventHandler (object sender, PerpetratorEventArgs args);

        public event PerpetratorEventHandler OnSaveOrUpdate = null;

        public PerpetratorWindow () : 
                base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
        }

        public PerpetratorWindow (Perpetrator perpetrator, PerpetratorEventHandler handler) : base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.Modal = true;
            //show.Perpetrator = perpetrator;
            OnSaveOrUpdate = handler;
        }

        protected void OnSave (object sender, System.EventArgs e)
        {
            Perpetrator p = sender as Perpetrator;
            OnSaveOrUpdate (this, new PerpetratorEventArgs(p));
            this.Destroy ();
        }
    }
}

