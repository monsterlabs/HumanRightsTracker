using System;
using System.Reflection;
using HumanRightsTracker.Models;
using NHibernate.Criterion;

namespace Views
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class CatalogSelector : Gtk.Bin
	{
		String model;
		Object[] collection;
		Type t;
		
		public event EventHandler Changed;
		
		public CatalogSelector ()
		{
			this.Build ();
		}
		
		public String Model {
        get {
        	return this.model;
        }
        set {
            model = value;
			Assembly asm = Assembly.Load("Models");
			t = asm.GetType("HumanRightsTracker.Models." + model);
       		MethodInfo method = t.GetMethod("FindAll", 
                                      BindingFlags.FlattenHierarchy | BindingFlags.Static | BindingFlags.Public,
                                      null,
                                      new Type[0],
                                      null);
        	Object[]  options = (Object[]) method.Invoke(null, null);
            DeleteAndSetOptions(options);
        }
}               

		protected virtual void onChanged (object sender, System.EventArgs e)
		{
			if (Changed != null)
				Changed(this, e);
		}
		
		public Object Active {
			get {
				return collection[combobox.Active];
			}
		}
		
		public void FilterBy(ICriterion[] criteria) 
		{
			Assembly asm = Assembly.Load("Models");
			t = asm.GetType("HumanRightsTracker.Models." + model);
			MethodInfo method = t.GetMethod("FindAll", 
                                      BindingFlags.FlattenHierarchy | BindingFlags.Static | BindingFlags.Public,
                                      null,
                                      new Type[] { typeof(ICriterion[]) },
                                      null);
			Object[] options = (Object[]) method.Invoke(null, new Object[] {criteria});
			DeleteAndSetOptions(options);
		}
		
		private void DeleteAndSetOptions(Object[] options) {
			collection = options;
			((Gtk.ListStore) combobox.Model).Clear();
			MethodInfo nameMethod = t.GetMethod("get_Name");
			foreach (Object o in collection)
            {
                String name = nameMethod.Invoke(o, null) as String;
                combobox.AppendText(name);
            }
		}
	}
}

