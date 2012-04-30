using System;
using HumanRightsTracker.DataBase;
using HumanRightsTracker.Models;

namespace HumanRightsTracker
{
	class SchemaGenerator
	{

    public static void Main ()
		{
      HumanRightsTracker.DataBase.ConnectionHandler.Init ();
      Console.WriteLine("Opening database to generate *.hbm.xml files for humanrightstracker application...");
		}

	}
}
