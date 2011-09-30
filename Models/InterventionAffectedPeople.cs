using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.Components.Validator;

namespace HumanRightsTracker.Models
{
    [ActiveRecord("intervention_affected_people")]
    public class InterventionAffectedPeople
    {
        [PrimaryKey]
        public int Id { get; protected set; }

        [BelongsTo("intervention_id")]
        public Intervention Intervention { get; set;}

        [BelongsTo("people_id")]
        public Person person { get; set;}

    }
}

