using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Collections;
using System.Runtime.InteropServices;
using Gtk;
using HumanRightsTracker.DataBase;
using Reports;

namespace HumanRightsTracker
{

	class MainClass
	{

#if WIN32
        [DllImport("msvcrt.dll")]
        public static extern int _putenv (string varName);
#endif

        public static void Main (string[] args)
		{
            DataBase.ConnectionHandler.Init ();
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("es-ES");
            Environment.SetEnvironmentVariable ("LANGUAGE", "es");
#if WIN32
            _putenv ("LANG=es");
#endif
            Mono.Unix.Catalog.Init("i8n1", "locale");
			Application.Init ();
			LoginWindow win = new LoginWindow ();
			win.Show ();
			Application.Run ();
		}
	}
}