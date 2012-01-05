using System;
using HumanRightsTracker.Models;

namespace Views
{
    public partial class DocumentarySourceWindow : Gtk.Window
    {
        public event EventHandler OnDocumentarySourceSaved = null;

        DocumentarySource documentary_source;
        bool isEditable;
        public DocumentarySourceWindow () : 
                base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
        }

        public DocumentarySourceWindow (DocumentarySource ds, EventHandler onSave, Gtk.Window parent) :
            base(Gtk.WindowType.Toplevel)
       {
           this.Build ();
           this.Modal = true;
           this.OnDocumentarySourceSaved= onSave;
           this.TransientFor = parent;
           DocumentarySource = ds;

       }

       public DocumentarySource DocumentarySource {
           get { return this.documentary_source; }
           set {
               documentary_source = value;
           }
       }
    }
}
