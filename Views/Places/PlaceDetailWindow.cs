using System;
using HumanRightsTracker.Models;

namespace Views
{
    public partial class PlaceDetailWindow : Gtk.Window
    {
        public PlaceDetailWindow () : 
                base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
        }
    }
}

