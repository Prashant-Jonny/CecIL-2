using System.Collections.ObjectModel;
using Mono.Cecil;

namespace CecIL
{
	public class Reference : IName
	{
		private AssemblyNameReference _Reference = null;
		
		#region MyRegionctor / dtor
		
		public Reference( AssemblyNameReference Reference )
		{
			_Reference = Reference;
		}
		
		#endregion
	
		#region Properties
		
		public string Attributes
		{
			get
			{
				return( Util.AttributeToString( _Reference.Attributes ) );
			}
		}
		
		public string HashAlgorithm
		{
			get
			{
				return( Util.AttributeToString( _Reference.HashAlgorithm ) );
			}
		}
		
		public string Culture
		{
			get
			{
				return( _Reference.Culture );
			}
		}

		public string FullName
		{
			get
			{
				return( _Reference.FullName );
			}
		}
		
		public string Name
		{
			get
			{
				return( _Reference.Name );
			}
		}
		
		public string PublicKey
		{
			get
			{
				return( _Reference.HasPublicKey ? Util.HashToString( _Reference.PublicKey ) : null );
			}
		}
		
		public string PublicKeyToken
		{
			get
			{
				return( Util.HashToString( _Reference.PublicKeyToken ) );
			}
		}
		
		public string Version
		{
			get
			{
				return( _Reference.Version.ToString( ) );
			}
		}
		
		#endregion
	}

	public class References : Collection<Reference>, IName
	{
		public string Name
		{
			get
			{
				return( "References" );
			}
		}
	}
}

