using System;
using System.Collections.Generic;
using HumanRightsTracker.Models;
using Mono.Unix;
using System.Linq;

namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class CaseRow : Gtk.Bin
    {
        Case c;

        public CaseRow (Case c)
        {
            this.Build ();
            Case = c;
            ConnectPlacesHandlers ();
            ConnectActsHandlers ();
            ConnectCaseRelationshipHandlers ();
            ConnectInterventionsHandlers ();
        }
        
        public CaseRow ()
        {
            this.Build ();
            ConnectPlacesHandlers ();
            ConnectActsHandlers ();
            ConnectCaseRelationshipHandlers ();
            ConnectInterventionsHandlers ();

        }

        public Case Case
        {
            get { return c; }
            set {
                 c = value;
                 if (c != null) {
                    case_name.Text = "<b>" + c.Name + "</b>";
                    case_name.UseMarkup = true;
                    case_name1.Text = c.Name;
                    affected_people.Text = c.AffectedPeople.ToString ();
                    start_date.Text = String.Format("{0:MM/dd/yyyy}", c.start_date);
                    end_date.Text = String.Format("{0:MM/dd/yyyy}", c.end_date);
                    placeslist.Records = c.Places.Cast<ListableRecord>().ToList ();
                    act_list.Records = c.Acts.Cast<ListableRecord>().ToList();
                    interventionlist.Records = c.Interventions.Cast<ListableRecord>().ToList();
                    case_relationship_list.Records = c.CaseRelationships.Cast<ListableRecord>().ToList();
                    people_per_case.Case = c;
                }
            }
        }

        public void ConnectPlacesHandlers() {
            placeslist.DetailButtonPressed += (sender, e) => {
                Place p = sender as Place;
                new PlaceDetailWindow(p,  (Gtk.Window) this.Toplevel);
            };
        }

        public void ConnectActsHandlers () {
            act_list.DetailButtonPressed += (sender, e) => {
                Act record = sender as Act;
                new ActDetailWindow(record,(Gtk.Window) this.Toplevel);
            };
        }

        public void ConnectInterventionsHandlers() {
            interventionlist.DetailButtonPressed += (sender, e) => {
                Intervention i = sender as Intervention;
                new InterventionDetailWindow(i, (Gtk.Window) this.Toplevel);
            };
        }

        public void ConnectCaseRelationshipHandlers() {
            case_relationship_list.DetailButtonPressed += (sender, e) => {
                CaseRelationship record = sender as CaseRelationship;
                new CaseSummaryWindow(record.RelatedCase, (Gtk.Window) this.Toplevel);
            };
        }
    }
}

