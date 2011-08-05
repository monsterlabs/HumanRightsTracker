using System;
using HumanRightsTracker.Models;

namespace Views
{
    [Gtk.TreeNode (ListOnly=true)]
    public class ActNode : Gtk.TreeNode
    {
        public ActNode (Act act)
        {
            Act = act;
            ActTitle = act.HumanRightsViolation.Name;
        }

        public Act Act;
        [Gtk.TreeNodeValue (Column=0)]
        public String ActTitle;
    }

    [System.ComponentModel.ToolboxItem(true)]
    public partial class ActsList : Gtk.Bin
    {
        public ActsList ()
        {
            this.Build ();
        }
    }
}

