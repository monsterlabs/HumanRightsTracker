using System;
using HumanRightsTracker.Models;
using System.Collections;

namespace Views
{
    public partial class VictimsWindow : Gtk.Window
    {
        Act act;

        public VictimsWindow (Act act, Gtk.Window parent) : base(Gtk.WindowType.Toplevel)
        {
            this.Build ();
            this.TransientFor = parent;
            this.Modal = true;
            this.act = act;
            Initialize ();
        }

        void Initialize () {
            victimlist.Act = act;
            if (act.Victims == null || act.Victims.Count == 0) {
                Victim victim = new Victim ();
                victim.Act = act;
                victim.Perpetrators = new ArrayList ();
                victim.Act.Victims = victim.Act.Victims ?? new ArrayList ();
                show.Victim = victim;
            }
        }

        protected void OnVictimSelected (object sender, System.EventArgs e)
        {
            Victim v = sender as Victim;
            if (v != null) {
                show.Victim = v;
            }
        }

        protected void OnAddVictim (object sender, System.EventArgs e)
        {
            Victim v = new Victim ();
            v.Act = act;
            victimlist.UnselectAll ();

            show.Victim = v;
            show.Show ();
            show.IsEditable = true;
        }

        protected void OnShowSaved (object sender, System.EventArgs e)
        {
            victimlist.ReloadStore ();
            Victim v = sender as Victim;
            show.Victim = v;
            show.Show ();
            show.IsEditable = false;
        }

        protected void OnRemove (object sender, System.EventArgs e)
        {
            Victim v = show.Victim as Victim;
            v.Act.Victims.Remove(v);
            v.DeleteAndFlush ();
            victimlist.ReloadStore ();
        }
    }
}

