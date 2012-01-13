using System;
using HumanRightsTracker.Models;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class DocumentChooser : Gtk.Bin, IEditable
    {
        bool isEditable;
        Document document;

        public DocumentChooser ()
        {
            this.Build ();
            this.IsEditable = false;
            chooser.FileSet += new EventHandler (this.OnSet);
            Gtk.FileFilter docFilter = new Gtk.FileFilter ();
            docFilter.Name = "doc/pdf";
            docFilter.AddMimeType("application/pdf");
            docFilter.AddMimeType("application/msword");
            docFilter.AddPattern("*.pdf");
            docFilter.AddPattern("*.doc");
            chooser.AddFilter(docFilter);
        }

        protected void OnSet (object sender, System.EventArgs e)
        {
            Console.WriteLine ("Set: " + this.chooser.Filename);
            Console.WriteLine ("Basename: " + System.IO.Path.GetFileName (this.chooser.Filename));
        }

        public bool IsEditable {
            get { return isEditable; }
            set {
                this.isEditable = value;
                if (value) {
                    saveButton.Visible = false;
                    filename.Visible = false;
                    chooser.Visible = true;
                } else {
                    saveButton.Visible = true;
                    filename.Visible = true;
                    chooser.Visible = false;
                }

            }
        }

        public Document Document {
            get { return document; }
            set {
                document = value;
                if (document != null) {
                    filename.Text = document.Filename;
                    chooser.SetFilename(document.Filename);
                    chooser.SetUri(document.Filename);
                    chooser.CurrentName = document.Filename;
                }
            }
        }

        protected void OnOpen () {
        }
    }
}

