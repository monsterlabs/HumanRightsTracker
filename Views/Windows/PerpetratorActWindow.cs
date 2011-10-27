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

        public PerpetratorActWindow (PerpetratorAct perpetratorAct, PerpetratorActEventHandler handler) : base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.Modal = true;
            //show.PerpetratorAct = perpetrator;
            OnSaveOrUpdate = handler;
        }

        protected void OnSave (object sender, System.EventArgs e)
        {
            PerpetratorAct p = sender as PerpetratorAct;
            OnSaveOrUpdate (this, new PerpetratorActEventArgs(p));
            this.Destroy ();
        }
    }
}

