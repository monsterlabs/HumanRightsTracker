using System;
using System.IO;
using System.Collections.Generic;
using HumanRightsTracker.Models;
using NHibernate.Criterion;
using Mono.Unix;

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

        public List<Document> Documents {
            get { return documents; }
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
                documentsList.PackStart (new DocumentRow (d, OnDocumentRemoved));
            }
            documentsList.ShowAll ();
        }

        protected void OnDocumentRemoved (object sender, EventArgs e)
        {
            DocumentRow documentRow = sender as DocumentRow;
            Document d = documentRow.Document;
            documents.Remove(d);
            documentsList.Remove(documentRow);

            if (d.Id >= 1)
            {
                // TODO: Confirmation.
                d.Delete ();
            }
        }

        protected void OnNewDocument (object sender, System.EventArgs e)
        {
            Gtk.FileChooserDialog dialog = new Gtk.FileChooserDialog(Catalog.GetString("Add New Document"),
                (Gtk.Window) this.Toplevel,
                Gtk.FileChooserAction.Open);

            dialog.AddButton(Gtk.Stock.Cancel, Gtk.ResponseType.Cancel);
            dialog.AddButton(Gtk.Stock.Open, Gtk.ResponseType.Accept);

            dialog.DefaultResponse = Gtk.ResponseType.Cancel;
            dialog.LocalOnly = true;

            Gtk.FileFilter filter = new Gtk.FileFilter ();
            filter.Name = "doc/pdf";
            filter.AddMimeType("application/pdf");
            filter.AddMimeType("application/msword");
            filter.AddPattern("*.pdf");
            filter.AddPattern("*.doc");
            dialog.Filter = filter;

            int response = dialog.Run ();
            Document doc = new Document ();
            if (response == (int) Gtk.ResponseType.Accept) {
                string filename = System.IO.Path.GetFileName (dialog.Filename);
                string extension = filename.Split('.')[1];
                doc.Filename = filename;
                if (extension == "pdf") {
                    doc.ContentType = "application/pdf";
                } else if (extension == "doc") {
                    doc.ContentType = "application/msword";
                }
                doc.Content = File.ReadAllBytes(dialog.Filename);
                documents.Add(doc);
                documentsList.PackStart(new DocumentRow (doc, OnDocumentRemoved));
                documentsList.ShowAll ();
            }
            dialog.Destroy ();
        }
    }
}

