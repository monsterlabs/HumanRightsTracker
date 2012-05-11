using System;
namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class DateSelector : Gtk.Bin, IEditable
    {
        DateTime? currentDate;
        DateTime? defaultDateForCalendar;
        bool isEditable;

        public event EventHandler Changed;

        public DateSelector ()
        {
            this.Build ();
            CurrentDateForCalendar = DateTime.Now;
        }

        public DateTime? CurrentDate {
            get { return currentDate; }
            set {
                currentDate = value;
                if (value != null) {
                    dateEntry.Text = String.Format("{0:dd/MM/yyyy}", value);
                } else {
                    dateEntry.Text = "";
                }
            }
        }

        public DateTime? CurrentDateForCalendar {
            set {
                defaultDateForCalendar = value;
            }
        }

        protected virtual void openSelector (object sender, System.EventArgs e)
        {
            int x, y;
            this.TranslateCoordinates(this.Toplevel, 0, 0, out x, out y);

            DateTime? selectedDate = currentDate;

            if (selectedDate == null)
            {
                selectedDate = defaultDateForCalendar;
            }

            DateSelectorWindow selector = new DateSelectorWindow (x, y, selectedDate.Value, OnPopupDateChanged, (Gtk.Window)this.Toplevel);
            selector.TransientFor = (Gtk.Window)this.Toplevel;
            selector.Modal = true;
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
                clearButton.Visible = value;
            }
        }

        protected void OnClear (object sender, System.EventArgs e)
        {
            this.CurrentDate = null;
        }
    }
}

