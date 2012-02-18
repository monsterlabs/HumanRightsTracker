using System;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace HumanRightsTracker.Models
{
    public class ActiveRecordMetaBase : ActiveRecordBase
    {
        public ActiveRecordMetaBase ()
        {
        }

        public static Object FindFirst(Type t, ICriterion[] criteria)
        {
            return FindOne(t, criteria);
        }

        public static Array All(Type t)
        {
            return FindAll(t);
        }

         public static Array All(Type t, Order order)
        {
            return All(t, new Order[] { order });
        }

        public static Array All(Type t, Order[] order)
        {
            return FindAll(t, order, new ICriterion[0]);
        }

        public static Array Where(Type t, ICriterion[] criteria, Order order)
        {
            return Where(t, criteria, new Order[] { order } );
        }

        public static Array Where(Type t, ICriterion[] criteria, Order[] order)
        {
            return FindAll(t, order, criteria);
        }

        public static new void Save(Object record)
        {
            SaveAndFlush(record);
        }

        public static new void Delete(Object record)
        {
            DeleteAndFlush(record);
        }
    }
}

