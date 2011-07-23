using System;
namespace Views
{
	public class DateEventArgs : EventArgs
    {
         private DateTime date;
                
        public DateTime Date 
        {
                get 
                {
                    return date;
                }
        }
        
        public DateEventArgs( DateTime date )
        {
            this.date = date;
        }
    }
	
	
	public partial class DateSelectorWindow : Gtk.Window
	{
		public delegate void DateEventHandler(object sender, DateEventArgs args);

        public event DateEventHandler OnChange = null;
		
		public DateSelectorWindow ( int x, int y, DateTime defDate, DateEventHandler handler ) : base(Gtk.WindowType.Popup)
        {       
            this.Move( x, y );
            this.Build();
            this.OnChange  = handler;

            //TxtHour.Value  = defDate.Hour;
            //TxtMin.Value   = defDate.Minute;
            //TxtSec.Value   = defDate.Second;
            cal.Date = defDate;
                        
            //RefreshClock();
		}
		
		public DateTime CurrentDate
        {
            get
            {
                DateTime d = cal.Date;
                return new DateTime( d.Year, d.Month, d.Day );
            }
        }
		
		protected virtual void OnSelect (object sender, System.EventArgs e)
		{
			if( OnChange != null ) 
				OnChange( this, new DateEventArgs( CurrentDate ) );
			this.Destroy();
		}
	}
}

