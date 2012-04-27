using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Collections;
using System.Runtime.InteropServices;
using Castle.ActiveRecord.Framework.Config;
using Castle.ActiveRecord.Framework.Scopes;
using Castle.ActiveRecord.Framework;
using Castle.ActiveRecord;
using HumanRightsTracker.Models;
using HumanRightsTracker.MonoSqlite;

namespace HumanRightsTracker.DataBase
{
    public class ConnectionHandler
    {
        public static void Init ()
        {

            XmlConfigurationSource config;

            string location = Assembly.GetExecutingAssembly().Location;

            Environment.CurrentDirectory = Path.GetDirectoryName (location);

            config = new XmlConfigurationSource("Config/ARConfig.xml");

            Assembly asm = Assembly.Load("Models");

            ActiveRecordStarter.SessionFactoryHolderCreated += ActiveRecordStarter_SessionFactoryHolderCreated;

            ActiveRecordStarter.Initialize(asm, config);
        }

        static void ActiveRecordStarter_SessionFactoryHolderCreated(Castle.ActiveRecord.Framework.ISessionFactoryHolder holder)
        {
            holder.ThreadScopeInfo = new DataBase.ThreadScopeInfo ();
        }
    }
}