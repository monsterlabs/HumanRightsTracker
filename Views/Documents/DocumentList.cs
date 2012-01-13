using System;
using System.Collections.Generic;
using HumanRightsTracker.Models;
using NHibernate.Criterion;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class DocumentList : Gtk.Bin, IEditable
    {
        List<Document> documents;
        TrackingInformation trackingInfo;
        bool isEditable;

        public DocumentList ()
        {
            this.Build ();
            row.Destroy ();
            documents = new List<Document> ();
        }

        public TrackingInformation TrackingInformation {
            get { return trackingInfo; }
            set {
                trackingInfo = value;
                ReloadList ();
            }
        }

        public bool IsEditable {
            get { return isEditable; }
            set {
                isEditable = value;
                newButton.Visible = value;
                foreach (Gtk.Widget row in documentsList.AllChildren) {
                    ((DocumentRow) row).IsEditable = value;
                }
            }
        }

        public void ReloadList()
        {
            foreach (Gtk.Widget w in documentsList) {
                w.Destroy ();
            }

            if (trackingInfo.Id < 1) {
                return;
            }
            documents = trackingInfo.Record.Documents;
            foreach (Document d in documents) {
                documentsList.PackStart (new DocumentRow (d));
            }
            documentsList.ShowAll ();
        }
    }
}

