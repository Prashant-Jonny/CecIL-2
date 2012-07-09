using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace CecIL
{
	public class PropertyView
	{
		public string Name
		{
			get;
			set;
		}
		
		public object Value
		{ 
			get;
			set;
		}
	}
	
	public class PropertyList
	{
		private Collection<PropertyView> _PropertyViewCollection = new Collection<PropertyView>( );
		
		public Collection<PropertyView> PropertyViewCollection
		{
			get
			{
				return( _PropertyViewCollection );
			}
		}
		
		public PropertyList( object Object )
		{
			Type oType = Object.GetType( );
			List<PropertyInfo> oProperties = new List<PropertyInfo>( oType.GetProperties( ) );

			foreach( PropertyInfo oProperty in oProperties )
			{
				object oValue = oProperty.GetValue( Object, null );

				PropertyViewCollection.Add( new PropertyView( ){Name=oProperty.Name,Value=oValue} );
			}
		}
	}
}

