using System;
using System.Reflection;
namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class EditCatalogRecord : Gtk.Bin
    {
        public EditCatalogRecord ()
        {
            this.Build ();
        }

        public string NameEntry {
            set {
                nameEntry.Text = value;
            }

            get {
                return nameEntry.Text;
            }
        }

        public string NotesEntry {
            set {
                notesEntry.Buffer.Text = value;
            }

            get {
                return notesEntry.Buffer.Text;
            }
        }


        public string ParentName {
            set {
                parentNameLabel.Text = value;
            }

            get {
                return parentNameLabel.Text;
            }
        }

        public string ParentValue {
            set {
                parentValueLabel.Text = value;
            }

            get {
                return parentValueLabel.Text;
            }
        }

        public void HideNotesEntry () {
            notesEntry.Hide ();
            notesLabel.Hide ();
        }

        public void HideParentEntry () {
            parentNameLabel.Hide ();
            parentValueLabel.Hide ();
        }
    }
}

