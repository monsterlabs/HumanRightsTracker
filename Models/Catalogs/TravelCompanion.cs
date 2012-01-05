using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.Components.Validator;

namespace HumanRightsTracker.Models
{
    [ActiveRecord("travel_companions")]
    public class TravelCompanion : ActiveRecordValidationBase<TravelCompanion>
    {

        [PrimaryKey]
        public int Id { get; protected set; }

        [Property]
        [ValidateNonEmpty]
        public String Name { get; set; }
    }
}