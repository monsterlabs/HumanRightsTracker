using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;

namespace HumanRightsTracker.Models
{
	[ActiveRecord("images")]
	public class Image : ActiveRecordLinqBase<Image>
	{
		[PrimaryKey]
        public int Id { get; protected set; }
		
		[Property]
        public Byte[] Original { get; set; }
		[Property]
        public Byte[] Thumbnail { get; set; }
		
		[Property]
        public String ImageableType { get; set; }
		[Property]
        public int ImageableId { get; set; }
	}
}
