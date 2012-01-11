using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.Components.Validator;
using System.Collections;

namespace HumanRightsTracker.Models
{
    [ActiveRecord("places")]
    public class Place : ActiveRecordValidationBase<Place>
    {
        [PrimaryKey]
        public int Id { get; protected set; }

        [BelongsTo("case_id")]
        public Case Case { get; set; }

        [BelongsTo("country_id")]
        public Country Country { get; set; }

        [BelongsTo("state_id")]
        public State State { get; set; }

        [BelongsTo("city_id")]
        public City City { get; set; }
    }
}