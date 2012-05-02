using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.Components.Validator;

namespace HumanRightsTracker.Models
{
    [ActiveRecord("addresses")]
    public class Address : ActiveRecordValidationBase<Address>, ListableRecord
    {
        [PrimaryKey]
        public int Id { get; protected set; }

        [Property("location")]
        public String Location { get; set; }
        [Property("phone")]
        public String Phone { get; set; }
        [Property("mobile")]
        public String Mobile { get; set; }
        [Property("zipcode")]
        public String ZipCode { get; set; }

        [BelongsTo("country_id"), ValidateNonEmpty]
        public Country Country { get; set; }
        [BelongsTo("state_id")]
        public State State { get; set; }
        [BelongsTo("city_id")]
        public City City { get; set; }
        [BelongsTo("person_id")]
        public Person Person { get; set; }
        [BelongsTo("address_type_id"), ValidateNonEmpty]
        public AddressType AddressType { get; set; }


         public string[] ColumnData ()
        {
            string[] data = {
                "",
                "",
                "",
                "",
                ""
            };

            if (this.AddressType != null) {
                data[0] = this.AddressType.Name;
            }

            if (this.Location != null) {
                data[1] = this.Location;
            }

            if (this.Country != null) {
                data[2] = this.Country.Name;
            }

            if (this.State != null) {
                data[3] = this.State.Name;
            }

            if (this.City != null) {
                data[4] = this.City.Name;
            }
            return data;
        }

    }
}
