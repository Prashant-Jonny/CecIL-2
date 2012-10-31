using Mono.Cecil;

namespace CecIL
{		
	public class AssemblyParser : IName
	{
		private IAssemblyResolver _AssemblyResolver = new DefaultAssemblyResolver( );

		#region ctor / dtor
		
		public AssemblyParser( string FileName )
		{
			_AssemblyDefinition = AssemblyDefinition.ReadAssembly(
				FileName,
				new ReaderParameters( ) { AssemblyResolver = _AssemblyResolver }
			);
			
			foreach( AssemblyNameReference oRef in _AssemblyDefinition.MainModule.AssemblyReferences )
			{
				References.Add( new Reference( oRef ) );
			}
		}
		
		#endregion
		
		#region Properties
		
		#region AssemblyDefinition

		private AssemblyDefinition _AssemblyDefinition = null;

		public AssemblyDefinition AssemblyDefinition
		{
			get
			{
				return( _AssemblyDefinition );
			}
		}
		
		#endregion

		#region References
		
		private ReferencesList _References = new ReferencesList( );

		public ReferencesList References
		{
			get
			{
				return( _References );
			}
		}
		
		#endregion
		
		#region Name
		
		public string Name
		{ 
			get
			{ 
				return( _AssemblyDefinition == null ? null : _AssemblyDefinition.MainModule.Name );
			}
		}
		
		#endregion
		
		#endregion
	}
}

