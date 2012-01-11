using System;
using Mono.Unix;
using HumanRightsTracker.Models;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class TrackingShow : Gtk.Bin
    {
        TrackingInformation trackingInfo;
        bool isEditable;
        private EditableHelper editable_helper;

        public event EventHandler Saved;
        public event EventHandler Canceled;

        public TrackingShow ()
        {
            this.Build ();
            this.editable_helper = new EditableHelper(this);
        }

        public TrackingInformation TrackingInfo {
            get { return trackingInfo; }
            set {
                this.trackingInfo = value;
                if (this.trackingInfo != null) {
                }
            }
        }

        public bool IsEditable
        {
            get { return this.isEditable; }
            set {
                this.isEditable = value;
                this.editable_helper.SetAllEditable(value);

                if (value) {
                    editButton.Image = new Gtk.Image (Gtk.Stock.Cancel, Gtk.IconSize.Button);
                    editButton.Label = Catalog.GetString("Cancel");
                    saveButton.Visible = true;
                } else {
                    editButton.Image = new Gtk.Image (Gtk.Stock.Edit, Gtk.IconSize.Button);
                    editButton.Label = Catalog.GetString("Edit");
                    saveButton.Visible = false;
                }
            }
        }

        protected void OnToggle (object sender, System.EventArgs e)
        {
            IsEditable = !IsEditable;
            if (!isEditable && Canceled != null) {
                Canceled (sender, e);
            }
        }
    }
}

