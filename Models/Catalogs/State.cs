using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.Components.Validator;

namespace HumanRightsTracker.Models
{
    [ActiveRecord("states")]
    public class State : ActiveRecordValidationBase<State>
    {
        [PrimaryKey]
        public int Id { get; protected set; }

        [Property]
        [ValidateNonEmpty]
        public String Name { get; set; }

        [Property("country_id")]
        [ValidateNonEmpty]
        public int CountryId { get; set; }

        public int ParentId {
            get {
                return this.CountryId;
            }
            set {
                this.CountryId = value;
            }
        }


        public void SetParentId (int parent_id) {
           CountryId = parent_id;
        }

        public string ParentName () {
            return Country.Find(this.CountryId).Name;
        }

        public string ParentModel () {
            return "Country";
        }

    }
}