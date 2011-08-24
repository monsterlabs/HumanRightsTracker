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
            state.Sensitive = false;
            city.Sensitive = false;
        }

        protected virtual void onCountryChanged (object sender, System.EventArgs e)
        {
            city.Active = null;
            city.Sensitive = false;
            state.Active = null;
            state.Sensitive = false;

            if (country.Active != null)
            {
                state.Sensitive = true;
                state.FilterBy (new ICriterion[] { Restrictions.Eq ("CountryId", ((Country)country.Active).Id) }, ((Country)country.Active).Id);
                state.HideAddButton = (((Country)country.Active).Name == "México");
            }
        }

        protected void OnStateChanged (object sender, System.EventArgs e)
        {
            city.Active = null;
            city.Sensitive = false;

            if (state.Active != null)
            {
                city.Sensitive = true;
                city.FilterBy (new ICriterion[] { Restrictions.Eq ("StateId", ((State)state.Active).Id) }, ((State)state.Active).Id);
            }
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

                Country cnty = country.Active as Country;
                country.IsEditable = isEditable;
                if (cnty != null) {
                    country_label.Text = cnty.Name;
                    country_label.Visible = !value;
                }
                State s = state.Active as State;
                if (s != null) {
                    state_label.Text = s.Name;
                    state_label.Visible = !value;
                }
                City c = city.Active as City;
                if (c != null) {
                    city_label.Text = (city.Active as City).Name;
                    city_label.Visible = !value;
                }
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

