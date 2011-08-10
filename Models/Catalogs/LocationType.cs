using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.Components.Validator;

namespace HumanRightsTracker.Models
{
    [ActiveRecord("location_types")]
    public class LocationType : ActiveRecordValidationBase<LocationType>
    {

        [PrimaryKey]
        public int Id { get; protected set; }

        [Property]
        [ValidateNonEmpty]
        public String Name { get; set; }
    }
}