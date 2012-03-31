using System;
using System.Collections;
using HumanRightsTracker.Models;
using Mono.Unix;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class PerpetratorActShow : Gtk.Bin, IEditable
    {
        PerpetratorAct perpetratorAct;
        bool isEditable;

        public event EventHandler Saved;

        public PerpetratorActShow ()
        {
            this.Build ();
        }

        public PerpetratorAct PerpetratorAct
        {
            get { return perpetratorAct; }
            set {
                perpetratorAct = value;
                humanRight.Active = value.HumanRightsViolation;
                if (perpetratorAct.Id < 1) {
                    humanRight.FilterByCategoryId (value.Perpetrator.Victim.Act.HumanRightsViolationCategory.Id);
                } else {
                    humanRight.FilterByCategoryId (value.HumanRightsViolation.CategoryId);
                }
                place.Active = perpetratorAct.ActPlace;
                location.Buffer.Text = value.Location ?? "";
                IsEditable = false;
            }
        }

        public bool IsEditable
        {
            get { return this.isEditable; }
            set {
                isEditable = value;
                saveButton.Visible = value;
                humanRight.IsEditable = value;
                place.IsEditable = value;
                location.Editable = value;
            }
        }

        protected virtual void OnSave (object sender, System.EventArgs e)
        {
            bool newRow = perpetratorAct.Id < 1 ? true : false;
            perpetratorAct.HumanRightsViolation = humanRight.Active as HumanRightsViolation;
            perpetratorAct.ActPlace = place.Active as ActPlace;
            perpetratorAct.Location = location.Buffer.Text;

            if (perpetratorAct.IsValid()) {
                if (newRow) {
                    perpetratorAct.Perpetrator.PerpetratorActs.Add (PerpetratorAct);
                }

                if (perpetratorAct.Perpetrator.Id > 0) {
                    perpetratorAct.Perpetrator.SaveAndFlush ();
                }
                this.IsEditable = false;

                if (Saved != null)
                     Saved (this.PerpetratorAct, e);
            }  else {
                Console.WriteLine( String.Join(",", perpetratorAct.ValidationErrorMessages) );
                new ValidationErrorsDialog (perpetratorAct.PropertiesValidationErrorMessages, (Gtk.Window)this.Toplevel);
            }
        }
    }
}

