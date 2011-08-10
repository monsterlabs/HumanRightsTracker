using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.Components.Validator;

namespace HumanRightsTracker.Models
{
    [ActiveRecord("institution_categories")]
    public class InstitutionCategory : ActiveRecordValidationBase<InstitutionCategory>
    {

        [PrimaryKey]
        public int Id { get; protected set; }

        [Property]
        [ValidateNonEmpty]
        public String Name { get; set; }
    }
}