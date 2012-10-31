using System.Collections.ObjectModel;
using Mono.Cecil;
using System.Collections.Generic;

namespace CecIL
{
	public class Reference : IName, IRender
	{
		#region ctor / dtor
		
		public Reference( AssemblyNameReference Reference )
		{
			_Attributes = Util.AttributeToString( Reference.Attributes );
			_HashAlgorithm = Util.AttributeToString( Reference.HashAlgorithm );
			_Culture = Reference.Culture;
			_FullName = Reference.FullName;
			_Name = Reference.Name;
			_PublicKey = Reference.HasPublicKey ? Util.HashToString( Reference.PublicKey ) : null;
			_PublicKeyToken = Util.HashToString( Reference.PublicKeyToken );
			_Version = Reference.Version.ToString( );
		}
		
		#endregion
	
		#region Properties

		#region Attributes

		private string _Attributes;

		public string Attributes
		{
			get
			{
				return( _Attributes );
			}
		}

		#endregion

		#region HashAlgorithm

		private string _HashAlgorithm;

		public string HashAlgorithm
		{
			get
			{
				return( _HashAlgorithm );
			}
		}

		#endregion

		#region Culture

		private string _Culture;

		public string Culture
		{
			get
			{
				return( _Culture );
			}
		}

		#endregion

		#region FullName

		private string _FullName;

		public string FullName
		{
			get
			{
				return( _FullName );
			}
		}

		#endregion

		#region PublicKey

		private string _PublicKey;

		public string PublicKey
		{
			get
			{
				return( _PublicKey );
			}
		}

		#endregion

		#region PublicKeyToken

		private string _PublicKeyToken;

		public string PublicKeyToken
		{
			get
			{
				return( _PublicKeyToken );
			}
		}

		#endregion

		#region Version

		private string _Version;

		public string Version
		{
			get
			{
				return( _Version );
			}
		}

		#endregion		

		#endregion

		#region IName

		private string _Name;

		string IName.Name
		{ 
			get
			{ 
				return( _Name );
			}
		}
		
		#endregion


		#region IRender
		
		string IRender.Render( )
		{
			return(string.Format ("<?xml version=\"1.0\" encoding=\"UTF-8\"?><document><paragraph><section style=\"comment\">//\t{0}</section></paragraph></document>", _FullName));
		}
		
		#endregion
	}
}
