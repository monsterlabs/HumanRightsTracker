using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;

namespace HumanRightsTracker.Models
{
	[ActiveRecord("countries")]
	public class Country : ActiveRecordLinqBase<Country>
	{
		[PrimaryKey]
        public int Id { get; protected set; }
		
		[Property]
        public String Code { get; set; }
	}
}
