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
                if (value != default(DateTime)) {
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
            this.Parent.TranslateCoordinates(this.Toplevel, this.Allocation.Left, this.Allocation.Bottom, out x, out y);

            x += this.ParentWindow.FrameExtents.Left;
            y += this.ParentWindow.FrameExtents.Top;

            DateTime selectedDate = currentDate;

            if (currentDate.Year == 1)
            {
                selectedDate = new DateTime(1975, 1, 1);
            }

            DateSelectorWindow selector = new DateSelectorWindow (x, y, selectedDate, OnPopupDateChanged);
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
            }
        }
    }
}

