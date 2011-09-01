using System;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class DetailedDateSelector : Gtk.Bin
    {
        DateTime? currentDate;
        bool isEditable;

        public event EventHandler Changed;

        public DetailedDateSelector ()
        {
            this.Build ();
        }

        public DateTime? CurrentDate {
            get { return currentDate; }
            set {
                currentDate = value;
                if (value.HasValue) {
                    day.Text = CurrentDate.Value.Day.ToString ();
                    month.Active = CurrentDate.Value.Month - 1;
                    year.Text = CurrentDate.Value.Year.ToString ();
                } else {
                    day.Text = "";
                    month.Active = -1;
                    year.Text = "";
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
            if (CurrentDate.HasValue)
            {
                new DateSelectorWindow (x, y, CurrentDate.Value, OnPopupDateChanged);
            } else
            {
                new DateSelectorWindow (x, y, DateTime.Now, OnPopupDateChanged);
            }
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
            if (CurrentDate.HasValue)
            {
                currentDate = new DateTime (CurrentDate.Value.Year, CurrentDate.Value.Month, Convert.ToInt32 (day.Text));
            } else {
                DateTime defaultTime = DateTime.Now;
                currentDate = new DateTime (defaultTime.Year, defaultTime.Month, Convert.ToInt32 (day.Text));
            }
            DispatchChanged (e);
        }

        protected void OnChangeMonth (object sender, System.EventArgs e)
        {

            if (CurrentDate.HasValue)
            {
                currentDate = new DateTime (CurrentDate.Value.Year, month.Active + 1, CurrentDate.Value.Day);
            } else {
                if (month.Active < 0)
                {
                    return;
                }
                DateTime defaultTime = DateTime.Now;
                currentDate = new DateTime (defaultTime.Year, month.Active + 1, defaultTime.Day);
            }
            DispatchChanged (e);
        }

        protected void OnChangeYear (object sender, System.EventArgs e)
        {
            if (CurrentDate.HasValue)
            {
                currentDate = new DateTime (Convert.ToInt32 (year.Text), CurrentDate.Value.Month, CurrentDate.Value.Day);
            } else {
                DateTime defaultTime = DateTime.Now;
                currentDate = new DateTime (Convert.ToInt32 (year.Text), defaultTime.Month, defaultTime.Day);
            }
            DispatchChanged (e);
        }

        private void DispatchChanged (System.EventArgs e)
        {
            if (Changed != null)
                Changed(this, e);
        }
    }
}

