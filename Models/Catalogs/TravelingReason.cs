using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.Components.Validator;

namespace HumanRightsTracker.Models
{
    [ActiveRecord("traveling_reasons")]
    public class TravelingReason : ActiveRecordValidationBase<TravelingReason>
    {

        [PrimaryKey]
        public int Id { get; protected set; }

        [Property]
        [ValidateNonEmpty]
        [ValidateIsUnique]
        public String Name { get; set; }
    }
}