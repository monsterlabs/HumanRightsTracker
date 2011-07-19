using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;

namespace HumanRightsTracker.Models
{
	[ActiveRecord("people")]
	public class Person : ActiveRecordLinqBase<Person>
	{
		[PrimaryKey]
        public int Id { get; protected set; }
		
		[Property]
        public String Firstname { get; set; }
		[Property]
        public String Lastname { get; set; }
		[Property]
        public Boolean Gender { get; set; }
		[Property]
        public DateTime Birthday { get; set; }
		
		[BelongsTo("country_id")]
        public Country Country { get; set; }
		[BelongsTo("state_id")]
        public State State { get; set; }
		[BelongsTo("city_id")]
        public City City { get; set; }
		[BelongsTo("marital_status_id")]
        public MaritalStatus MaritalStatus { get; set; }
	}
}
