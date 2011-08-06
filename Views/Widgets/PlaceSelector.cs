using System;
using NHibernate.Criterion;
using HumanRightsTracker.Models;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class PlaceSelector : Gtk.Bin
    {
        bool isEditable;

        public PlaceSelector ()
        {
            this.Build ();
        }

        protected virtual void onCountryChanged (object sender, System.EventArgs e)
        {
            if (country.Active != null)
                state.FilterBy (new ICriterion[] { Restrictions.Eq ("CountryId", ((Country)country.Active).Id) });
        }

        public void SetPlace(Country theCountry, State theState, City theCity)
        {
            country.Active = theCountry;
            state.Active = theState;
            city.Active = theCity;
        }

        public Country Country {
            get { return country.Active as Country; }
        }

        public new State State {
            get { return state.Active as State; }
        }

        public City City {
            get { return city.Active as City; }
        }

        public bool IsEditable {
            get {
                return this.isEditable;
            }
            set {
                isEditable = value;
                country.Visible = value;
                state.Visible = value;
                city.Visible = value;
                text11.Visible = !value;
                text11.Text = PlaceName();
            }
        }

        private String PlaceName() {
            String name = "";
            Country cnty = country.Active as Country;
            if (cnty != null)
            {
                name += cnty.Name;
                State st = state.Active as State;
                if (st != null)
                {
                    name += " " + st.Name;
                    City cty = city.Active as City;
                    if (cty != null)
                    {
                        name += " " + cty.Name;
                    }
                }
            }

            return name;
        }

    }
}

