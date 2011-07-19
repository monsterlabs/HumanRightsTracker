using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;

namespace HumanRightsTracker.Models
{
	[ActiveRecord("cities")]
	public class City : ActiveRecordLinqBase<City>
	{
		[PrimaryKey]
        public int Id { get; protected set; }
		
		[Property]
        public String Name { get; set; }
		
		[BelongsTo("state_id")]
        public State State { get; set; }
	}
}
