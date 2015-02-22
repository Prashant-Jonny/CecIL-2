using System.Collections.Generic;
using Gtk;

namespace CecIL
{
	public class ReferencesList : List<Reference>, INode, IName
	{
		string IName.Name
		{
			get
			{
				return( "References" );
			}
		}

		#region INode
		
		void INode.AddChildren( TreeStore TreeStore, TreeIter Root )
		{
			foreach(Reference oRef in this)
			{
				TreeIter oNewRoot = TreeStore.AppendValues (Root, oRef);
			}
		}
		
		#endregion
	}
}

