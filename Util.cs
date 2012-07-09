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
	
		public static AssemblyParser LoadAssembly (string Location)
		{
			return(new AssemblyParser (Location));
		}

		public static void Assembly2Tree (AssemblyParser AssemblyParser, TreeStore TreeStore)
		{
			TreeIter oRoot = TreeStore.AppendValues (AssemblyParser);
			TreeIter oRefRoot = TreeStore.AppendValues (oRoot, AssemblyParser.References);
			
			foreach (Reference oRef in AssemblyParser.References)
			{
				TreeStore.AppendValues (oRefRoot, oRef);
			}
		}
		
		public static string LoadXMLResource (string FileName)
		{
			string szResult;
			Stream oStream = typeof (Util).Assembly.GetManifestResourceStream (string.Format ("CecIL.Embedded.{0}", FileName));
			StreamReader oStreamReader = new StreamReader (oStream);
			
			szResult = oStreamReader.ReadToEnd ( );
			
			return(szResult);
		}

		public static void XmlToTextBuffer (TextBuffer TextBuffer, string XML, bool Clear = true)
		{
			TextIter oIter = TextBuffer.GetIterAtOffset (0);        
			XmlDocument oXML = new XmlDocument ( );
			
			oXML.LoadXml (XML);
			
			XmlNode oParagraph = oXML.DocumentElement.SelectSingleNode ("paragraph");
			XmlNodeList oSectionList = oParagraph.SelectNodes ("section");
			
			if (Clear)
			{
				TextBuffer.Clear ( );
			}
			
			foreach (XmlNode oSection in oSectionList)
			{
				string szStyle = oSection.Attributes.GetNamedItem ("style") != null ? oSection.Attributes.GetNamedItem ("style").Value : null;
				
					
				TextBuffer.InsertWithTagsByName (ref oIter, oSection.InnerText, szStyle);
			}
		}
		
		public static string CreateAssemblyString (AssemblyParser AssemblyParser)
		{
			string szXML = string.Format ("<?xml version=\"1.0\" encoding=\"UTF-8\"?><document><paragraph><section style=\"comment\">//\t{0}</section></paragraph></document>", AssemblyParser.AssemblyDefinition.FullName);

			return(szXML);
		}
	}
}
