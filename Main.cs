using System;
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
			ActiveRecordStarter.Initialize(config, 
			                               typeof(Country), 
			                               typeof(Religion),
			                               typeof(EthnicGroup),
			                               typeof(MaritalStatus),
			                               typeof(ScholarityLevel)
			                               );
			
			Application.Init ();
			MainWindow win = new MainWindow ();
			win.Show ();
			Application.Run ();
		}
	}
}

