using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.Components.Validator;

namespace HumanRightsTracker.Models
{
	[ActiveRecord("ethnic_groups")]
	public class EthnicGroup : ActiveRecordValidationBase<EthnicGroup>
	{
		[PrimaryKey]
        public int Id { get; protected set; }
		
		[Property]
		[ValidateNonEmpty]
		[ValidateIsUnique]
   		public String Name { get; set; }	
	}
}
