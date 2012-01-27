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
                placeselector.SetPlace (place.Country, place.State, place.City);
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
                    saveButton.Visible = false;
                }
            }
        }

        public void HideActionButtons () {
            editButton.Visible = false;
            saveButton.Visible = false;
        }

        protected void OnToggle (object sender, System.EventArgs e)
        {
            IsEditable = !IsEditable;
            if (!isEditable && Canceled != null) {
                Canceled (sender, e);
            }
        }

        protected void OnSave (object sender, System.EventArgs e)
        {
            bool newRow = false;
            if (place.Id < 1) {
                newRow = true;
            }
            place.Country = placeselector.Country;
            place.State = placeselector.State;
            place.City = placeselector.City;

            if (place.IsValid ()) {
                place.SaveAndFlush ();
                if (newRow) {
                    place.Case.Places.Add (Place);
                    place.Case.SaveAndFlush ();
                }
                this.IsEditable = false;

                if (Saved != null)
                        Saved (this.Place, e);
            } else {
                Console.WriteLine( String.Join(",", place.ValidationErrorMessages) );
                new ValidationErrorsDialog (place.PropertiesValidationErrorMessages, (Gtk.Window)this.Toplevel);
            }
        }
    }
}

