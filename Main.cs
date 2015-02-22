using System;
using Gtk;

namespace CecIL
{
	class MainClass
	{
		public static void Main( string[] args )
		{
			Application.Init( );
			
			MainWindow oMainWindow = new MainWindow( );

			oMainWindow.Show( );

			Application.Run( );
		}
	}
}
