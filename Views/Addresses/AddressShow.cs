using System;
using Mono.Unix;
using HumanRightsTracker.Models;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class AddressShow : Gtk.Bin
    {
        Address address;
        bool isEditable;
        private EditableHelper editable_helper;

        public event EventHandler Saved;
        public event EventHandler Canceled;

        public AddressShow ()
        {
            this.Build ();
            this.editable_helper = new EditableHelper(this);
        }


        public Address Address {
            get { return address; }
            set {
                this.address = value;
                address_type.Active = address.AddressType;
                location.Text = address.Location ?? "";

                address_place.SetPlace(address.Country, address.State, address.City);
                zipcode.Text = address.ZipCode ?? "";
                phone.Text = address.Phone ?? "";
                mobile.Text = address.Mobile ?? "";
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

        protected void OnSave (object sender, System.EventArgs e)
        {
            bool newRow = false;
            if (address.Id < 1) {
                newRow = true;
            }
            address.AddressType = address_type.Active as AddressType;
            address.Location = location.Text;
            address.Phone = phone.Text;
            address.Mobile = mobile.Text;
            address.ZipCode = zipcode.Text;
            address.Country = address_place.Country;
            address.State = address_place.State;
            address.City = address_place.City;

            if (address.IsValid ()) {

                address.SaveAndFlush ();
                address.Person.Refresh();
                this.IsEditable = false;

                if (Saved != null)
                   Saved (this.Address, e);
            } else {
                Console.WriteLine( String.Join(",", address.ValidationErrorMessages) );
                new ValidationErrorsDialog (address.PropertiesValidationErrorMessages, (Gtk.Window)this.Toplevel);
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

