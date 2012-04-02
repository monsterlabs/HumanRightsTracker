using System;
using HumanRightsTracker.Models;

namespace Views
{
    public class CaseEventArgs : EventArgs {
        private Case c;

        public Case Case {
            get { return c; }
        }

        public CaseEventArgs (Case c)
        {
            this.c = c;
        }
    }

    public partial class CaseSelectorWindow : Gtk.Window
    {

        public delegate void CaseEventHandler (object sender, CaseEventArgs args);
        public event CaseEventHandler OnSelect = null;

        public CaseSelectorWindow (CaseEventHandler handler, Gtk.Window parent) : base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.TransientFor = parent;
            this.Modal = true;
            OnSelect = handler;
        }

        protected void OnSelection (object sender, System.EventArgs e)
        {
            Case c = sender as Case;
            OnSelect (this, new CaseEventArgs(c));
            this.Destroy();
        }
    }
}

