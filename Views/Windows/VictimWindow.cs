using System;

namespace Views
{
    public partial class VictimWindow : Gtk.Window
    {
        public VictimWindow () : 
                base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
        }
    }
}

