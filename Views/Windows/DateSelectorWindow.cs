using System;
namespace Views
{
    public class DateEventArgs : EventArgs
    {
        private DateTime date;

        public DateTime Date {
            get { return date; }
        }

        public DateEventArgs (DateTime date)
        {
            this.date = date;
        }
    }


    public partial class DateSelectorWindow : Gtk.Window
    {
        public delegate void DateEventHandler (object sender, DateEventArgs args);

        Gtk.Window _parent;

        public event DateEventHandler OnChange = null;

        public DateSelectorWindow (int x, int y, DateTime defDate, DateEventHandler handler, Gtk.Window parent) : base(Gtk.WindowType.Popup)
        {
            this.TransientFor = parent;
            _parent = parent;
            _parent.Modal = false;
            this.Modal = true;
            this.Build ();
            this.Move (x, y);
            this.WindowPosition = Gtk.WindowPosition.None;
            this.OnChange = handler;
            cal.Date = defDate;
        }

        public DateTime CurrentDate {
            get {
                DateTime d = cal.Date;
                return new DateTime (d.Year, d.Month, d.Day);
            }
        }

        protected virtual void OnSelect (object sender, System.EventArgs e)
        {
            if (OnChange != null)
                OnChange (this, new DateEventArgs (CurrentDate));
            _parent.Modal = true;
            this.Destroy ();
        }

        protected void OnCancel (object sender, System.EventArgs e)
        {
            _parent.Modal = true;
            this.Destroy();
        }

        protected void OnCalDaySelected (object sender, System.EventArgs e)
        {
        }
    }
}
