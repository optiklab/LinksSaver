using System;
using System.Windows;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.InteropServices; // <- для работы с dll
using Microsoft.Win32; // <- для работы с реестром
using System.Resources;
using System.Reflection;
using System.Threading;

namespace AYarkov.LinksSaver
{
    #region Description
    ///<summary>
    /// Description
    ///</summary>
    public partial class frmMain : Form
    {
        #region Constructor
        public frmMain()
        {
            InitializeComponent();
            _mainNode = true;
            CreateTheme.Checked = true;
            _links = new LinksCollection();
            _themes = new ThemesCollection();
            _selectedTheme = -1;
            _selectedLink = -1;
            _dragDropSelected = -1;
            _isThemeSelected = true;
            //        Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ru-RU");
        }
        #endregion  

        #region Static Methods
        // Функция ShellExecute из библиотеки shell32.dll выполняет операцию над указанным файлом.
        [DllImport("shell32.dll")]
        static extern IntPtr ShellExecute(
        IntPtr hwnd,        // <- дескриптор родительского окна.
        string Operation,   // <- операция: может принимать одно из следующих значений: "find", "explore", "edit", "open" или "print"
        string File,        // <- имя файла или папки - в зависимости от значения параметра Operation.
        string Parameters,  // <- список параметров, передаваемых загружаемому приложению
        string Directory,   // <- путь к файлу, указанному в File
        int nShowCmd);      // <- определяет вид главного окна загружаемого приложения
        #endregion

        #region Events

        /// <summary>
        /// Add button click handler
        /// </summary>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            AddNewElement();
        }

        /// <summary>
        /// Delete button click handler
        /// </summary>
        private void buttonDelete_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show(AYarkov.LinksSaver.Properties.Resources.AreYouSure,
                                AYarkov.LinksSaver.Properties.Resources.Warning,  
                                MessageBoxButtons.YesNo, 
                                MessageBoxIcon.Question) == DialogResult.Yes)
                DeleteElement();
        }

        /// <summary>
        /// Radiobutton state changed: "Create theme" selected
        /// </summary>
        private void CreateTheme_CheckedChanged(object sender, EventArgs e)
        {
            _mainNode = true;
            txbURL.Enabled = false;
            txbName.Text = AYarkov.LinksSaver.Properties.Resources.NewThemeName;
        }

        /// <summary>
        /// Radiobutton state changed: "Create link" selected
        /// </summary>
        private void CreateLink_CheckedChanged(object sender, EventArgs e)
        {
            _mainNode = false;
            txbURL.Enabled = true;
            txbName.Text = AYarkov.LinksSaver.Properties.Resources.NewLinkName;
        }

        /// <summary>
        /// Single click in TreeView handler
        /// </summary>
        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode Temp = treeView.SelectedNode;
            
            if (Temp != null)
            {
                int temp = -1; //will contain number of selected node
                if ((temp = _themes.GetID(Temp.Text)) >= 0) //if Theme is selected
                {
                    _selectedTheme = temp;   //remember this 
                    _isThemeSelected = true;
                    EditTheme();
                }
                //TreeView automatically selected this inside itself
                else    //if some link selected
                {  
                    temp = -1;
                    if ((temp = _links.GetID(Temp.Text)) >= 0)
                    {
                        _selectedLink = temp;
                        _isThemeSelected = false;
                        EditLink();
                    }
                }
            }
        }

        /// <summary>
        /// Form loading handler: deserializing
        /// </summary>
        private void frmMain_Load(object sender, EventArgs e)
        {
            //Deserialize
            DeserializeThemes();
            DeserializeLinks();
            AYarkov.LinksSaver.Settings.Default.Reload();
            _pathToBrowser = AYarkov.LinksSaver.Settings.Default.pathToBrowser;
            txbBrowser.Text = _pathToBrowser;
            UpdateTree(-1);
        }

        /// <summary>
        /// Form closing handler: serializing
        /// </summary>
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Serialization
            try
            {
                XmlSerializer s = new XmlSerializer(typeof(ThemesCollection));
                TextWriter w = new StreamWriter(AYarkov.LinksSaver.Properties.Resources.ThemesStorageName);
                s.Serialize(w, _themes);
                w.Close();
                XmlSerializer l = new XmlSerializer(typeof(LinksCollection));
                TextWriter k = new StreamWriter(AYarkov.LinksSaver.Properties.Resources.LinksStorageName);
                l.Serialize(k, _links);
                k.Close();
            }
            catch (System.Security.SecurityException)
            {
                MessageBox.Show(@"Cannot save changed settings due to folder security settings!");
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show(@"Please, use 'Run As Administrator' option to start application!");
            }
            catch (PathTooLongException)
            {
                MessageBox.Show(@"Cannot save changed settings because path to the source file is too long!");
            }
            catch (IOException)
            {
                MessageBox.Show(@"Cannot save changed settings due to I/O Error!");
            }
            catch (Exception)
            {
                MessageBox.Show(@"Cannot save changed settings to some errors!");
            }
        }

        /// <summary>
        /// Form resizing handler: icon appear in task bar
        /// </summary>
        private void frmMain_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
            {
                notifyIcon.Visible = true;
                contextMenuStrip.Items.Clear();
                //foreach(LinkRecord lnk in _links.Collection)
                //    contextMenuStrip.Items.Add(lnk._name);
                for (int i = 0; i < treeView.Nodes.Count; i++)
                {
                    contextMenuStrip.Items.Add(AYarkov.LinksSaver.Properties.Resources.MenuItemDelimeter);
                    for (int j = 0; j < treeView.Nodes[i].Nodes.Count; j++)
                    {
                        contextMenuStrip.Items.Add(treeView.Nodes[i].Nodes[j].Text);
                    }
                }

                contextMenuStrip.Items.Add(AYarkov.LinksSaver.Properties.Resources.MenuItemDelimeter);
                contextMenuStrip.Items.Add(AYarkov.LinksSaver.Properties.Resources.MenuItemExit);
                Hide();
            }
        }

        /// <summary>
        /// Open form from icon
        /// </summary>
        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        }

        /// <summary>
        /// Context menu with list of URLs click handler
        /// </summary>
        private void contextMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text != AYarkov.LinksSaver.Properties.Resources.MenuItemExit && e.ClickedItem.Text != AYarkov.LinksSaver.Properties.Resources.MenuItemDelimeter)
            {
                String URL = _links.GetURL(e.ClickedItem.Text);
                if (URL != String.Empty) // && (URL.StartsWith("www") || URL.StartsWith("http")))
                {
                    OpenLinkInBrowser(URL);
                }
            }
            else
            {
                Application.Exit();
            }
        }

        /// <summary>
        /// Expand method for treeView
        /// Expands all nodes in treeView
        /// </summary>
        private void ExpandAll_Click(object sender, EventArgs e)
        {
            treeView.ExpandAll();
        }

        /// <summary>
        /// Collapse method for treeView
        /// Collapse all nodes in treeView
        /// </summary>
        private void CloseAll_Click(object sender, EventArgs e)
        {
            treeView.CollapseAll();
        }

        /// <summary>
        /// DragDrop for treeView
        /// </summary>
        private void treeView_DragDrop(object sender, DragEventArgs e)
        {
            // Get the screen point.
            Point pt = new Point(e.X, e.Y);
            // Convert to a point in the TreeView's coordinate system. 
            pt = treeView.PointToClient(pt);
            // Get the node underneath the mouse.
            TreeNode node = treeView.GetNodeAt(pt);
            if (_isThemeSelected == true)
            {
                // Add a child node.
                TreeNode child = (TreeNode)e.Data.GetData(typeof(TreeNode));
                int Index = _links.GetID(child.Text);
                //don't allow drop to SAME theme as is
                if (_links.Collection[Index]._parent != _themes.Collection[_selectedTheme]._name) //if ANOTHER theme selected
                {
                    node.Nodes.Add(child);
                    node.Expand();
                    int IndexOfParent = treeView.Nodes.IndexOfKey(_links.Collection[Index]._parent);
                    treeView.Nodes[IndexOfParent].Nodes.RemoveByKey(_links.Collection[Index]._name);
                    _links.Collection[Index]._parent = node.Text;
                }
            }
        }

        /// <summary>
        /// MouseDown for treeView
        /// </summary>
        private void treeView_MouseDown(object sender, MouseEventArgs e)
        {
            // Get the node underneath the mouse.
            TreeNode node = treeView.GetNodeAt(e.X, e.Y);
            treeView.SelectedNode = node;
            // Start the drag-and-drop operation with a cloned copy of the node.
            if (node != null && _isThemeSelected == false)
            {
                _dragDropSelected = _selectedLink;
                treeView.DoDragDrop(node.Clone(), DragDropEffects.Copy);
            }
        }

        /// <summary>
        /// DragOver for treeView
        /// </summary>
        private void treeView_DragOver(object sender, DragEventArgs e)
        {
            // Drag and drop denied by default.
            e.Effect = DragDropEffects.None;
            // Is it a valid format?
            if (e.Data.GetData(typeof(TreeNode)) != null)
            {
                // Get the screen point.
                Point pt = new Point(e.X, e.Y);
                // Convert to a point in the TreeView's coordinate system.
                pt = treeView.PointToClient(pt);
                // Is the mouse over a valid node?
                TreeNode node = treeView.GetNodeAt(pt);
                if (node != null)
                {
                    e.Effect = DragDropEffects.Copy;
                    treeView.SelectedNode = node;
                }
            }
        }

        /// <summary>
        /// Double click on the Node
        /// If link (not theme) is selected, when execute Link in the browser
        /// </summary>
        private void treeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (!_isThemeSelected)
            {
                String URL = _links.GetURL(e.Node.Text);
                if (URL != String.Empty) // && (URL.StartsWith("www") || URL.StartsWith("http")))
                {
                    OpenLinkInBrowser(URL);
                }
            }
        }

        /// <summary>
        /// Select path to browser
        /// </summary>
        private void browse_Click(object sender, EventArgs e)
        {
            String pathToBrowser = String.Empty;
            OpenFileDialog ofd;
            try
            {
                ofd = new OpenFileDialog();
                ofd.Filter = AYarkov.LinksSaver.Properties.Resources.OpenFileDialogOptions;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    pathToBrowser = ofd.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (IsNonEmptyPathToBrowser(pathToBrowser) == true)
            {
                txbBrowser.Text = pathToBrowser;
                _pathToBrowser = pathToBrowser;
                AYarkov.LinksSaver.Settings.Default.pathToBrowser = pathToBrowser;
                AYarkov.LinksSaver.Settings.Default.Save();
            }
        }


        /// <summary>
        /// Press OK when Name edited in textbox
        /// </summary>
        private void btnChangeName_Click(object sender, EventArgs e)
        {
            RenameNode();
        }

        /// <summary>
        /// Press Enter in URL textbox
        /// </summary>
        private void txbEditURL_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                ChangeURL();
                e.Handled = true;
            }
        }

        /// <summary>
        /// Press OK when URL edited in textbox
        /// </summary>
        private void btnChangeURL_Click(object sender, EventArgs e)
        {
            ChangeURL();
        }

        //Editing in TreeView handler
        private void treeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            //try
            //{
            //    if (_isThemeSelected == true)
            //    {
            //        //rename themes in all the links
            //        _links.ChangeThemeInGroup(_themes.Collection[_selectedTheme]._name, e.Label);
            //        _themes.Rename(_selectedTheme, e.Label); //rename theme 
            //    }
            //    else
            //    {
            //        _links.Rename(_selectedLink, e.Label);
            //    }
            //}
            //catch (ItemException ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Editing of the theme: show information from Collection to TextBox
        /// </summary>
        private void EditTheme()
        {
            txbEditURL.Enabled = false;
            txbEditURL.Text = String.Empty;
            txbEditName.Text = _themes.Collection[_selectedTheme]._name;
        }

        /// <summary>
        /// Editing of the link: show information from Collection to TextBox
        /// </summary>
        private void EditLink()
        {
            txbEditURL.Enabled = true;
            txbEditURL.Text = _links.Collection[_selectedLink]._url;
            txbEditName.Text = _links.Collection[_selectedLink]._name;
        }

        /// <summary>
        /// Add selected theme to tree view
        /// </summary>
        private void AddThemeToViews(String ThemeName)
        {
            //exeption
            try
            {
                //add to tree
                treeView.Nodes.Add(ThemeName, ThemeName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "AddThemeToViews()");
            } 
        }

        /// <summary>
        /// Add one new item to both views
        /// </summary>
        private void AddItemToViews(String Name)
        {
            try
            {
                //add to tree
                TreeNode node = treeView.Nodes[_selectedTheme];
                if (node != null)
                    node.Nodes.Add(Name, Name);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message + "AddItemToViews()");
            }
        }

        /// <summary>
        /// Method Updates TreeView - load all items from the collections of _links and _themes
        /// </summary>
        private void UpdateTree(int ThemeToSelect)
        {
            treeView.Nodes.Clear();
            //add themes
            int ThemesCounter = 0, LinksCounter = 0;
            foreach (Themes th in _themes.Collection)
            {
                 AddThemeToViews(th._name);
                 _selectedTheme = ThemesCounter++; //select last added
                //add links
                foreach (LinkRecord lnk in _links.Collection)
                {
                    if (lnk._parent == _themes.Collection[_selectedTheme]._name)
                    {
                        AddItemToViews(lnk._name);
                        _selectedLink = LinksCounter++;
                    }
                }
            }
            if (ThemeToSelect >= 0 && ThemeToSelect < _themes.GetCount())
            {
                _selectedTheme = ThemeToSelect;
            }
        }

        /// <summary>
        /// Delete URL "ItemName" From Both Views
        /// </summary>
        private void DeleteItemFromTree(string ItemName)
        {
            try
            {
                //from TreeView
                for (int i = 0; i < treeView.Nodes.Count; i++)
                    for (int j = 0; j < treeView.Nodes[i].Nodes.Count; j++)
                    {
                        if (treeView.Nodes[i].Nodes[j].Text == ItemName)
                        {
                            treeView.Nodes[i].Nodes[j].Remove();
                            break;
                        }
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "DeleteItemFromViews");
            }
        }

        /// <summary>
        /// Delete Theme "ThemeName" From Tree View
        /// </summary>
        private void DeleteThemeFromTree(string ThemeName)
        {
            try
            {
                //from TreeView
                for (int i = 0; i < treeView.Nodes.Count; i++)
                {
                    if (treeView.Nodes[i].Text == ThemeName)
                    {
                        treeView.Nodes[i].Remove();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "DeleteThemeFromViews()");
            }
        }

        /// <summary>
        /// </summary>
        private void DeserializeThemes()
        {
            try
            {
                FileInfo themesfile = new FileInfo(AYarkov.LinksSaver.Properties.Resources.ThemesStorageName);
                if (themesfile.Exists == true)
                {

                    XmlSerializer s = new XmlSerializer(typeof(ThemesCollection));
                    TextReader r = new StreamReader(AYarkov.LinksSaver.Properties.Resources.ThemesStorageName);
                    _themes = (ThemesCollection)s.Deserialize(r);
                    r.Close();
                }
            }
            catch (System.Security.SecurityException)
            {
                MessageBox.Show(@"Cannot read settings due to folder security settings!");
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show(@"Please, use 'Run As Administrator' option to start application!");
            }
            catch (PathTooLongException)
            {
                MessageBox.Show(@"Cannot read settings because path to the source file is too long!");
            }
            catch (IOException)
            {
                MessageBox.Show(@"Cannot read settings due to I/O Error!");
            }
            catch (Exception)
            {
                MessageBox.Show(@"Cannot read settings due to some errors!");
            }
        }

        /// <summary>
        /// Deserializing method
        /// </summary>
        private void DeserializeLinks()
        {
            try
            {
                FileInfo linksfile = new FileInfo(AYarkov.LinksSaver.Properties.Resources.LinksStorageName);
                if (linksfile.Exists == true)
                {
                    XmlSerializer n = new XmlSerializer(typeof(LinksCollection));
                    TextReader m = new StreamReader(AYarkov.LinksSaver.Properties.Resources.LinksStorageName);
                    _links = (LinksCollection)n.Deserialize(m);
                    m.Close();
                }
            }
            catch (System.Security.SecurityException)
            {
                MessageBox.Show(@"Cannot read settings due to folder security settings!");
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show(@"Please, use 'Run As Administrator' option to start application!");
            }
            catch (PathTooLongException)
            {
                MessageBox.Show(@"Cannot read settings because path to the source file is too long!");
            }
            catch (IOException)
            {
                MessageBox.Show(@"Cannot read settings due to I/O Error!");
            }
            catch (Exception)
            {
                MessageBox.Show(@"Cannot read settings due to some errors!");
            }
        }

        /// <summary>
        /// Rename node: theme or link
        /// </summary>
        private void RenameNode()
        {
            String NewName = txbEditName.Text;
            try
            {
                if (_isThemeSelected == true)
                {
                    _links.ChangeThemeInGroup(_themes.Collection[_selectedTheme]._name, NewName);  //rename themes in all the links
                    _themes.Rename(_selectedTheme, NewName);  //rename theme
                }
                else
                {
                    _links.Rename(_selectedLink, NewName);
                }
            }
            catch (ItemException ex)
            {
                MessageBox.Show(ex.Message);
            }
            UpdateTree(_selectedTheme);
        }

        /// <summary>
        /// Save URL from TextBox
        /// </summary>
        private void ChangeURL()
        {
            _links.Collection[_selectedLink]._url = txbEditURL.Text;
            UpdateTree(_selectedTheme);
        }
        
        /// <summary>
        /// Press Enter in Name textbox
        /// </summary>
        private void txbEditName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                RenameNode();
                e.Handled = true;
            }
        }

        /// <summary>
        /// check path to browser
        /// </summary>
        /// <returns>
        /// FALSE - if path to browser is empty,  otherwise TRUE
        /// </returns>
        private bool IsNonEmptyPathToBrowser(String pathToBrowser)
        {
            if (pathToBrowser != String.Empty)
                return true;
            return false;
        }

        /// <summary>
        /// open link
        /// </summary>
        private void OpenLinkInBrowser(string link)
        {
            // Открываем браузер и передаем в качестве параметра - адрес ресурса: url
            ShellExecute(IntPtr.Zero, "open", _pathToBrowser, link, "", 1);
        }

        /// <summary>
        /// Method create theme or item in the collection and put it into UI elements: tree and list Views
        /// </summary>
        private void AddNewElement()
        {
            String NameTextBox = txbName.Text;
            if (_mainNode == false && _themes.Collection.Count > 0)//add link
            {
                try
                {
                    _links.AddNew(_themes.Collection[_selectedTheme]._name, NameTextBox, txbURL.Text);
                    AddItemToViews(NameTextBox); //Add last item to the selected treeNode
                    _selectedLink = _links.GetCount() - 1; //select last
                }
                catch (ItemException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (_mainNode == true) //add theme
            {
                try
                {
                    _themes.AddNew(NameTextBox);
                    AddThemeToViews(NameTextBox);
                    _selectedTheme = _themes.GetCount() - 1; //select last
                }
                catch (ItemException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        /// delete element (theme or url) from collections and from views
        /// </summary>
        /// <returns>
        /// FALSE - if fails, TRUE - if success
        /// </returns>
        private bool DeleteElement()
        {
            TreeNode node = treeView.SelectedNode;
            if (node != null)
            {
                if (_themes.IsUnique(node.Text) == false)
                {
                    _themes.DeleteTheme(node.Text);
                    _links.DeleteByTheme(node.Text);
                    DeleteThemeFromTree(node.Text); //auto delete all sub nodes
                    _selectedTheme = _themes.GetCount() - 1;
                    return true;
                }
                else
                {
                    _links.Delete(node.Text);
                    DeleteItemFromTree(node.Text);
                    _selectedLink = _links.GetCount() - 1;
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region Private Member Variables

        /// <summary>
        /// 
        /// </summary>
        public LinksCollection _links;

        /// <summary>
        /// 
        /// </summary>
        public ThemesCollection _themes;

        /// <summary>
        /// What to create: theme (if true) or link (if false)
        /// </summary>
        private bool _mainNode;

        /// <summary>
        /// Number of selected theme
        /// </summary>
        private int _selectedTheme;

        /// <summary>
        /// Number of selected link
        /// </summary>
        private int _selectedLink;

        /// <summary>
        /// Last selected item: theme (if true) or link (if false)
        /// </summary>
        private bool _isThemeSelected;

        /// <summary>
        /// Index of Link which was selected for drag&drop
        /// </summary>
        private int _dragDropSelected;

        /// <summary>
        /// 
        /// </summary>
        private string _pathToBrowser;

        #endregion

        #region Ctrl + A

        private void txbName_KeyDown(object sender, KeyEventArgs e)
        {
            if (txbName.Focused == true && e.KeyData == (Keys.A | Keys.Control))
            {
                txbName.SelectAll();
            }
        }

        private void txbURL_KeyDown(object sender, KeyEventArgs e)
        {
            if (txbURL.Focused == true && e.KeyData == (Keys.A | Keys.Control))
            {
                txbURL.SelectAll();
            }
        }

        private void txbEditName_KeyDown(object sender, KeyEventArgs e)
        {
            if (txbEditName.Focused == true && e.KeyData == (Keys.A | Keys.Control))
            {
                txbEditName.SelectAll();
            }
        }

        private void txbEditURL_KeyDown(object sender, KeyEventArgs e)
        {
            if (txbEditURL.Focused == true && e.KeyData == (Keys.A | Keys.Control))
            {
                txbEditURL.SelectAll();
            }
        }

        private void txbBrowser_KeyDown(object sender, KeyEventArgs e)
        {
            if (txbBrowser.Focused == true && e.KeyData == (Keys.A | Keys.Control))
            {
                txbBrowser.SelectAll();
            }
        }

        #endregion
    }
    #endregion
}