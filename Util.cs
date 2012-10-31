using System;
using System.IO;
using System.Xml;
using Gtk;

namespace CecIL
{
	public class Util
	{
		public static string AttributeToString (object Value)
		{
			return(Enum.Format (Value.GetType ( ), Value, "F").Replace (", ", " | "));
		}
		
		public static string HashToString (byte[] Value)
		{
			return(BitConverter.ToString (Value).Replace ("-", string.Empty));
		}
	
		public static string LoadXMLResource (string FileName)
		{
			string szResult;
			Stream oStream = typeof (Util).Assembly.GetManifestResourceStream (string.Format ("CecIL.Embedded.{0}", FileName));
			StreamReader oStreamReader = new StreamReader (oStream);
			
			szResult = oStreamReader.ReadToEnd ( );
			
			return(szResult);
		}
	}
}
