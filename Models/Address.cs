using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;

namespace HumanRightsTracker.Models
{
	[ActiveRecord("addresses")]
	public class Address : ActiveRecordLinqBase<Address>
	{
		[PrimaryKey]
        public int Id { get; protected set; }
		
		[Property]
        public String Location { get; set; }
		[Property]
        public String phone { get; set; }
		[Property]
        public String mobile { get; set; }
		[Property]
        public String zipcode { get; set; }
		
		[BelongsTo("country_id")]
        public Country Country { get; set; }
		[BelongsTo("state_id")]
        public State State { get; set; }
		[BelongsTo("city_id")]
        public City City { get; set; }
		[BelongsTo("person_id")]
        public Person Person { get; set; }
	}
}
