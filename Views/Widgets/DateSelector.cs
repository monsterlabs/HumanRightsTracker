using System;
namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class DateSelector : Gtk.Bin
    {
        DateTime currentDate;
        bool isEditable;

        public event EventHandler Changed;

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
                } else {
                    dateEntry.Text = "";
                }
            }
        }

        protected virtual void openSelector (object sender, System.EventArgs e)
        {
            int x, y;
            this.ParentWindow.GetPosition (out x, out y);
            x += this.Allocation.Left;
            y += this.Allocation.Top + this.Allocation.Height;
            DateTime selectedDate = currentDate;

            if (currentDate.Year == 1)
            {
                selectedDate = new DateTime(1975, 1, 1);
            }

            new DateSelectorWindow (x, y, selectedDate, OnPopupDateChanged);
        }

        private void OnPopupDateChanged (object sender, DateEventArgs args)
        {
            CurrentDate = args.Date;
            if (Changed != null)
                Changed(args.Date, args);
        }

        public bool IsEditable {
            get {
                return this.isEditable;
            }
            set {
                isEditable = value;
                dateEntry.Visible = value;
                button1.Visible = value;
                text.Visible = !value;
                text.Text = dateEntry.Text;
            }
        }
    }
}

