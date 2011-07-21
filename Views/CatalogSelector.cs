using System;
using System.Reflection;
using HumanRightsTracker.Models;

namespace Views
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class CatalogSelector : Gtk.Bin
	{
		String model;
		
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
				Type t = asm.GetType("HumanRightsTracker.Models." + model);
        		MethodInfo method 
             		= t.GetMethod("FindAll", 
					              BindingFlags.FlattenHierarchy | BindingFlags.Static | BindingFlags.Public, 
					              null,
					              new Type[0],
					              null);
				MethodInfo nameMethod 
             		= t.GetMethod("get_Name");

        		Object[] collection = (Object[]) method.Invoke(null, null);
				foreach (Object o in collection)
				{
					String name = nameMethod.Invoke(o, null) as String;
					combo.AppendText(name);
				}
			}
		}		
		
	}
}

