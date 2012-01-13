using System;

namespace Views
{
    public partial class DocumentWindow : Gtk.Window
    {
        public DocumentWindow () : 
                base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
        }
    }
}

