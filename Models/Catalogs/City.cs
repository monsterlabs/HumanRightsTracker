using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.Components.Validator;

namespace HumanRightsTracker.Models
{
	[ActiveRecord("cities")]
	public class City : ActiveRecordValidationBase<City>
	{
		[PrimaryKey]
        public int Id { get; protected set; }
		
		[Property]
		[ValidateNonEmpty]
        public String Name { get; set; }
		
		[BelongsTo("state_id")]
        public State State { get; set; }
	}
}
