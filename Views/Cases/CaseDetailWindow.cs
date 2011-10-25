
using System;
using HumanRightsTracker.Models;
namespace Views
{
    public partial class CaseDetailWindow : Gtk.Window
    {

        public CaseDetailWindow (Case c, Gtk.Window parent) :
                base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            case_show.Case = c;
            case_show.HideEditingButtons ();
        }


        protected void OnBack (object sender, System.EventArgs e)
        {
          this.Destroy ();
        }
    }
}

