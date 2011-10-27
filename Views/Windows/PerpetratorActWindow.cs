using System;
using HumanRightsTracker.Models;

namespace Views
{

    public class PerpetratorActEventArgs : EventArgs
    {
        private PerpetratorAct perpetratorAct;

        public PerpetratorAct PerpetratorAct {
            get { return perpetratorAct; }
        }

        public PerpetratorActEventArgs (PerpetratorAct perpetratorAct)
        {
            this.perpetratorAct = perpetratorAct;
        }
    }

    public partial class PerpetratorActWindow : Gtk.Window
    {
        public delegate void PerpetratorActEventHandler (object sender, PerpetratorActEventArgs args);

        public event PerpetratorActEventHandler OnSaveOrUpdate = null;

        public PerpetratorActWindow () : 
                base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
        }

        public PerpetratorActWindow (PerpetratorAct perpetratorAct, PerpetratorActEventHandler handler, Gtk.Window parent) : base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.Modal = true;
            show.PerpetratorAct = perpetratorAct;
            OnSaveOrUpdate = handler;
            show.IsEditing = false;
            this.TransientFor = parent;
        }

        public PerpetratorActWindow (Perpetrator perpetrator, PerpetratorActEventHandler handler, Gtk.Window parent) : base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.Modal = true;
            show.PerpetratorAct = new PerpetratorAct();
            show.PerpetratorAct.Perpetrator = perpetrator;
            OnSaveOrUpdate = handler;
            show.IsEditing = false;
            this.TransientFor = parent;
        }

        protected void OnSave (object sender, System.EventArgs e)
        {
            PerpetratorAct p = sender as PerpetratorAct;
            OnSaveOrUpdate (this, new PerpetratorActEventArgs(p));
            this.Destroy ();
        }
    }
}

