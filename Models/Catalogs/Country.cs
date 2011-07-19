using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.Components.Validator;

namespace HumanRightsTracker.Models
{
	[ActiveRecord("countries")]
	public class Country : ActiveRecordValidationBase<Country>
	{
		[PrimaryKey]
        public int Id { get; protected set; }
		
		[Property]
		[ValidateNonEmpty]
		[ValidateIsUnique]
        public String Name { get; set; }
		
		[Property]
		[ValidateNonEmpty]
		[ValidateIsUnique]
        public String Citizen { get; set; }
		
		[Property]
		[ValidateNonEmpty]
		[ValidateIsUnique]
        public String Code { get; set; }
	}
}
