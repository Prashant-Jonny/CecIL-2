using Gtk;

namespace CecIL
{
	public interface INode
	{
		void AddChildren( TreeStore TreeStore, TreeIter Root );
	}
}

