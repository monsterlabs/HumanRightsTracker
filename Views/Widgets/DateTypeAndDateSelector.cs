using System;
using HumanRightsTracker.Models;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class DateTypeAndDateSelector : Gtk.Bin
    {

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
            if (date != null)
                datefield.CurrentDate = date;
        }

        public void setDateType (DateType type) {
            if (type != null)
                dateType.Active = type;
        }
    }
}

