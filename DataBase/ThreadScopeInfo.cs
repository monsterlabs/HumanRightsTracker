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

namespace HumanRightsTracker.DataBase
{
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