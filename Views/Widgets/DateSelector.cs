using System;
namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class DateSelector : Gtk.Bin
    {
        DateTime currentDate;

        public DateSelector ()
        {
            this.Build ();
        }

        public DateTime CurrentDate {
            get { return currentDate; }
            set {
                if (value.Year > 1) {
                    currentDate = value;
                    dateEntry.Text = value.ToLongDateString ();
                }
            }
        }

        protected virtual void openSelector (object sender, System.EventArgs e)
        {
            int x, y;
            this.ParentWindow.GetPosition (out x, out y);
            x += this.Allocation.Left;
            y += this.Allocation.Top + this.Allocation.Height;
            new DateSelectorWindow (x, y, currentDate, OnPopupDateChanged);
        }

        private void OnPopupDateChanged (object sender, DateEventArgs args)
        {
            CurrentDate = args.Date;
        }
    }
}

