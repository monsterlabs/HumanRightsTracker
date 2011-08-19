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

        [Property("state_id")]
        public int ParentId {
            get {
                return this.StateId;
            }
            set {
                this.StateId = value;
            }
        }

        [Property("state_id")]
        public string ParentName {
            get {
                return State.Find(this.StateId).Name;
            }
            protected set {}
        }

        [Property("state_id")]
        public string ParentModel {
            get {
                return "State";
            }
            protected set {}
        }

        [BelongsTo("state_id")]
        public State State { get; set; }
    }
}
