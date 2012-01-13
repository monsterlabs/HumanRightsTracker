using System;
using HumanRightsTracker.Models;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class DocumentarySourceRow : Gtk.Bin
    {
        DocumentarySource documentary_source;
        bool isEditable;
        public new event EventHandler Removed;

        public DocumentarySourceRow ()
        {
            this.Build ();
        }

        public DocumentarySourceRow (DocumentarySource ds, EventHandler removed)
        {
            this.Build ();
            DocumentarySource = ds;
            this.Removed = removed;
        }

        public DocumentarySource DocumentarySource
        {
           get { return documentary_source; }
           set
           {
               documentary_source = value;
               name.Text = documentary_source.Name;
               type.Text = documentary_source.AdditionalInfo;

                if (value.Date.HasValue)
                  published_date.Text = documentary_source.Date.Value.ToShortDateString ();

                if (value.AccessDate.HasValue)
                  access_date.Text = documentary_source.AccessDate.Value.ToShortDateString ();
           }
        }

        public bool IsEditable {
           get {
               return this.isEditable;
           }
           set {
               isEditable = value;
               removeButton.Visible = value;
           }
        }

        protected void OnRemoveButtonClicked (object sender, System.EventArgs e)
        {
             if (Removed != null)
               Removed (this, e);
        }

        protected void OnDetailButtonClicked (object sender, System.EventArgs e)
        {
         new DocumentarySourceWindow (DocumentarySource, OnDetailReturned, (Gtk.Window)this.Toplevel);
        }

        protected void OnDetailReturned (object sender, System.EventArgs e)
        {
           this.DocumentarySource = sender as DocumentarySource;
        }
    }
}