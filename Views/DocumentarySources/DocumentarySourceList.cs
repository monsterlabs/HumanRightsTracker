using System;
using HumanRightsTracker.Models;
using System.Collections.Generic;
using NHibernate.Criterion;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class DocumentarySourceList : Gtk.Bin
    {
        List<DocumentarySource> documentary_sources;
        Case c;
        bool isEditable;
        public DocumentarySourceList ()
        {
            this.Build ();
            row.Destroy ();
            documentary_sources = new List<DocumentarySource>();
        }

        public List<DocumentarySource> DocumentarySources
        {
            get {return documentary_sources;}
        }

        public Case Case
        {
            get { return c; }
            set
            {
                c = value;
                ReloadList ();
            }
        }

        public bool IsEditable {
            get {
                return this.isEditable;
            }
            set {
                isEditable = value;
                newButton.Visible = value;
                foreach (Gtk.Widget row in documentarySourceList.AllChildren) {
                    ((DocumentarySourceRow) row).IsEditable = value;
                }
            }
        }

        public void ReloadList ()
        {
            foreach (Gtk.Widget w in documentarySourceList.AllChildren)
            {
                w.Destroy();
            }
            if (c.Id < 1) {
                return;
            }

            documentary_sources = new List<DocumentarySource>();

            foreach (DocumentarySource i in c.DocumentarySources)
            {
                documentary_sources.Add (i);
                documentarySourceList.PackStart (new DocumentarySourceRow (i, OnDocumentarySourceRowRemoved));
            }
           documentarySourceList.ShowAll ();
        }

        protected void OnDocumentarySourceRowRemoved (object sender, EventArgs e)
        {
            DocumentarySourceRow documentarySourceRow = sender as DocumentarySourceRow;
            DocumentarySource ds = documentarySourceRow.DocumentarySource;
            documentary_sources.Remove(ds);
            documentarySourceList.Remove(documentarySourceRow);

            if (ds.Id >= 1)
            {
                // TODO: Confirmation.
                ds.Delete ();
            }

            return;
        }

        protected void OnNewButtonClicked (object sender, System.EventArgs e)
        {
            DocumentarySource ds = new DocumentarySource();
            ds.Case = c;
            new DocumentarySourceWindow (ds, OnDocumentarySourceSaved, (Gtk.Window)this.Toplevel);

        }

           protected void OnDocumentarySourceSaved (object sender, EventArgs args)
        {
            DocumentarySource ds = sender as DocumentarySource;
            documentarySourceList.PackEnd (new DocumentarySourceRow (ds, OnDocumentarySourceRowRemoved));
            documentarySourceList.ShowAll ();
            documentary_sources.Add (ds);
            return;
        }

    }
}

