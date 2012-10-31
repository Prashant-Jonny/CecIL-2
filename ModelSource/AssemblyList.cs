using System.Collections.Generic;

namespace CecIL
{
	public class AssemblyList : List<Assembly>
	{
		public Assembly Add(string Path)
		{
			Assembly oAssembly = Find(delegate(Assembly Assembly){ return(Assembly.Path == Path);});

			if(oAssembly==null){
				oAssembly = new Assembly(Path);

				Add(oAssembly);
			}

			return(oAssembly);
		}
	}
}
