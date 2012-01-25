using System;
using HumanRightsTracker.Models;
namespace Views
{
    public partial class DocumentarySourceDetailWindow : Gtk.Window
    {
        public event EventHandler OnSaved = null;

        public DocumentarySourceDetailWindow (Case c, EventHandler OnSave, Gtk.Window parent) : base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.Modal = true;
            this.OnSaved = OnSave;
            this.TransientFor = parent;
            DocumentarySource doc_source = new DocumentarySource ();
            doc_source.Case = c;
            show.DocumentarySource = doc_source;
            show.IsEditable = true;
        }

        public DocumentarySourceDetailWindow (DocumentarySource doc_source, EventHandler OnSave, Gtk.Window parent) : base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.Modal = true;
            this.OnSaved = OnSave;
            this.TransientFor = parent;
            show.DocumentarySource = doc_source;
            show.IsEditable = false;
        }


        protected void OnShowSaved (object sender, System.EventArgs e)
        {
            OnSaved (sender, e);
            this.Destroy ();
        }

        protected void OnShowCanceled (object sender, System.EventArgs e)
        {
            if (show.DocumentarySource.Id < 1) {
                this.Destroy ();
            }
        }
    }
}

