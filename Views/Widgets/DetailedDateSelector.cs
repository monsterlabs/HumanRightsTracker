using System;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class DateFieldsSelector : Gtk.Bin
    {
        DateTime currentDate;
        bool isEditable;

        public DateFieldsSelector ()
        {
            this.Build ();
            currentDate = DateTime.Now;
        }

        public DateTime CurrentDate {
            get { return currentDate; }
            set {
                if (value.Year > 1) {
                    currentDate = value;
                    day.Text = CurrentDate.Day.ToString ();
                    month.Active = CurrentDate.Month - 1;
                    year.Text = CurrentDate.Year.ToString ();
                }
            }
        }

        public void Full () {
            day.Visible = true;
            change.Visible = true;
            month.Visible = true;
            year.Visible = true;
        }

        public void WithoutDay () {
            day.Visible = false;
            change.Visible = false;
            month.Visible = true;
            year.Visible = true;
        }

        public void YearOnly () {
            day.Visible = false;
            change.Visible = false;
            month.Visible = false;
            year.Visible = true;
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

        public bool IsEditable {
            get {
                return this.isEditable;
            }
            set {
                isEditable = value;
                change.Visible = value;
            }
        }

        protected void OnChangeDay (object sender, System.EventArgs e)
        {
            currentDate = new DateTime (currentDate.Year, currentDate.Month, Convert.ToInt32 (day.Text));
        }

        protected void OnChangeMonth (object sender, System.EventArgs e)
        {
            currentDate = new DateTime (currentDate.Year, month.Active + 1, currentDate.Day);
        }

        protected void OnChangeYear (object sender, System.EventArgs e)
        {
            currentDate = new DateTime (Convert.ToInt32 (year.Text), currentDate.Month, currentDate.Day);
        }
    }
}

