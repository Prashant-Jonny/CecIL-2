using System;
using CecIL;
using Gtk;

public partial class MainWindow: Gtk.Window
{	
	private TreeStore _TreeStore = new TreeStore (typeof (object));
	private AssemblyList _AssemblyList = new AssemblyList();
	
	#region ctor / dtor
	
	public MainWindow ( ): base (Gtk.WindowType.Toplevel)
	{
		Build ( );
		
		InitTreeView ( );
		InitTextView ( );
	}
	
	#endregion
	
	#region Menu / Toolbar actions
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ( );
		
		a.RetVal = true;
	}

	protected void OnOpen (object sender, System.EventArgs e)
	{
		FileChooserDialog oFileDialog = new FileChooserDialog (
			"Select a .NET assembly to load...",
			this,
			FileChooserAction.Open,
			Stock.Cancel,
			ResponseType.Cancel,
			Stock.Open,
			ResponseType.Ok
		);
		FileFilter oAssemblyFilter = new FileFilter ( );
		FileFilter oAllFilter = new FileFilter ( );
		
		oAssemblyFilter.Name = ".NET Assembly";
		oAssemblyFilter.AddPattern ("*.dll");
		oAssemblyFilter.AddPattern ("*.exe");
		
		oAllFilter.Name = "All files";
		oAllFilter.AddPattern ("*.*");
		
		oFileDialog.AddFilter (oAssemblyFilter);
		oFileDialog.AddFilter (oAllFilter);
		
		if ((ResponseType)oFileDialog.Run ( ) == ResponseType.Ok)
		{
			LoadAssembly (oFileDialog.Filename);
		}
		
		oFileDialog.Destroy ( );
	}

	protected void OnQuit (object sender, System.EventArgs e)
	{
		Application.Quit ( );
	}
	
	#endregion
	
	#region Helpers
	
	private void LoadAssembly( string Path )
	{
		bool bExists = false;
		Assembly oAssembly = _AssemblyList.Add( Path );
		TreeIter oIter;

		if( _TreeStore.GetIterFirst( out oIter ) )
		{
			do
			{
				object oData = _TreeStore.GetValue( oIter, 0 );

				bExists = ( oData != null ) && ( oData is Assembly ) && ( ( ( Assembly )oData ).Path == Path );

				if( bExists )
				{
					break;
				}
			}
			while (_TreeStore.IterNext (ref oIter));
		}

		if( bExists )
		{
			TreeView.Selection.SelectIter(oIter);
		}
		else
		{
			TreeIter oRoot = _TreeStore.AppendValues( oAssembly );

			((INode)oAssembly).AddChildren(_TreeStore, oRoot);
		}
	}
	
	#endregion
	
	#region TreeView
	
	private void InitTreeView ( )
	{
		TreeViewColumn oCol = new TreeViewColumn ( );
		CellRendererText oCellRend = new CellRendererText ( );
 
		oCol.Title = "Assembly";
		oCol.PackStart (oCellRend, true);
		
		oCol.SetCellDataFunc (oCellRend, new Gtk.TreeCellDataFunc (RenderCell));
		
		TreeView.AppendColumn (oCol);
		
		TreeView.Model = _TreeStore;
		
		TreeView.Selection.Changed += HandleTreeViewSelectionChanged;
	}
	
	private void RenderCell( TreeViewColumn Column, CellRenderer Cell, TreeModel Model, TreeIter Iter )
	{
		object oData = Model.GetValue( Iter, 0 );
	
		if( oData is IName )
		{
			( ( CellRendererText )Cell ).Text = ((IName)oData).Name;
		}
	}

	private void HandleTreeViewSelectionChanged (object sender, EventArgs e)
	{
		TreeModel oModel;
		TreeIter oIter;
		
		if (TreeView.Selection.GetSelected (out oModel, out oIter))
		{
			TextView.Buffer.Clear();

			object oData = oModel.GetValue (oIter, 0);

			if(oData is IRender)
			{
				TextView.Buffer.Load(((IRender)oData).Render());
			}
		}
	}
	
	#endregion
	
	#region TextView
	
	private void InitTextView ( )
	{
		TextView.Buffer.TagTable.Load (Util.LoadXMLResource ("Styles.xml"));
	}

	#endregion
}
