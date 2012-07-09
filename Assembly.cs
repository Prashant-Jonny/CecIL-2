using Mono.Cecil;
using System.Text;

namespace CecIL
{		
	public class Assembly : IName, IRender
	{
		private IAssemblyResolver _Resolver = new DefaultAssemblyResolver( );
		private AssemblyDefinition _Definition = null;
		
		#region ctor / dtor
		
		public Assembly( string FileName )
		{
			_Definition = AssemblyDefinition.ReadAssembly( FileName, new ReaderParameters( ) { AssemblyResolver = _Resolver } );
		}
		
		#endregion
		
		#region Properties
		
		#region Name
		
		public string Name
		{ 
			get
			{ 
				return( _Definition.MainModule.Name );
			}
		}
		
		#endregion
		
		#endregion
		
		public void Render( StringBuilder StringBuilder )
		{
		}
	}
}

