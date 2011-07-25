using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.Components.Validator;

namespace HumanRightsTracker.Models
{
    [ActiveRecord("date_types")]
    public class DateType : ActiveRecordValidationBase<DateType>
    {
        [PrimaryKey]
        public int Id { get; protected set; }

        [Property]
        [ValidateNonEmpty]
        [ValidateIsUnique]
        public String Name { get; set; }
    }
}
