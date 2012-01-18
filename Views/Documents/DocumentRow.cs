using System;
using System.IO;
using HumanRightsTracker.Models;
using Mono.Unix;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class DocumentRow : Gtk.Bin
    {
        bool isEditable;
        Document document;
        public new event EventHandler Removed;

        public DocumentRow ()
        {
            this.Build ();
        }

        public DocumentRow (Document d, EventHandler removed)
        {
            this.Build ();
            this.Document = d;
            this.Removed = removed;
        }

        public Document Document {
            get { return this.document; }
            set {
                this.document = value;
                filename.Text = this.document.Filename;
            }
        }

        public bool IsEditable {
            get { return this.isEditable; }
            set {
                this.isEditable = value;
                deleteButton.Visible = value;
                saveButton.Visible = !value;
            }
        }

        protected void OnDelete (object sender, System.EventArgs e)
        {
            if (Removed != null) {
                Removed (this, e);
            }
        }

        protected void OnDocumentSave (object sender, System.EventArgs e)
        {
            Gtk.FileChooserDialog dialog = new Gtk.FileChooserDialog(Catalog.GetString("Save Document ..."),
                (Gtk.Window) this.Toplevel,
                Gtk.FileChooserAction.Save);

            dialog.AddButton(Gtk.Stock.Cancel, Gtk.ResponseType.Cancel);
            dialog.AddButton(Gtk.Stock.Save, Gtk.ResponseType.Accept);
            dialog.CurrentName = this.Document.Filename;

            dialog.DefaultResponse = Gtk.ResponseType.Cancel;
            dialog.LocalOnly = true;

            int response = dialog.Run ();
            if (response == (int) Gtk.ResponseType.Accept) {
                File.WriteAllBytes(dialog.Filename, this.Document.Content);
            }
            dialog.Destroy ();
        }
    }
}

