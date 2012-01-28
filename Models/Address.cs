using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.Components.Validator;

namespace HumanRightsTracker.Models
{
    [ActiveRecord("addresses")]
    public class Address : ActiveRecordValidationBase<Address>
    {
        [PrimaryKey]
        public int Id { get; protected set; }

        [Property("location")]
        [ValidateNonEmpty]
        public String Location { get; set; }
        [Property("phone")]
        public String Phone { get; set; }
        [Property("mobile")]
        public String Mobile { get; set; }
        [Property("zipcode")]
        public String ZipCode { get; set; }

        [BelongsTo("country_id")]
        public Country Country { get; set; }
        [BelongsTo("state_id")]
        public State State { get; set; }
        [BelongsTo("city_id")]
        public City City { get; set; }
        [BelongsTo("person_id")]
        public Person Person { get; set; }
        [BelongsTo("address_type_id")]
        public AddressType AddressType { get; set; }

    }
}
