using System;
using HumanRightsTracker.Models;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class DateTypeAndDateSelector : Gtk.Bin
    {
        bool isEditable;

        public DateTypeAndDateSelector ()
        {
            this.Build ();
        }

        protected void OnChangeType (object sender, System.EventArgs e)
        {
            DateType type = dateType.Active as DateType;
            if (type.Name == "Fecha exacta" || type.Name == "Fecha aproximada")
            {
                datefield.Full ();
            } else if (type.Name == "Se desconoce el día")
            {
                datefield.WithoutDay ();
            } else if (type.Name == "Se desconoce el día y el mes")
            {
                datefield.YearOnly ();
            }
        }

        public DateTime? SelectedDate () {
            return datefield.CurrentDate;
        }

        public DateType SelectedDateType () {
            return dateType.Active as DateType;
        }

        public void setDate (DateTime? date) {
            datefield.CurrentDate = date;
            if (date.HasValue)
            {
                label2.Text = date.Value.ToShortDateString ();
            } else {
                label2.Text = "";
            }
        }

        public void setDateType (DateType type) {
            dateType.Active = type;
        }

        public bool IsEditable {
            get {
                return this.isEditable;
            }
            set {
                dateType.IsEditable = value;
                isEditable = value;
                hbox5.Visible = value;
                label2.Visible = !value;
            }
        }
    }
}

