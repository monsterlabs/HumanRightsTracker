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

        public DateTime SelectedDate () {
            return datefield.CurrentDate;
        }

        public DateType SelectedDateType () {
            return dateType.Active as DateType;
        }

        public void setDate (DateTime date) {
            if (date.Year > 1)
            {
                datefield.CurrentDate = date;
                label2.Text = date.ToShortDateString ();
            }
        }

        public void setDateType (DateType type) {
            if (type != null)
                dateType.Active = type;
        }

        public bool IsEditable {
            get {
                return this.isEditable;
            }
            set {
                isEditable = value;
                hbox5.Visible = value;
                label2.Visible = !value;
            }
        }
    }
}

