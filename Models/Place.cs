using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;

namespace HumanRightsTracker.Models
{
    [ActiveRecord("places")]
    public class Place : ActiveRecordValidationBase<Place>, ListableRecord
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

        public string[] ColumnData ()
        {
            string[] data = {
                "",
                "",
                ""
            };

            if (this.Country != null) {
                data[0] = this.Country.Name;
            }

            if (this.State != null) {
                data[1] = this.State.Name;
            }

            if (this.City != null) {
                data[2] = this.City.Name;
            }

            return data;
        }
    }
}