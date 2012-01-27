using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.Components.Validator;

namespace HumanRightsTracker.Models
{
    [ActiveRecord("cities")]
    public class City : ActiveRecordValidationBase<City>
    {
        [PrimaryKey]
        public int Id { get; protected set; }

        [Property]
        [ValidateNonEmpty]
        public String Name { get; set; }

        [Property("state_id")]
        [ValidateNonEmpty]
        public int StateId { get; set; }

        public int ParentId {
            get {
                return this.StateId;
            }
            set {
                this.StateId = value;
            }
        }


        public string ParentName () {
            return State.Find(this.StateId).Name;
        }

        public string ParentModel () {
            return "State";
        }
    }
}
