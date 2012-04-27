using System;

namespace HumanRightsTracker.MonoSqlite
{
    public class Driver : NHibernate.Driver.ReflectionBasedDriver
    {
        public Driver () :
            base("Mono.Data.Sqlite",
            "Mono.Data.Sqlite.SqliteConnection",
            "Mono.Data.Sqlite.SqliteCommand")
        {
        }

        public override bool UseNamedPrefixInParameter {
            get {
                return true;
            }
        }

        public override bool UseNamedPrefixInSql {
            get {
                return true;
            }
        }

        public override string NamedPrefix {
            get {
                return "@";
            }
        }

        public override bool SupportsMultipleOpenReaders {
            get {
                return false;
            }
        }
    }
}