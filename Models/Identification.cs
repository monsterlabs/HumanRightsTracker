using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.Components.Validator;

namespace HumanRightsTracker.Models
{
    [ActiveRecord("identifications")]
    public class Identification : ActiveRecordValidationBase<Identification>
    {
        [PrimaryKey]
        public int Id { get; protected set; }

        [Property("identification_number")]
        public String IdentificationNumber { get; set; }

        [BelongsTo("person_id")]
        public Person Person { get; set; }

        [BelongsTo("identification_type_id")]
        public IdentificationType IdentificationType { get; set; }
    }
}
