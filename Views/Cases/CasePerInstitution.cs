using System;
using System.Collections.Generic;
using HumanRightsTracker.Models;
using NHibernate.Criterion;
using System.Diagnostics;
namespace Views
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class CasePerInstitution : Gtk.Bin
    {

        Institution i;
        bool isEditable;
        private EditableHelper editable_helper;

        public CasePerInstitution ()
        {
            this.Build ();
            this.editable_helper = new EditableHelper(this);
        }

        public Institution Institution
        {
            get { return i; }
            set
            {
                i = value;
                ReloadList ();
            }
        }

        public bool IsEditable {
            get { return isEditable; }
            set {
                isEditable = value;
                this.editable_helper.SetAllEditable (value);
            }
        }


        public void ReloadList ()
        {
            foreach (Gtk.Widget child in case_vbox.Children) {
                child.Destroy();
            }

            foreach (Case c in i.caseList ())
            {
                case_vbox.PackStart (new CaseAndPeopleRow (c));

            }
            case_vbox.ShowAll ();
            this.editable_helper.UpdateEditableWidgets();
            this.editable_helper.SetAllEditable (false);
        }


    }
}

















