using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Collections;
using Gtk;
using Castle.ActiveRecord.Framework.Config;
using Castle.ActiveRecord.Framework.Scopes;
using Castle.ActiveRecord.Framework;
using Castle.ActiveRecord;
using HumanRightsTracker.Models;
using Reports;

namespace HumanRightsTracker
{

	class MainClass
	{
		public static void Main (string[] args)
		{
            XmlConfigurationSource config;

            string location = Assembly.GetExecutingAssembly().Location;

            Environment.CurrentDirectory = Path.GetDirectoryName (location);

            config = new XmlConfigurationSource("Config/ARConfig.xml");

			Assembly asm = Assembly.Load("Models");
            ActiveRecordStarter.SessionFactoryHolderCreated += ActiveRecordStarter_SessionFactoryHolderCreated;
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("es-ES");
			ActiveRecordStarter.Initialize(asm, config);
            Environment.SetEnvironmentVariable ("LANGUAGE", "es");
            Mono.Unix.Catalog.Init("i8n1", "locale");
			Application.Init ();
			LoginWindow win = new LoginWindow ();
			win.Show ();
			Application.Run ();
		}

       static void ActiveRecordStarter_SessionFactoryHolderCreated(Castle.ActiveRecord.Framework.ISessionFactoryHolder holder)
       {
            holder.ThreadScopeInfo = new ThreadScopeInfo ();
       }
	}

    public class ThreadScopeInfo : IThreadScopeInfo
    {
        private readonly object _SyncLock;
        private readonly Stack _CurrentStack;

        public ThreadScopeInfo()
        {
            _CurrentStack = new Stack();
            _SyncLock = new object();
        }

        public Stack CurrentStack {get { return (_CurrentStack); }}

        public void RegisterScope(ISessionScope scope)
        {
            CurrentStack.Push(scope);
        }

        public ISessionScope GetRegisteredScope()
        {
            if (CurrentStack.Count == 0)
            {
                lock (_SyncLock)
                {
                    if (CurrentStack.Count == 0)
                    {
                        new SessionScope (FlushAction.Never);
                    }
                }
            }
            return CurrentStack.Peek() as ISessionScope;
        }

        public void UnRegisterScope(ISessionScope scope)
        {
            if (GetRegisteredScope() != scope)
            {
                throw new ScopeMachineryException("Tried to unregister a scope that is not the active one");
            }
            CurrentStack.Pop();
        }

        public bool HasInitializedScope
        {
            get { return GetRegisteredScope() != null; }
        }
    }
}