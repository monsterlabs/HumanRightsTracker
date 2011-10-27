using System;
using HumanRightsTracker.Models;
using Mono.Unix;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class PerpetratorActShow : Gtk.Bin
    {
        PerpetratorAct perpetratorAct;
        bool isEditing;

        public event EventHandler Unsaved;
        public event EventHandler Saved;
        public event EventHandler Cancel;

        public PerpetratorActShow ()
        {
            this.Build ();
        }

        public PerpetratorAct PerpetratorAct
        {
            get {return perpetratorAct;}
            set {
                perpetratorAct = value;
                humanRight.Active = value.HumanRightsViolation;
                place.Active = value.ActPlace;
                location.Buffer.Text = value.Location;

                IsEditing = false;
            }
        }

        protected virtual void OnToggleEdit (object sender, System.EventArgs e)
        {
            IsEditing = !IsEditing;
        }

        public bool IsEditing
        {
            get { return this.isEditing; }
            set
            {
                isEditing = value;
                if (value) {
                    editButton.Label = Catalog.GetString("Cancel");
                    saveButton.Visible = true;
                } else {
                    editButton.Label = Catalog.GetString("Edit");
                    saveButton.Visible = false;
                }
                humanRight.IsEditable = value;
                place.IsEditable = value;
                location.Editable = value;
            }
        }

        protected virtual void OnSave (object sender, System.EventArgs e)
        {
            perpetratorAct.HumanRightsViolation = humanRight.Active as HumanRightsViolation;
            perpetratorAct.ActPlace = place.Active as ActPlace;
            perpetratorAct.Location = location.Buffer.Text;

            if (perpetratorAct.Id < 1 || perpetratorAct.Perpetrator.Id < 1)
            {
                if (Unsaved != null)
                    Unsaved (this.PerpetratorAct, e);
                return;
            } else {
                perpetratorAct.Save();
                if (Saved != null)
                    Saved (this.PerpetratorAct, e);
            }
        }
    }
}

