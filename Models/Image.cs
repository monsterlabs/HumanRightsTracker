using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.ActiveRecord.Queries;
using Castle.Components.Validator;

namespace HumanRightsTracker.Models
{
    [ActiveRecord("images")]
    public class Image : ActiveRecordValidationBase<Image>
    {
        [PrimaryKey]
        public int Id { get; protected set; }

        [Property]
        public Byte[] Original { get; set; }
        [Property]
        public Byte[] Thumbnail { get; set; }
        [Property]
        public Byte[] Icon { get; set; }

        [Property("imageable_type")]
        public String ImageableType { get; set; }
        [Property("imageable_id")]
        public int ImageableId { get; set; }
    }
}
