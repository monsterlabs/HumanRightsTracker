using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.Components.Validator;

namespace HumanRightsTracker.Models
{
    [ActiveRecord("indigenous_languages")]
    public class IndigenousLanguage : ActiveRecordValidationBase<IndigenousLanguage>
    {

        [PrimaryKey]
        public int Id { get; protected set; }

        [Property]
        [ValidateNonEmpty]
        public String Name { get; set; }

        [Property]
        public String Notes { get; set; }

        [BelongsTo("country_id"), ValidateNonEmpty]
        public Country Country { get; set; }

    }
}