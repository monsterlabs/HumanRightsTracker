using System;
using System.Collections.Generic;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework.Internal;

namespace HumanRightsTracker.Models
{
    public class ARComparer<T> : IEqualityComparer<T> where T : ActiveRecordBase {
        public ARComparer() {}
        public bool Equals(T x, T y) {
            ActiveRecordModel model = ActiveRecordModel.GetModel(x.GetType());
            PrimaryKeyModel pkModel = model.PrimaryKey;

            int xId = (int)pkModel.Property.GetValue(x, null);
            int yId = (int)pkModel.Property.GetValue(x, null);

            return yId == xId;
        }

        public int GetHashCode(T x) {
            ActiveRecordModel model = ActiveRecordModel.GetModel(x.GetType());
            PrimaryKeyModel pkModel = model.PrimaryKey;

            int xId = (int)pkModel.Property.GetValue(x, null);
            return xId;
        }
    }
}

