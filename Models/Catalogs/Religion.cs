using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.Components.Validator;

namespace HumanRightsTracker.Models
{
	[ActiveRecord("religions")]
	public class Religion : ActiveRecordValidationBase<Religion>
	{
		[PrimaryKey]
        public int Id { get; protected set; }
		
		[Property]
        [ValidateNonEmpty]
        [ValidateIsUnique]
   		public String Name { get; set; }
				
	}
}
