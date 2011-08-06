using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.Components.Validator;

namespace HumanRightsTracker.Models
{
    [ActiveRecord("human_rights_violations")]
    public class HumanRightsViolation : ActiveRecordValidationBase<HumanRightsViolation>
    {
        public HumanRightsViolation ()
        {
        }

        [PrimaryKey]
        public int Id { get; protected set; }

        [Property]
        [ValidateNonEmpty]
        public String Name { get; set; }

        [Property("category_id")]
        public int CategoryId { get; set; }

        //[BelongsTo("parent_id")]
        //public HumanRightsViolation Parent { get; set; }
    }
}

