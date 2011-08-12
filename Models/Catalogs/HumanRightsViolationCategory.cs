using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.Components.Validator;

namespace HumanRightsTracker.Models
{
    [ActiveRecord("human_rights_violation_categories")]
    public class HumanRightsViolationCategory : ActiveRecordValidationBase<HumanRightsViolationCategory>
    {
        public HumanRightsViolationCategory ()
        {
        }

        [PrimaryKey]
        public int Id { get; protected set; }

        [Property]
        [ValidateNonEmpty]
        public String Name { get; set; }
    }
}

