using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.Components.Validator;

namespace HumanRightsTracker.Models
{
    [ActiveRecord("source_information_types")]
    public class SourceInformationType : ActiveRecordValidationBase<SourceInformationType>
    {

        [PrimaryKey]
        public int Id { get; protected set; }

        [Property]
        [ValidateNonEmpty]
        public String Name { get; set; }

        //[BelongsTo("parent_id")]
        //public HumanRightsViolation Parent { get; set; }
    }
}