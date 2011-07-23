using System;
using NHibernate.Criterion;
using HumanRightsTracker.Models;

namespace Views
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class PlaceSelector : Gtk.Bin
	{
		public PlaceSelector ()
		{
			this.Build ();
		}
		
		protected virtual void onCountryChanged (object sender, System.EventArgs e)
		{
			state.FilterBy(new ICriterion[] { Restrictions.Eq("CountryId", ((Country) country.Active).Id) });
		}
		
	}
}

