using System;
using CecIL;
using Gtk;

public partial class MainWindow: Gtk.Window
{	
	private TreeStore _TreeViewStore = new TreeStore (typeof (object));
	
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
			LoadDll (oFileDialog.Filename);
		}
		
		oFileDialog.Destroy ( );
	}

	protected void OnQuit (object sender, System.EventArgs e)
	{
		Application.Quit ( );
	}
	
	#endregion
	
	#region Parser
	
	private void LoadDll (string Filename)
	{
		Util.Assembly2Tree (Util.LoadAssembly (Filename), _TreeViewStore);
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
		
		TreeView.Model = _TreeViewStore;
		
		TreeView.Selection.Changed += HandleTreeViewSelectionChanged;
	}
	
	private void RenderCell (TreeViewColumn Column, CellRenderer Cell, TreeModel Model, TreeIter Iter)
	{
		IName oData = (IName)Model.GetValue (Iter, 0);

	
		((CellRendererText)Cell).Text = oData.Name;
	}

	private void HandleTreeViewSelectionChanged (object sender, EventArgs e)
	{
		TreeModel oModel;
		TreeIter oIter;
		
		if (TreeView.Selection.GetSelected (out oModel, out oIter))
		{
			object oData = oModel.GetValue (oIter, 0);
			
			if (oData.GetType ( ) == typeof (AssemblyParser))
			{
				Util.XmlToTextBuffer (TextView.Buffer, Util.CreateAssemblyString ((AssemblyParser)oData));
			}
			
			if (oData.GetType ( ) == typeof (References))
			{
			}
			
			if (oData.GetType ( ) == typeof (Reference))
			{
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
