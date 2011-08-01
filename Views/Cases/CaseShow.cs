using System;
using HumanRightsTracker.Models;
using Mono.Unix;
namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class CaseShow : Gtk.Bin
    {
        public CaseShow ()
        {
            this.Build ();
            nameEntry.Text = "";
        }


    }
}

