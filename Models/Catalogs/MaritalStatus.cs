using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.Components.Validator;

namespace HumanRightsTracker.Models
{
	[ActiveRecord("marital_statuses")]
	public class MaritalStatus : ActiveRecordValidationBase<MaritalStatus>
	{
		[PrimaryKey]
        public int Id { get; protected set; }
		
		[Property]
		[ValidateNonEmpty]
        [ValidateIsUnique]
        public String Name { get; set; }
	}
}
