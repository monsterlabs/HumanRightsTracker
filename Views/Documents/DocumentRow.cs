using System;
using HumanRightsTracker.Models;

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
    }
}

