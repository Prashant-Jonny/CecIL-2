using System;
using System.Xml;
using Gtk;

namespace CecIL
{
	public static class TextTagTableEx
	{
        public static void Load(this TextTagTable TagTable, string Xml, bool Clear = true)
        {
			XmlDocument oXML = new XmlDocument( );
			
			oXML.LoadXml( Xml );
			
			XmlNode oStyleSet = oXML.DocumentElement.SelectSingleNode( "styleset" );
			XmlNodeList oStyleDefList = oStyleSet.SelectNodes( "styledef" );
			
			if( Clear )
			{
				TagTable.Data.Clear( );
			}
			
			foreach( XmlNode oStyleDef in oStyleDefList )
			{
				string szName = oStyleDef.Attributes.GetNamedItem( "name" ).Value;
				string szColor = oStyleDef.Attributes.GetNamedItem( "color" ).Value;
				string szStyle = oStyleDef.Attributes.GetNamedItem( "style" ) != null ? oStyleDef.Attributes.GetNamedItem( "style" ).Value : null;
				string szWeight = oStyleDef.Attributes.GetNamedItem( "weight" ) != null ? oStyleDef.Attributes.GetNamedItem( "weight" ).Value : null;
				
				TextTag oTextTag = new TextTag( szName );

				oTextTag.Foreground = szColor;
				
				if( !string.IsNullOrEmpty( szWeight ) )
				{
					oTextTag.Weight = ( Pango.Weight )Enum.Parse( typeof( Pango.Weight ), szWeight );
				}
				
				if( !string.IsNullOrEmpty( szStyle ) )
				{
					oTextTag.Style = ( Pango.Style )Enum.Parse( typeof( Pango.Style ), szStyle );
				}
				
				TagTable.Add( oTextTag );
			}
			
        }
	}
}

