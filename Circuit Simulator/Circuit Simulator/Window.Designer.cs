namespace Circuit_Simulator
{
    partial class Window
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Window));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.menuStrip_File = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip_File_New = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip_File_Open = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip_File_Import = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip_File_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip_File_SaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip_Edit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip_Edit_Undo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip_Edit_Redo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip_Edit_Copy = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip_Edit_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip_Edit_Paste = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip_Edit_SelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip_View = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip_View_Reset = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip_View_ZoomIn = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip_View_ZoomOut = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip_Tools = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip_Tools_Reload = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip_Tools_Clear = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip_Tools_Rename = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip_Tools_Resize = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip_Tools_AddInput = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip_Tools_RemoveConn = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip_Tools_TrimInput = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip_Tools_Invert = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip_Tools_Merge = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip_Tools_Toggle = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenu_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenu_Type = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenu_Rename = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenu_Resize = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenu_AddInput = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenu_RemoveConn = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenu_Trim = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenu_Merge = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenu_Invert = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenu_Toggle = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenu_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenu_Copy = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenu_Paste = new System.Windows.Forms.ToolStripMenuItem();
            this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStrip_New = new System.Windows.Forms.ToolStripButton();
            this.separator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip_Open = new System.Windows.Forms.ToolStripButton();
            this.toolStrip_Import = new System.Windows.Forms.ToolStripButton();
            this.separator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip_Save = new System.Windows.Forms.ToolStripButton();
            this.toolStrip_SaveAs = new System.Windows.Forms.ToolStripButton();
            this.separator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip_Copy = new System.Windows.Forms.ToolStripButton();
            this.toolStrip_Paste = new System.Windows.Forms.ToolStripButton();
            this.toolStrip_Delete = new System.Windows.Forms.ToolStripButton();
            this.separator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip_Undo = new System.Windows.Forms.ToolStripButton();
            this.toolStrip_Redo = new System.Windows.Forms.ToolStripButton();
            this.separator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip_ZoomIn = new System.Windows.Forms.ToolStripButton();
            this.toolStrip_ZoomOut = new System.Windows.Forms.ToolStripButton();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.menuStrip.SuspendLayout();
            this.contextMenu.SuspendLayout();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.SystemColors.ControlLight;
            this.menuStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuStrip_File,
            this.menuStrip_Edit,
            this.menuStrip_View,
            this.menuStrip_Tools});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(984, 24);
            this.menuStrip.TabIndex = 0;
            // 
            // menuStrip_File
            // 
            this.menuStrip_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuStrip_File_New,
            this.menuStrip_File_Open,
            this.menuStrip_File_Import,
            this.menuStrip_File_Save,
            this.menuStrip_File_SaveAs});
            this.menuStrip_File.Name = "menuStrip_File";
            this.menuStrip_File.Size = new System.Drawing.Size(37, 20);
            this.menuStrip_File.Text = "File";
            // 
            // menuStrip_File_New
            // 
            this.menuStrip_File_New.Image = global::Circuit_Simulator.Properties.Resources.newfile;
            this.menuStrip_File_New.Name = "menuStrip_File_New";
            this.menuStrip_File_New.ShortcutKeyDisplayString = "Ctrl+Shift+N";
            this.menuStrip_File_New.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.N)));
            this.menuStrip_File_New.Size = new System.Drawing.Size(195, 22);
            this.menuStrip_File_New.Text = "New";
            this.menuStrip_File_New.Click += new System.EventHandler(this.menuStrip_File_New_Click);
            // 
            // menuStrip_File_Open
            // 
            this.menuStrip_File_Open.Image = global::Circuit_Simulator.Properties.Resources.open;
            this.menuStrip_File_Open.Name = "menuStrip_File_Open";
            this.menuStrip_File_Open.ShortcutKeyDisplayString = "Ctrl+O";
            this.menuStrip_File_Open.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.menuStrip_File_Open.Size = new System.Drawing.Size(195, 22);
            this.menuStrip_File_Open.Text = "Open";
            this.menuStrip_File_Open.Click += new System.EventHandler(this.menuStrip_File_Open_Click);
            // 
            // menuStrip_File_Import
            // 
            this.menuStrip_File_Import.Image = global::Circuit_Simulator.Properties.Resources.import;
            this.menuStrip_File_Import.Name = "menuStrip_File_Import";
            this.menuStrip_File_Import.ShortcutKeyDisplayString = "Ctrl+I";
            this.menuStrip_File_Import.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.menuStrip_File_Import.Size = new System.Drawing.Size(195, 22);
            this.menuStrip_File_Import.Text = "Import";
            this.menuStrip_File_Import.Click += new System.EventHandler(this.menuStrip_File_Import_Click);
            // 
            // menuStrip_File_Save
            // 
            this.menuStrip_File_Save.Image = global::Circuit_Simulator.Properties.Resources.save;
            this.menuStrip_File_Save.Name = "menuStrip_File_Save";
            this.menuStrip_File_Save.ShortcutKeyDisplayString = "Ctrl+S";
            this.menuStrip_File_Save.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.menuStrip_File_Save.Size = new System.Drawing.Size(195, 22);
            this.menuStrip_File_Save.Text = "Save";
            this.menuStrip_File_Save.Click += new System.EventHandler(this.menuStrip_File_Save_Click);
            // 
            // menuStrip_File_SaveAs
            // 
            this.menuStrip_File_SaveAs.Image = global::Circuit_Simulator.Properties.Resources.saveas;
            this.menuStrip_File_SaveAs.Name = "menuStrip_File_SaveAs";
            this.menuStrip_File_SaveAs.ShortcutKeyDisplayString = "Ctrl+Shift+S";
            this.menuStrip_File_SaveAs.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.menuStrip_File_SaveAs.Size = new System.Drawing.Size(195, 22);
            this.menuStrip_File_SaveAs.Text = "Save As...";
            this.menuStrip_File_SaveAs.Click += new System.EventHandler(this.menuStrip_File_SaveAs_Click);
            // 
            // menuStrip_Edit
            // 
            this.menuStrip_Edit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuStrip_Edit_Undo,
            this.menuStrip_Edit_Redo,
            this.menuStrip_Edit_Copy,
            this.menuStrip_Edit_Delete,
            this.menuStrip_Edit_Paste,
            this.menuStrip_Edit_SelectAll});
            this.menuStrip_Edit.Name = "menuStrip_Edit";
            this.menuStrip_Edit.Size = new System.Drawing.Size(39, 20);
            this.menuStrip_Edit.Text = "Edit";
            // 
            // menuStrip_Edit_Undo
            // 
            this.menuStrip_Edit_Undo.Image = global::Circuit_Simulator.Properties.Resources.undo;
            this.menuStrip_Edit_Undo.Name = "menuStrip_Edit_Undo";
            this.menuStrip_Edit_Undo.ShortcutKeyDisplayString = "Ctrl+Z";
            this.menuStrip_Edit_Undo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.menuStrip_Edit_Undo.Size = new System.Drawing.Size(174, 22);
            this.menuStrip_Edit_Undo.Text = "Undo";
            this.menuStrip_Edit_Undo.Click += new System.EventHandler(this.menuStrip_Edit_Undo_Click);
            // 
            // menuStrip_Edit_Redo
            // 
            this.menuStrip_Edit_Redo.Image = global::Circuit_Simulator.Properties.Resources.redo;
            this.menuStrip_Edit_Redo.Name = "menuStrip_Edit_Redo";
            this.menuStrip_Edit_Redo.ShortcutKeyDisplayString = "Ctrl+Shift+Z";
            this.menuStrip_Edit_Redo.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.Z)));
            this.menuStrip_Edit_Redo.Size = new System.Drawing.Size(174, 22);
            this.menuStrip_Edit_Redo.Text = "Redo";
            this.menuStrip_Edit_Redo.Click += new System.EventHandler(this.menuStrip_Edit_Redo_Click);
            // 
            // menuStrip_Edit_Copy
            // 
            this.menuStrip_Edit_Copy.Image = global::Circuit_Simulator.Properties.Resources.copy;
            this.menuStrip_Edit_Copy.Name = "menuStrip_Edit_Copy";
            this.menuStrip_Edit_Copy.ShortcutKeyDisplayString = "Ctrl+C";
            this.menuStrip_Edit_Copy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.menuStrip_Edit_Copy.Size = new System.Drawing.Size(174, 22);
            this.menuStrip_Edit_Copy.Text = "Copy";
            this.menuStrip_Edit_Copy.Click += new System.EventHandler(this.menuStrip_Edit_Copy_Click);
            // 
            // menuStrip_Edit_Delete
            // 
            this.menuStrip_Edit_Delete.Image = global::Circuit_Simulator.Properties.Resources.delete;
            this.menuStrip_Edit_Delete.Name = "menuStrip_Edit_Delete";
            this.menuStrip_Edit_Delete.ShortcutKeyDisplayString = "Del";
            this.menuStrip_Edit_Delete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.menuStrip_Edit_Delete.Size = new System.Drawing.Size(174, 22);
            this.menuStrip_Edit_Delete.Text = "Delete";
            this.menuStrip_Edit_Delete.Click += new System.EventHandler(this.menuStrip_Edit_Delete_Click);
            // 
            // menuStrip_Edit_Paste
            // 
            this.menuStrip_Edit_Paste.Image = global::Circuit_Simulator.Properties.Resources.paste;
            this.menuStrip_Edit_Paste.Name = "menuStrip_Edit_Paste";
            this.menuStrip_Edit_Paste.ShortcutKeyDisplayString = "Ctrl+V";
            this.menuStrip_Edit_Paste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.menuStrip_Edit_Paste.Size = new System.Drawing.Size(174, 22);
            this.menuStrip_Edit_Paste.Text = "Paste";
            this.menuStrip_Edit_Paste.Click += new System.EventHandler(this.menuStrip_Edit_Paste_Click);
            // 
            // menuStrip_Edit_SelectAll
            // 
            this.menuStrip_Edit_SelectAll.Name = "menuStrip_Edit_SelectAll";
            this.menuStrip_Edit_SelectAll.ShortcutKeyDisplayString = "Ctrl+A";
            this.menuStrip_Edit_SelectAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.menuStrip_Edit_SelectAll.Size = new System.Drawing.Size(174, 22);
            this.menuStrip_Edit_SelectAll.Text = "Select All";
            this.menuStrip_Edit_SelectAll.Click += new System.EventHandler(this.menuStrip_Edit_SelectAll_Click);
            // 
            // menuStrip_View
            // 
            this.menuStrip_View.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuStrip_View_Reset,
            this.menuStrip_View_ZoomIn,
            this.menuStrip_View_ZoomOut});
            this.menuStrip_View.Name = "menuStrip_View";
            this.menuStrip_View.Size = new System.Drawing.Size(44, 20);
            this.menuStrip_View.Text = "View";
            // 
            // menuStrip_View_Reset
            // 
            this.menuStrip_View_Reset.Name = "menuStrip_View_Reset";
            this.menuStrip_View_Reset.ShortcutKeyDisplayString = "Ctrl+Shift+0";
            this.menuStrip_View_Reset.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.D0)));
            this.menuStrip_View_Reset.Size = new System.Drawing.Size(202, 22);
            this.menuStrip_View_Reset.Text = "Reset View";
            this.menuStrip_View_Reset.Click += new System.EventHandler(this.menuStrip_View_Reset_Click);
            // 
            // menuStrip_View_ZoomIn
            // 
            this.menuStrip_View_ZoomIn.Image = global::Circuit_Simulator.Properties.Resources.zoomin;
            this.menuStrip_View_ZoomIn.Name = "menuStrip_View_ZoomIn";
            this.menuStrip_View_ZoomIn.ShortcutKeyDisplayString = "Ctrl++";
            this.menuStrip_View_ZoomIn.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Oemplus)));
            this.menuStrip_View_ZoomIn.Size = new System.Drawing.Size(202, 22);
            this.menuStrip_View_ZoomIn.Text = "Zoom In";
            this.menuStrip_View_ZoomIn.Click += new System.EventHandler(this.menuStrip_View_ZoomIn_Click);
            // 
            // menuStrip_View_ZoomOut
            // 
            this.menuStrip_View_ZoomOut.Image = global::Circuit_Simulator.Properties.Resources.zoomout;
            this.menuStrip_View_ZoomOut.Name = "menuStrip_View_ZoomOut";
            this.menuStrip_View_ZoomOut.ShortcutKeyDisplayString = "Ctrl+-";
            this.menuStrip_View_ZoomOut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.OemMinus)));
            this.menuStrip_View_ZoomOut.Size = new System.Drawing.Size(202, 22);
            this.menuStrip_View_ZoomOut.Text = "Zoom Out";
            this.menuStrip_View_ZoomOut.Click += new System.EventHandler(this.menuStrip_View_ZoomOut_Click);
            // 
            // menuStrip_Tools
            // 
            this.menuStrip_Tools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuStrip_Tools_Reload,
            this.menuStrip_Tools_Clear,
            this.menuStrip_Tools_Rename,
            this.menuStrip_Tools_Resize,
            this.menuStrip_Tools_AddInput,
            this.menuStrip_Tools_RemoveConn,
            this.menuStrip_Tools_TrimInput,
            this.menuStrip_Tools_Invert,
            this.menuStrip_Tools_Merge,
            this.menuStrip_Tools_Toggle});
            this.menuStrip_Tools.Name = "menuStrip_Tools";
            this.menuStrip_Tools.Size = new System.Drawing.Size(48, 20);
            this.menuStrip_Tools.Text = "Tools";
            // 
            // menuStrip_Tools_Reload
            // 
            this.menuStrip_Tools_Reload.Name = "menuStrip_Tools_Reload";
            this.menuStrip_Tools_Reload.Size = new System.Drawing.Size(206, 22);
            this.menuStrip_Tools_Reload.Text = "Reload";
            this.menuStrip_Tools_Reload.Click += new System.EventHandler(this.menuStrip_Tools_Reload_Click);
            // 
            // menuStrip_Tools_Clear
            // 
            this.menuStrip_Tools_Clear.Image = global::Circuit_Simulator.Properties.Resources.delete;
            this.menuStrip_Tools_Clear.Name = "menuStrip_Tools_Clear";
            this.menuStrip_Tools_Clear.Size = new System.Drawing.Size(206, 22);
            this.menuStrip_Tools_Clear.Text = "Clear All Gates";
            this.menuStrip_Tools_Clear.Click += new System.EventHandler(this.menuStrip_Tools_Clear_Click);
            // 
            // menuStrip_Tools_Rename
            // 
            this.menuStrip_Tools_Rename.Image = global::Circuit_Simulator.Properties.Resources.rename;
            this.menuStrip_Tools_Rename.Name = "menuStrip_Tools_Rename";
            this.menuStrip_Tools_Rename.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.menuStrip_Tools_Rename.Size = new System.Drawing.Size(206, 22);
            this.menuStrip_Tools_Rename.Text = "Rename";
            this.menuStrip_Tools_Rename.Click += new System.EventHandler(this.menuStrip_Tools_Rename_Click);
            // 
            // menuStrip_Tools_Resize
            // 
            this.menuStrip_Tools_Resize.Image = global::Circuit_Simulator.Properties.Resources.resize;
            this.menuStrip_Tools_Resize.Name = "menuStrip_Tools_Resize";
            this.menuStrip_Tools_Resize.Size = new System.Drawing.Size(206, 22);
            this.menuStrip_Tools_Resize.Text = "Resize";
            this.menuStrip_Tools_Resize.Click += new System.EventHandler(this.menuStrip_Tools_Resize_Click);
            // 
            // menuStrip_Tools_AddInput
            // 
            this.menuStrip_Tools_AddInput.Image = global::Circuit_Simulator.Properties.Resources.addinput;
            this.menuStrip_Tools_AddInput.Name = "menuStrip_Tools_AddInput";
            this.menuStrip_Tools_AddInput.ShortcutKeyDisplayString = "Ctrl+,";
            this.menuStrip_Tools_AddInput.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Oemcomma)));
            this.menuStrip_Tools_AddInput.Size = new System.Drawing.Size(206, 22);
            this.menuStrip_Tools_AddInput.Text = "Add Empty Input";
            this.menuStrip_Tools_AddInput.Click += new System.EventHandler(this.menuStrip_Tools_AddInput_Click);
            // 
            // menuStrip_Tools_RemoveConn
            // 
            this.menuStrip_Tools_RemoveConn.Image = global::Circuit_Simulator.Properties.Resources.removeinput;
            this.menuStrip_Tools_RemoveConn.Name = "menuStrip_Tools_RemoveConn";
            this.menuStrip_Tools_RemoveConn.Size = new System.Drawing.Size(206, 22);
            this.menuStrip_Tools_RemoveConn.Text = "Remove Connection";
            this.menuStrip_Tools_RemoveConn.Click += new System.EventHandler(this.menuStrip_Tools_RemoveConn_Click);
            // 
            // menuStrip_Tools_TrimInput
            // 
            this.menuStrip_Tools_TrimInput.Image = global::Circuit_Simulator.Properties.Resources.triminput;
            this.menuStrip_Tools_TrimInput.Name = "menuStrip_Tools_TrimInput";
            this.menuStrip_Tools_TrimInput.ShortcutKeyDisplayString = "";
            this.menuStrip_Tools_TrimInput.Size = new System.Drawing.Size(206, 22);
            this.menuStrip_Tools_TrimInput.Text = "Trim Empty Inputs";
            this.menuStrip_Tools_TrimInput.Click += new System.EventHandler(this.menuStrip_Tools_TrimInput_Click);
            // 
            // menuStrip_Tools_Invert
            // 
            this.menuStrip_Tools_Invert.Image = global::Circuit_Simulator.Properties.Resources.invert;
            this.menuStrip_Tools_Invert.Name = "menuStrip_Tools_Invert";
            this.menuStrip_Tools_Invert.ShortcutKeyDisplayString = "Ctrl+.";
            this.menuStrip_Tools_Invert.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.OemPeriod)));
            this.menuStrip_Tools_Invert.Size = new System.Drawing.Size(206, 22);
            this.menuStrip_Tools_Invert.Text = "Invert Connection";
            this.menuStrip_Tools_Invert.Click += new System.EventHandler(this.menuStrip_Tools_Invert_Click);
            // 
            // menuStrip_Tools_Merge
            // 
            this.menuStrip_Tools_Merge.Image = global::Circuit_Simulator.Properties.Resources.merge;
            this.menuStrip_Tools_Merge.Name = "menuStrip_Tools_Merge";
            this.menuStrip_Tools_Merge.Size = new System.Drawing.Size(206, 22);
            this.menuStrip_Tools_Merge.Text = "Merge Connections";
            this.menuStrip_Tools_Merge.Click += new System.EventHandler(this.menuStrip_Tools_Merge_Click);
            // 
            // menuStrip_Tools_Toggle
            // 
            this.menuStrip_Tools_Toggle.Image = global::Circuit_Simulator.Properties.Resources.toggle;
            this.menuStrip_Tools_Toggle.Name = "menuStrip_Tools_Toggle";
            this.menuStrip_Tools_Toggle.ShortcutKeyDisplayString = "Space";
            this.menuStrip_Tools_Toggle.Size = new System.Drawing.Size(206, 22);
            this.menuStrip_Tools_Toggle.Text = "Toggle Buttons";
            this.menuStrip_Tools_Toggle.Click += new System.EventHandler(this.menuStrip_Tools_Toggle_Click);
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextMenu_Add,
            this.contextMenu_Type,
            this.contextMenu_Delete,
            this.contextMenu_Copy,
            this.contextMenu_Paste,
            this.contextMenu_Rename,
            this.contextMenu_Resize,
            this.contextMenu_AddInput,
            this.contextMenu_RemoveConn,
            this.contextMenu_Trim,
            this.contextMenu_Merge,
            this.contextMenu_Invert,
            this.contextMenu_Toggle});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(183, 290);
            // 
            // contextMenu_Add
            // 
            this.contextMenu_Add.Image = global::Circuit_Simulator.Properties.Resources.addnew;
            this.contextMenu_Add.Name = "contextMenu_Add";
            this.contextMenu_Add.Size = new System.Drawing.Size(182, 22);
            this.contextMenu_Add.Text = "Add Component";
            // 
            // contextMenu_Type
            // 
            this.contextMenu_Type.Image = global::Circuit_Simulator.Properties.Resources.changetype;
            this.contextMenu_Type.Name = "contextMenu_Type";
            this.contextMenu_Type.Size = new System.Drawing.Size(182, 22);
            this.contextMenu_Type.Text = "Change Type";
            // 
            // contextMenu_Rename
            // 
            this.contextMenu_Rename.Image = global::Circuit_Simulator.Properties.Resources.rename;
            this.contextMenu_Rename.Name = "contextMenu_Rename";
            this.contextMenu_Rename.Size = new System.Drawing.Size(182, 22);
            this.contextMenu_Rename.Text = "Rename";
            this.contextMenu_Rename.Click += new System.EventHandler(this.menuStrip_Tools_Rename_Click);
            // 
            // contextMenu_Resize
            // 
            this.contextMenu_Resize.Image = global::Circuit_Simulator.Properties.Resources.resize;
            this.contextMenu_Resize.Name = "contextMenu_Resize";
            this.contextMenu_Resize.Size = new System.Drawing.Size(182, 22);
            this.contextMenu_Resize.Text = "Resize";
            this.contextMenu_Resize.Click += new System.EventHandler(this.menuStrip_Tools_Resize_Click);
            // 
            // contextMenu_AddInput
            // 
            this.contextMenu_AddInput.Image = global::Circuit_Simulator.Properties.Resources.addinput;
            this.contextMenu_AddInput.Name = "contextMenu_AddInput";
            this.contextMenu_AddInput.Size = new System.Drawing.Size(182, 22);
            this.contextMenu_AddInput.Text = "Add Empty Input";
            this.contextMenu_AddInput.Click += new System.EventHandler(this.menuStrip_Tools_AddInput_Click);
            // 
            // contextMenu_RemoveConn
            // 
            this.contextMenu_RemoveConn.Image = global::Circuit_Simulator.Properties.Resources.removeinput;
            this.contextMenu_RemoveConn.Name = "contextMenu_RemoveConn";
            this.contextMenu_RemoveConn.Size = new System.Drawing.Size(182, 22);
            this.contextMenu_RemoveConn.Text = "Remove Connection";
            this.contextMenu_RemoveConn.Click += new System.EventHandler(this.menuStrip_Tools_RemoveConn_Click);
            // 
            // contextMenu_Trim
            // 
            this.contextMenu_Trim.Image = global::Circuit_Simulator.Properties.Resources.triminput;
            this.contextMenu_Trim.Name = "contextMenu_Trim";
            this.contextMenu_Trim.Size = new System.Drawing.Size(182, 22);
            this.contextMenu_Trim.Text = "Trim Extra Inputs";
            this.contextMenu_Trim.Click += new System.EventHandler(this.menuStrip_Tools_TrimInput_Click);
            // 
            // contextMenu_Merge
            // 
            this.contextMenu_Merge.Image = global::Circuit_Simulator.Properties.Resources.merge;
            this.contextMenu_Merge.Name = "contextMenu_Merge";
            this.contextMenu_Merge.Size = new System.Drawing.Size(182, 22);
            this.contextMenu_Merge.Text = "Merge Connections";
            this.contextMenu_Merge.Click += new System.EventHandler(this.menuStrip_Tools_Merge_Click);
            // 
            // contextMenu_Invert
            // 
            this.contextMenu_Invert.Image = global::Circuit_Simulator.Properties.Resources.invert;
            this.contextMenu_Invert.Name = "contextMenu_Invert";
            this.contextMenu_Invert.Size = new System.Drawing.Size(182, 22);
            this.contextMenu_Invert.Text = "Invert Connection";
            this.contextMenu_Invert.Click += new System.EventHandler(this.menuStrip_Tools_Invert_Click);
            // 
            // contextMenu_Toggle
            // 
            this.contextMenu_Toggle.Image = global::Circuit_Simulator.Properties.Resources.toggle;
            this.contextMenu_Toggle.Name = "contextMenu_Toggle";
            this.contextMenu_Toggle.Size = new System.Drawing.Size(182, 22);
            this.contextMenu_Toggle.Text = "Toggle Buttons";
            this.contextMenu_Toggle.Click += new System.EventHandler(this.menuStrip_Tools_Toggle_Click);
            // 
            // contextMenu_Delete
            // 
            this.contextMenu_Delete.Image = global::Circuit_Simulator.Properties.Resources.delete;
            this.contextMenu_Delete.Name = "contextMenu_Delete";
            this.contextMenu_Delete.Size = new System.Drawing.Size(182, 22);
            this.contextMenu_Delete.Text = "Delete";
            this.contextMenu_Delete.Click += new System.EventHandler(this.menuStrip_Edit_Delete_Click);
            // 
            // contextMenu_Copy
            // 
            this.contextMenu_Copy.Image = global::Circuit_Simulator.Properties.Resources.copy;
            this.contextMenu_Copy.Name = "contextMenu_Copy";
            this.contextMenu_Copy.Size = new System.Drawing.Size(182, 22);
            this.contextMenu_Copy.Text = "Copy";
            this.contextMenu_Copy.Click += new System.EventHandler(this.menuStrip_Edit_Copy_Click);
            // 
            // contextMenu_Paste
            // 
            this.contextMenu_Paste.Image = global::Circuit_Simulator.Properties.Resources.paste;
            this.contextMenu_Paste.Name = "contextMenu_Paste";
            this.contextMenu_Paste.Size = new System.Drawing.Size(182, 22);
            this.contextMenu_Paste.Text = "Paste";
            this.contextMenu_Paste.Click += new System.EventHandler(this.menuStrip_Edit_Paste_Click);
            // 
            // BottomToolStripPanel
            // 
            this.BottomToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.BottomToolStripPanel.Name = "BottomToolStripPanel";
            this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.BottomToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // TopToolStripPanel
            // 
            this.TopToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.TopToolStripPanel.Name = "TopToolStripPanel";
            this.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.TopToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // RightToolStripPanel
            // 
            this.RightToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.RightToolStripPanel.Name = "RightToolStripPanel";
            this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.RightToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // LeftToolStripPanel
            // 
            this.LeftToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftToolStripPanel.Name = "LeftToolStripPanel";
            this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.LeftToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // ContentPanel
            // 
            this.ContentPanel.Size = new System.Drawing.Size(960, 613);
            // 
            // toolStrip
            // 
            this.toolStrip.BackColor = System.Drawing.SystemColors.ControlLight;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStrip_New,
            this.separator1,
            this.toolStrip_Open,
            this.toolStrip_Import,
            this.separator2,
            this.toolStrip_Save,
            this.toolStrip_SaveAs,
            this.separator3,
            this.toolStrip_Copy,
            this.toolStrip_Paste,
            this.toolStrip_Delete,
            this.separator6,
            this.toolStrip_Undo,
            this.toolStrip_Redo,
            this.separator7,
            this.toolStrip_ZoomIn,
            this.toolStrip_ZoomOut});
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(984, 25);
            this.toolStrip.TabIndex = 2;
            // 
            // toolStrip_New
            // 
            this.toolStrip_New.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStrip_New.Image = global::Circuit_Simulator.Properties.Resources.newfile;
            this.toolStrip_New.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStrip_New.Name = "toolStrip_New";
            this.toolStrip_New.Size = new System.Drawing.Size(23, 22);
            this.toolStrip_New.Text = "New File";
            this.toolStrip_New.Click += new System.EventHandler(this.menuStrip_File_New_Click);
            // 
            // separator1
            // 
            this.separator1.Name = "separator1";
            this.separator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStrip_Open
            // 
            this.toolStrip_Open.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStrip_Open.Image = global::Circuit_Simulator.Properties.Resources.open;
            this.toolStrip_Open.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStrip_Open.Name = "toolStrip_Open";
            this.toolStrip_Open.Size = new System.Drawing.Size(23, 22);
            this.toolStrip_Open.Text = "Open File";
            this.toolStrip_Open.Click += new System.EventHandler(this.menuStrip_File_Open_Click);
            // 
            // toolStrip_Import
            // 
            this.toolStrip_Import.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStrip_Import.Image = global::Circuit_Simulator.Properties.Resources.import;
            this.toolStrip_Import.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStrip_Import.Name = "toolStrip_Import";
            this.toolStrip_Import.Size = new System.Drawing.Size(23, 22);
            this.toolStrip_Import.Text = "Import File";
            this.toolStrip_Import.Click += new System.EventHandler(this.menuStrip_File_Import_Click);
            // 
            // separator2
            // 
            this.separator2.Name = "separator2";
            this.separator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStrip_Save
            // 
            this.toolStrip_Save.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStrip_Save.Image = global::Circuit_Simulator.Properties.Resources.save;
            this.toolStrip_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStrip_Save.Name = "toolStrip_Save";
            this.toolStrip_Save.Size = new System.Drawing.Size(23, 22);
            this.toolStrip_Save.Text = "Save";
            this.toolStrip_Save.Click += new System.EventHandler(this.menuStrip_File_Save_Click);
            // 
            // toolStrip_SaveAs
            // 
            this.toolStrip_SaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStrip_SaveAs.Image = global::Circuit_Simulator.Properties.Resources.saveas;
            this.toolStrip_SaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStrip_SaveAs.Name = "toolStrip_SaveAs";
            this.toolStrip_SaveAs.Size = new System.Drawing.Size(23, 22);
            this.toolStrip_SaveAs.Text = "Save As";
            this.toolStrip_SaveAs.Click += new System.EventHandler(this.menuStrip_File_SaveAs_Click);
            // 
            // separator3
            // 
            this.separator3.Name = "separator3";
            this.separator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStrip_Copy
            // 
            this.toolStrip_Copy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStrip_Copy.Image = global::Circuit_Simulator.Properties.Resources.copy;
            this.toolStrip_Copy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStrip_Copy.Name = "toolStrip_Copy";
            this.toolStrip_Copy.Size = new System.Drawing.Size(23, 22);
            this.toolStrip_Copy.Text = "Copy";
            this.toolStrip_Copy.Click += new System.EventHandler(this.menuStrip_Edit_Copy_Click);
            // 
            // toolStrip_Paste
            // 
            this.toolStrip_Paste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStrip_Paste.Image = global::Circuit_Simulator.Properties.Resources.paste;
            this.toolStrip_Paste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStrip_Paste.Name = "toolStrip_Paste";
            this.toolStrip_Paste.Size = new System.Drawing.Size(23, 22);
            this.toolStrip_Paste.Text = "Paste";
            this.toolStrip_Paste.Click += new System.EventHandler(this.menuStrip_Edit_Paste_Click);
            // 
            // toolStrip_Delete
            // 
            this.toolStrip_Delete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStrip_Delete.Image = global::Circuit_Simulator.Properties.Resources.delete;
            this.toolStrip_Delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStrip_Delete.Name = "toolStrip_Delete";
            this.toolStrip_Delete.Size = new System.Drawing.Size(23, 22);
            this.toolStrip_Delete.Text = "Delete";
            this.toolStrip_Delete.Click += new System.EventHandler(this.menuStrip_Edit_Delete_Click);
            // 
            // separator6
            // 
            this.separator6.Name = "separator6";
            this.separator6.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStrip_Undo
            // 
            this.toolStrip_Undo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStrip_Undo.Image = global::Circuit_Simulator.Properties.Resources.undo;
            this.toolStrip_Undo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStrip_Undo.Name = "toolStrip_Undo";
            this.toolStrip_Undo.Size = new System.Drawing.Size(23, 22);
            this.toolStrip_Undo.Text = "Undo";
            this.toolStrip_Undo.Click += new System.EventHandler(this.menuStrip_Edit_Undo_Click);
            // 
            // toolStrip_Redo
            // 
            this.toolStrip_Redo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStrip_Redo.Image = global::Circuit_Simulator.Properties.Resources.redo;
            this.toolStrip_Redo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStrip_Redo.Name = "toolStrip_Redo";
            this.toolStrip_Redo.Size = new System.Drawing.Size(23, 22);
            this.toolStrip_Redo.Text = "Redo";
            this.toolStrip_Redo.Click += new System.EventHandler(this.menuStrip_Edit_Redo_Click);
            // 
            // separator7
            // 
            this.separator7.Name = "separator7";
            this.separator7.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStrip_ZoomIn
            // 
            this.toolStrip_ZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStrip_ZoomIn.Image = global::Circuit_Simulator.Properties.Resources.zoomin;
            this.toolStrip_ZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStrip_ZoomIn.Name = "toolStrip_ZoomIn";
            this.toolStrip_ZoomIn.Size = new System.Drawing.Size(23, 22);
            this.toolStrip_ZoomIn.Text = "Zoom +";
            this.toolStrip_ZoomIn.Click += new System.EventHandler(this.menuStrip_View_ZoomIn_Click);
            // 
            // toolStrip_ZoomOut
            // 
            this.toolStrip_ZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStrip_ZoomOut.Image = global::Circuit_Simulator.Properties.Resources.zoomout;
            this.toolStrip_ZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStrip_ZoomOut.Name = "toolStrip_ZoomOut";
            this.toolStrip_ZoomOut.Size = new System.Drawing.Size(23, 22);
            this.toolStrip_ZoomOut.Text = "Zoom -";
            this.toolStrip_ZoomOut.Click += new System.EventHandler(this.menuStrip_View_ZoomOut_Click);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "Logic Circuit |*.circ";
            this.saveFileDialog.OverwritePrompt = false;
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Logic Circuit |*.circ";
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 10;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // pictureBox
            // 
            this.pictureBox.AllowDrop = true;
            this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox.ContextMenuStrip = this.contextMenu;
            this.pictureBox.Location = new System.Drawing.Point(0, 52);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(984, 629);
            this.pictureBox.TabIndex = 3;
            this.pictureBox.TabStop = false;
            this.pictureBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pictureBox_KeyDown);
            this.pictureBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.pictureBox_DragDrop);
            this.pictureBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.pictureBox_DragEnter);
            this.pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_Paint);
            this.pictureBox.DoubleClick += new System.EventHandler(this.pictureBox_DoubleClick);
            this.pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            this.pictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
            this.pictureBox.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseWheel);
            // 
            // Window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(984, 681);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "Window";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Window_FormClosing);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.contextMenu.ResumeLayout(false);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuStrip_File;
        private System.Windows.Forms.ToolStripMenuItem menuStrip_File_Save;
        private System.Windows.Forms.ToolStripMenuItem menuStrip_View;
        private System.Windows.Forms.ToolStripMenuItem menuStrip_Tools;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem menuStrip_File_Open;
        private System.Windows.Forms.ToolStripMenuItem menuStrip_View_Reset;
        private System.Windows.Forms.ToolStripMenuItem contextMenu_Add;
        private System.Windows.Forms.ToolStripMenuItem menuStrip_File_New;
        private System.Windows.Forms.ToolStripMenuItem menuStrip_File_Import;
        private System.Windows.Forms.ToolStripMenuItem menuStrip_Edit;
        private System.Windows.Forms.ToolStripMenuItem menuStrip_Edit_Undo;
        private System.Windows.Forms.ToolStripMenuItem menuStrip_Edit_Redo;
        private System.Windows.Forms.ToolStripMenuItem menuStrip_Edit_Copy;
        private System.Windows.Forms.ToolStripMenuItem menuStrip_Edit_Delete;
        private System.Windows.Forms.ToolStripMenuItem menuStrip_Edit_Paste;
        private System.Windows.Forms.ToolStripMenuItem menuStrip_Edit_SelectAll;
        private System.Windows.Forms.ToolStripMenuItem menuStrip_View_ZoomIn;
        private System.Windows.Forms.ToolStripMenuItem menuStrip_View_ZoomOut;
        private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
        private System.Windows.Forms.ToolStripPanel TopToolStripPanel;
        private System.Windows.Forms.ToolStripPanel RightToolStripPanel;
        private System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
        private System.Windows.Forms.ToolStripContentPanel ContentPanel;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton toolStrip_New;
        private System.Windows.Forms.ToolStripButton toolStrip_Save;
        private System.Windows.Forms.ToolStripButton toolStrip_Open;
        private System.Windows.Forms.ToolStripButton toolStrip_Import;
        private System.Windows.Forms.ToolStripButton toolStrip_Copy;
        private System.Windows.Forms.ToolStripButton toolStrip_Paste;
        private System.Windows.Forms.ToolStripButton toolStrip_Delete;
        private System.Windows.Forms.ToolStripButton toolStrip_Undo;
        private System.Windows.Forms.ToolStripButton toolStrip_Redo;
        private System.Windows.Forms.ToolStripButton toolStrip_ZoomIn;
        private System.Windows.Forms.ToolStripButton toolStrip_ZoomOut;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.ToolStripSeparator separator1;
        private System.Windows.Forms.ToolStripSeparator separator2;
        private System.Windows.Forms.ToolStripSeparator separator3;
        private System.Windows.Forms.ToolStripSeparator separator6;
        private System.Windows.Forms.ToolStripSeparator separator7;
        private System.Windows.Forms.ToolStripMenuItem contextMenu_Rename;
        private System.Windows.Forms.ToolStripMenuItem contextMenu_Type;
        private System.Windows.Forms.ToolStripMenuItem contextMenu_Copy;
        private System.Windows.Forms.ToolStripMenuItem contextMenu_Paste;
        private System.Windows.Forms.ToolStripMenuItem contextMenu_Delete;
        private System.Windows.Forms.ToolStripMenuItem menuStrip_Tools_Clear;
        private System.Windows.Forms.ToolStripMenuItem contextMenu_AddInput;
        private System.Windows.Forms.ToolStripMenuItem contextMenu_Invert;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolStripButton toolStrip_SaveAs;
        private System.Windows.Forms.ToolStripMenuItem menuStrip_File_SaveAs;
        private System.Windows.Forms.ToolStripMenuItem contextMenu_Toggle;
        private System.Windows.Forms.ToolStripMenuItem contextMenu_Trim;
        private System.Windows.Forms.ToolStripMenuItem menuStrip_Tools_Invert;
        private System.Windows.Forms.ToolStripMenuItem menuStrip_Tools_Reload;
        private System.Windows.Forms.ToolStripMenuItem contextMenu_Merge;
        private System.Windows.Forms.ToolStripMenuItem menuStrip_Tools_Rename;
        private System.Windows.Forms.ToolStripMenuItem menuStrip_Tools_Resize;
        private System.Windows.Forms.ToolStripMenuItem contextMenu_Resize;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ToolStripMenuItem menuStrip_Tools_AddInput;
        private System.Windows.Forms.ToolStripMenuItem menuStrip_Tools_TrimInput;
        private System.Windows.Forms.ToolStripMenuItem menuStrip_Tools_Merge;
        private System.Windows.Forms.ToolStripMenuItem menuStrip_Tools_Toggle;
        private System.Windows.Forms.ToolStripMenuItem menuStrip_Tools_RemoveConn;
        private System.Windows.Forms.ToolStripMenuItem contextMenu_RemoveConn;
    }
}

