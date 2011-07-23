using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.Components.Validator;

namespace HumanRightsTracker.Models
{
	[ActiveRecord("states")]
	public class State : ActiveRecordValidationBase<State>
	{
		[PrimaryKey]
        public int Id { get; protected set; }
		
		[Property]
		[ValidateNonEmpty]
        public String Name { get; set; }
		
		[Property("country_id")]
		[ValidateNonEmpty]
        public int CountryId { get; set; }
		
		[BelongsTo("country_id")]
        public Country Country { get; set; }
	}
}
