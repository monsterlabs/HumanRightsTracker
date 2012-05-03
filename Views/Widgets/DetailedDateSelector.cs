using System;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class DetailedDateSelector : Gtk.Bin, IEditable
    {
        DateTime? currentDate;
        bool isEditable;
        bool hideChangeButton;

        public event EventHandler Changed;

        public DetailedDateSelector ()
        {
            this.Build ();
            this.hideChangeButton = false;
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
            hideChangeButton = false;
        }

        public void WithoutDay () {
            day.Visible = false;
            change.Visible = false;
            hideChangeButton = true;
            month.Visible = true;
            year.Visible = true;
        }

        public void YearOnly () {
            day.Visible = false;
            change.Visible = false;
            hideChangeButton = true;
            month.Visible = false;
            year.Visible = true;
        }

        protected virtual void openSelector (object sender, System.EventArgs e)
        {

            int x, y;
            this.TranslateCoordinates(this.Toplevel, 0, 0, out x, out y);
            DateTime date = DateTime.Now;

            if (CurrentDate.HasValue)
            {
                date = CurrentDate.Value;
            }

            DateSelectorWindow selector =  new DateSelectorWindow (x, y, date, OnPopupDateChanged, (Gtk.Window)this.Toplevel);
            selector.TransientFor = (Gtk.Window)this.Toplevel;
            selector.Modal = true;
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
                if (hideChangeButton)
                    change.Hide ();
                else
                    change.Show ();
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

        protected void OnYearEditingDone (object sender, System.EventArgs e)
        {
            DateTime defaultTime = DateTime.Now;
            currentDate = new DateTime (Convert.ToInt32 (year.Text), defaultTime.Month, defaultTime.Day);
        }

        protected void OnYearTextInserted (object o, Gtk.TextInsertedArgs args)
        {
            DateTime defaultTime = DateTime.Now;
            int y;
            bool isNum = int.TryParse(year.Text, out y);
            if (!isNum) {
                y = defaultTime.Year;
            }

            int m;
            if (month.Active > 0) {
                m = month.Active + 1;
                currentDate = new DateTime (y, month.Active + 1 , defaultTime.Day);
            } else {
                m =  defaultTime.Month;

                currentDate = new DateTime (y, defaultTime.Month, defaultTime.Day);
            }

            int d;
            bool dayIsNum = int.TryParse(day.Text, out d);
            if (!dayIsNum) {
                d = defaultTime.Day;
            }
            currentDate = new DateTime (y, m, d);
        }

        protected void OnClear (object sender, System.EventArgs e)
        {
            this.CurrentDate = null;
        }
    }
}

