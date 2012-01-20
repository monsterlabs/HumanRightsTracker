using System;
using Mono.Unix;
using HumanRightsTracker.Models;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class PlaceShow : Gtk.Bin
    {
        Place place;
        bool isEditable;
        private EditableHelper editable_helper;

        public event EventHandler Saved;
        public event EventHandler Canceled;

        public PlaceShow ()
        {
            this.Build ();
            this.editable_helper = new EditableHelper(this);
        }

        public Place Place {
            get { return place; }
            set {
                this.place = value;
            }
        }

        public bool IsEditable
        {
            get { return this.isEditable; }
            set {
                this.isEditable = value;
                this.editable_helper.SetAllEditable(value);

                if (value) {
                    editButton.Label = Catalog.GetString("Cancel");
                    saveButton.Visible = true;
                } else {
                    editButton.Label = Catalog.GetString("Edit");
                }
            }
        }
    }
}

