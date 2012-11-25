namespace AYarkov.LinksSaver
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.splitter = new System.Windows.Forms.SplitContainer();
            this.CloseAll = new System.Windows.Forms.Button();
            this.ExpandAll = new System.Windows.Forms.Button();
            this.treeView = new System.Windows.Forms.TreeView();
            this.BrowserGroup = new System.Windows.Forms.GroupBox();
            this.Browse = new System.Windows.Forms.Button();
            this.lblBrowserHint = new System.Windows.Forms.Label();
            this.txbBrowser = new System.Windows.Forms.TextBox();
            this.lblLink = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CreateLink = new System.Windows.Forms.RadioButton();
            this.CreateTheme = new System.Windows.Forms.RadioButton();
            this.txbURL = new System.Windows.Forms.TextBox();
            this.lblCrURL = new System.Windows.Forms.Label();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.lblCrName = new System.Windows.Forms.Label();
            this.txbName = new System.Windows.Forms.TextBox();
            this.EditForm = new System.Windows.Forms.GroupBox();
            this.btnChangeURL = new System.Windows.Forms.Button();
            this.btnChangeName = new System.Windows.Forms.Button();
            this.lblEditURL = new System.Windows.Forms.Label();
            this.lblEditName = new System.Windows.Forms.Label();
            this.txbEditURL = new System.Windows.Forms.TextBox();
            this.txbEditName = new System.Windows.Forms.TextBox();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.splitter.Panel1.SuspendLayout();
            this.splitter.Panel2.SuspendLayout();
            this.splitter.SuspendLayout();
            this.BrowserGroup.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.EditForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitter
            // 
            resources.ApplyResources(this.splitter, "splitter");
            this.splitter.Name = "splitter";
            // 
            // splitter.Panel1
            // 
            resources.ApplyResources(this.splitter.Panel1, "splitter.Panel1");
            this.splitter.Panel1.Controls.Add(this.CloseAll);
            this.splitter.Panel1.Controls.Add(this.ExpandAll);
            this.splitter.Panel1.Controls.Add(this.treeView);
            this.toolTip.SetToolTip(this.splitter.Panel1, resources.GetString("splitter.Panel1.ToolTip"));
            // 
            // splitter.Panel2
            // 
            resources.ApplyResources(this.splitter.Panel2, "splitter.Panel2");
            this.splitter.Panel2.Controls.Add(this.BrowserGroup);
            this.splitter.Panel2.Controls.Add(this.lblLink);
            this.splitter.Panel2.Controls.Add(this.groupBox1);
            this.splitter.Panel2.Controls.Add(this.EditForm);
            this.toolTip.SetToolTip(this.splitter.Panel2, resources.GetString("splitter.Panel2.ToolTip"));
            this.toolTip.SetToolTip(this.splitter, resources.GetString("splitter.ToolTip"));
            // 
            // CloseAll
            // 
            resources.ApplyResources(this.CloseAll, "CloseAll");
            this.CloseAll.Name = "CloseAll";
            this.toolTip.SetToolTip(this.CloseAll, resources.GetString("CloseAll.ToolTip"));
            this.CloseAll.UseVisualStyleBackColor = true;
            this.CloseAll.Click += new System.EventHandler(this.CloseAll_Click);
            // 
            // ExpandAll
            // 
            resources.ApplyResources(this.ExpandAll, "ExpandAll");
            this.ExpandAll.Name = "ExpandAll";
            this.toolTip.SetToolTip(this.ExpandAll, resources.GetString("ExpandAll.ToolTip"));
            this.ExpandAll.UseVisualStyleBackColor = true;
            this.ExpandAll.Click += new System.EventHandler(this.ExpandAll_Click);
            // 
            // treeView
            // 
            resources.ApplyResources(this.treeView, "treeView");
            this.treeView.AllowDrop = true;
            this.treeView.FullRowSelect = true;
            this.treeView.HideSelection = false;
            this.treeView.Name = "treeView";
            this.treeView.ShowNodeToolTips = true;
            this.toolTip.SetToolTip(this.treeView, resources.GetString("treeView.ToolTip"));
            this.treeView.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeView_AfterLabelEdit);
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            this.treeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseDoubleClick);
            this.treeView.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeView_DragDrop);
            this.treeView.DragOver += new System.Windows.Forms.DragEventHandler(this.treeView_DragOver);
            this.treeView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView_MouseDown);
            // 
            // BrowserGroup
            // 
            resources.ApplyResources(this.BrowserGroup, "BrowserGroup");
            this.BrowserGroup.Controls.Add(this.Browse);
            this.BrowserGroup.Controls.Add(this.lblBrowserHint);
            this.BrowserGroup.Controls.Add(this.txbBrowser);
            this.BrowserGroup.Name = "BrowserGroup";
            this.BrowserGroup.TabStop = false;
            this.toolTip.SetToolTip(this.BrowserGroup, resources.GetString("BrowserGroup.ToolTip"));
            // 
            // Browse
            // 
            resources.ApplyResources(this.Browse, "Browse");
            this.Browse.Name = "Browse";
            this.toolTip.SetToolTip(this.Browse, resources.GetString("Browse.ToolTip"));
            this.Browse.UseVisualStyleBackColor = true;
            this.Browse.Click += new System.EventHandler(this.browse_Click);
            // 
            // lblBrowserHint
            // 
            resources.ApplyResources(this.lblBrowserHint, "lblBrowserHint");
            this.lblBrowserHint.Name = "lblBrowserHint";
            this.toolTip.SetToolTip(this.lblBrowserHint, resources.GetString("lblBrowserHint.ToolTip"));
            // 
            // txbBrowser
            // 
            resources.ApplyResources(this.txbBrowser, "txbBrowser");
            this.txbBrowser.Name = "txbBrowser";
            this.toolTip.SetToolTip(this.txbBrowser, resources.GetString("txbBrowser.ToolTip"));
            this.txbBrowser.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txbBrowser_KeyDown);
            // 
            // lblLink
            // 
            resources.ApplyResources(this.lblLink, "lblLink");
            this.lblLink.Name = "lblLink";
            this.toolTip.SetToolTip(this.lblLink, resources.GetString("lblLink.ToolTip"));
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.CreateLink);
            this.groupBox1.Controls.Add(this.CreateTheme);
            this.groupBox1.Controls.Add(this.txbURL);
            this.groupBox1.Controls.Add(this.lblCrURL);
            this.groupBox1.Controls.Add(this.buttonAdd);
            this.groupBox1.Controls.Add(this.lblCrName);
            this.groupBox1.Controls.Add(this.txbName);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            this.toolTip.SetToolTip(this.groupBox1, resources.GetString("groupBox1.ToolTip"));
            // 
            // CreateLink
            // 
            resources.ApplyResources(this.CreateLink, "CreateLink");
            this.CreateLink.Name = "CreateLink";
            this.CreateLink.TabStop = true;
            this.toolTip.SetToolTip(this.CreateLink, resources.GetString("CreateLink.ToolTip"));
            this.CreateLink.UseVisualStyleBackColor = true;
            this.CreateLink.CheckedChanged += new System.EventHandler(this.CreateLink_CheckedChanged);
            // 
            // CreateTheme
            // 
            resources.ApplyResources(this.CreateTheme, "CreateTheme");
            this.CreateTheme.Name = "CreateTheme";
            this.CreateTheme.TabStop = true;
            this.toolTip.SetToolTip(this.CreateTheme, resources.GetString("CreateTheme.ToolTip"));
            this.CreateTheme.UseVisualStyleBackColor = true;
            this.CreateTheme.CheckedChanged += new System.EventHandler(this.CreateTheme_CheckedChanged);
            // 
            // txbURL
            // 
            resources.ApplyResources(this.txbURL, "txbURL");
            this.txbURL.Name = "txbURL";
            this.toolTip.SetToolTip(this.txbURL, resources.GetString("txbURL.ToolTip"));
            this.txbURL.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txbURL_KeyDown);
            // 
            // lblCrURL
            // 
            resources.ApplyResources(this.lblCrURL, "lblCrURL");
            this.lblCrURL.Name = "lblCrURL";
            this.toolTip.SetToolTip(this.lblCrURL, resources.GetString("lblCrURL.ToolTip"));
            // 
            // buttonAdd
            // 
            resources.ApplyResources(this.buttonAdd, "buttonAdd");
            this.buttonAdd.Name = "buttonAdd";
            this.toolTip.SetToolTip(this.buttonAdd, resources.GetString("buttonAdd.ToolTip"));
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // lblCrName
            // 
            resources.ApplyResources(this.lblCrName, "lblCrName");
            this.lblCrName.Name = "lblCrName";
            this.toolTip.SetToolTip(this.lblCrName, resources.GetString("lblCrName.ToolTip"));
            // 
            // txbName
            // 
            resources.ApplyResources(this.txbName, "txbName");
            this.txbName.Name = "txbName";
            this.toolTip.SetToolTip(this.txbName, resources.GetString("txbName.ToolTip"));
            this.txbName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txbName_KeyDown);
            // 
            // EditForm
            // 
            resources.ApplyResources(this.EditForm, "EditForm");
            this.EditForm.Controls.Add(this.btnChangeURL);
            this.EditForm.Controls.Add(this.btnChangeName);
            this.EditForm.Controls.Add(this.lblEditURL);
            this.EditForm.Controls.Add(this.lblEditName);
            this.EditForm.Controls.Add(this.txbEditURL);
            this.EditForm.Controls.Add(this.txbEditName);
            this.EditForm.Controls.Add(this.buttonDelete);
            this.EditForm.Name = "EditForm";
            this.EditForm.TabStop = false;
            this.toolTip.SetToolTip(this.EditForm, resources.GetString("EditForm.ToolTip"));
            // 
            // btnChangeURL
            // 
            resources.ApplyResources(this.btnChangeURL, "btnChangeURL");
            this.btnChangeURL.Name = "btnChangeURL";
            this.toolTip.SetToolTip(this.btnChangeURL, resources.GetString("btnChangeURL.ToolTip"));
            this.btnChangeURL.UseVisualStyleBackColor = true;
            this.btnChangeURL.Click += new System.EventHandler(this.btnChangeURL_Click);
            // 
            // btnChangeName
            // 
            resources.ApplyResources(this.btnChangeName, "btnChangeName");
            this.btnChangeName.Name = "btnChangeName";
            this.toolTip.SetToolTip(this.btnChangeName, resources.GetString("btnChangeName.ToolTip"));
            this.btnChangeName.UseVisualStyleBackColor = true;
            this.btnChangeName.Click += new System.EventHandler(this.btnChangeName_Click);
            // 
            // lblEditURL
            // 
            resources.ApplyResources(this.lblEditURL, "lblEditURL");
            this.lblEditURL.Name = "lblEditURL";
            this.toolTip.SetToolTip(this.lblEditURL, resources.GetString("lblEditURL.ToolTip"));
            // 
            // lblEditName
            // 
            resources.ApplyResources(this.lblEditName, "lblEditName");
            this.lblEditName.Name = "lblEditName";
            this.toolTip.SetToolTip(this.lblEditName, resources.GetString("lblEditName.ToolTip"));
            // 
            // txbEditURL
            // 
            resources.ApplyResources(this.txbEditURL, "txbEditURL");
            this.txbEditURL.Name = "txbEditURL";
            this.toolTip.SetToolTip(this.txbEditURL, resources.GetString("txbEditURL.ToolTip"));
            this.txbEditURL.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txbEditURL_KeyDown);
            this.txbEditURL.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbEditURL_KeyPress);
            // 
            // txbEditName
            // 
            resources.ApplyResources(this.txbEditName, "txbEditName");
            this.txbEditName.Name = "txbEditName";
            this.toolTip.SetToolTip(this.txbEditName, resources.GetString("txbEditName.ToolTip"));
            this.txbEditName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txbEditName_KeyDown);
            this.txbEditName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbEditName_KeyPress);
            // 
            // buttonDelete
            // 
            resources.ApplyResources(this.buttonDelete, "buttonDelete");
            this.buttonDelete.Name = "buttonDelete";
            this.toolTip.SetToolTip(this.buttonDelete, resources.GetString("buttonDelete.ToolTip"));
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // contextMenuStrip
            // 
            resources.ApplyResources(this.contextMenuStrip, "contextMenuStrip");
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.toolTip.SetToolTip(this.contextMenuStrip, resources.GetString("contextMenuStrip.ToolTip"));
            this.contextMenuStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip_ItemClicked);
            // 
            // notifyIcon
            // 
            resources.ApplyResources(this.notifyIcon, "notifyIcon");
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // frmMain
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitter);
            this.Name = "frmMain";
            this.toolTip.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Resize += new System.EventHandler(this.frmMain_Resize);
            this.splitter.Panel1.ResumeLayout(false);
            this.splitter.Panel2.ResumeLayout(false);
            this.splitter.Panel2.PerformLayout();
            this.splitter.ResumeLayout(false);
            this.BrowserGroup.ResumeLayout(false);
            this.BrowserGroup.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.EditForm.ResumeLayout(false);
            this.EditForm.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitter;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.Label lblCrURL;
        private System.Windows.Forms.Label lblCrName;
        private System.Windows.Forms.TextBox txbURL;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.TextBox txbName;
        private System.Windows.Forms.RadioButton CreateLink;
        private System.Windows.Forms.RadioButton CreateTheme;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button CloseAll;
        private System.Windows.Forms.Button ExpandAll;
        private System.Windows.Forms.Label lblLink;
        private System.Windows.Forms.GroupBox BrowserGroup;
        private System.Windows.Forms.Label lblBrowserHint;
        private System.Windows.Forms.TextBox txbBrowser;
        private System.Windows.Forms.GroupBox EditForm;
        private System.Windows.Forms.Label lblEditURL;
        private System.Windows.Forms.Label lblEditName;
        private System.Windows.Forms.TextBox txbEditURL;
        private System.Windows.Forms.TextBox txbEditName;
        private System.Windows.Forms.Button Browse;
        private System.Windows.Forms.Button btnChangeURL;
        private System.Windows.Forms.Button btnChangeName;
    }
}

