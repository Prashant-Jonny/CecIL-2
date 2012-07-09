using System;
using Gtk;
using System.Xml;

namespace CecIL
{
	public static class TextBufferEx
	{
        public static void Load(this TextBuffer TextBuffer, string Xml, bool Clear = true)
        {
			if (Clear)
			{
				TextBuffer.Clear ( );
			}

			TextIter oIter = TextBuffer.	GetIterAtOffset (0);        
			XmlDocument oXML = new XmlDocument ( );
			
			oXML.LoadXml (Xml);
			
			XmlNode oParagraph = oXML.DocumentElement.SelectSingleNode ("paragraph");
			XmlNodeList oSectionList = oParagraph.SelectNodes ("section");
			
			foreach (XmlNode oSection in oSectionList)
			{
				string szStyle = oSection.Attributes.GetNamedItem ("style") != null ? oSection.Attributes.GetNamedItem ("style").Value : null;
				
					
				TextBuffer.InsertWithTagsByName (ref oIter, oSection.InnerText, szStyle);
			}
		}
	}
}

