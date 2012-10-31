using Mono.Cecil;
using System.Text;
using Gtk;

namespace CecIL
{		
	public class Assembly : INode, IName, IRender
	{
		private IAssemblyResolver _Resolver = new DefaultAssemblyResolver( );
		private AssemblyDefinition _Definition;
		
		#region ctor / dtor
		
		public Assembly( string Path )
		{
			_Path = Path;

			_Definition = AssemblyDefinition.ReadAssembly( _Path, new ReaderParameters( ) { AssemblyResolver = _Resolver } );

			LoadReferenceList();
		}
		
		#endregion
		
		#region Properties

		#region Path

		private string _Path;

		public string Path
		{
			get
			{
				return(_Path);
			}
		}

		#endregion

		#region ReferenceList
		
		private ReferencesList _ReferenceList = new ReferencesList( );
		
		public ReferencesList ReferenceList
		{
			get
			{
				return( _ReferenceList );
			}
		}
		
		#endregion

		#endregion

		#region Helpers

		private void LoadReferenceList()
		{
			foreach( AssemblyNameReference oRef in _Definition.MainModule.AssemblyReferences )
			{
				_ReferenceList.Add( new Reference( oRef ) );
			}
		}

		#endregion

		#region INode

		void INode.AddChildren( TreeStore TreeStore, TreeIter Root )
		{
			TreeIter oNewRoot = TreeStore.AppendValues (Root, _ReferenceList);

			((INode)_ReferenceList).AddChildren(TreeStore, oNewRoot);
		}

		#endregion

		#region IName
		
		string IName.Name
		{ 
			get
			{ 
				return( _Definition.MainModule.Name );
			}
		}

		#endregion

		#region IRender

		string IRender.Render( )
		{
			return(string.Format (
				"<?xml version=\"1.0\" encoding=\"UTF-8\"?><document>" +
					"<paragraph><section style=\"comment\">//\t{0}</section></paragraph>" +
					"<paragraph><section style=\"comment\">//\t{1}</section></paragraph>" +
					"<paragraph></paragraph>" +
					"<paragraph><section style=\"comment\">//\t</section><section style=\"comment.keyword\">Architecture:</section><section style=\"comment\"> {2}</section></paragraph>" +
					"<paragraph><section style=\"comment\">//\t</section><section style=\"comment.keyword\">Kind:</section><section style=\"comment\"> {3}</section></paragraph>" +
					"<paragraph><section style=\"comment\">//\t</section><section style=\"comment.keyword\">Runtime:</section><section style=\"comment\"> {4}</section></paragraph>" +
				"</document>", 
				_Definition.FullName,
				_Definition.MainModule.FullyQualifiedName,
				Util.AttributeToString(_Definition.MainModule.Architecture),
				Util.AttributeToString(_Definition.MainModule.Kind),
				Util.AttributeToString(_Definition.MainModule.Runtime)
			));
		}

		#endregion
	}
}

