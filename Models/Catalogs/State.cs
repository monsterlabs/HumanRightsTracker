using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;

namespace HumanRightsTracker.Models
{
	[ActiveRecord("states")]
	public class State : ActiveRecordLinqBase<State>
	{
		[PrimaryKey]
        public int Id { get; protected set; }
		
		[Property]
        public String Name { get; set; }
		
		[BelongsTo("country_id")]
        public Country Country { get; set; }
	}
}
