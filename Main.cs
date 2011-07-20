using System;
using System.Reflection;
using Gtk;
using Castle.ActiveRecord.Framework.Config;
using Castle.ActiveRecord;
using HumanRightsTracker.Models;

namespace HumanRightsTracker
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			XmlConfigurationSource config = new XmlConfigurationSource("Config/ARConfig.xml");
			Assembly asm = Assembly.Load("Models");
			ActiveRecordStarter.Initialize(asm, config);
			
			Application.Init ();
			LoginWindow win = new LoginWindow ();
			win.Show ();
			Application.Run ();
		}
	}
}

