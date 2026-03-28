namespace Lab_6_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            PopulateDrives();
        }

        private void PopulateDrives()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                if (drive.IsReady)
                {
                    TreeNode rootNode = new TreeNode(drive.Name);
                    rootNode.Tag = drive.RootDirectory.FullName;
                    treeView1.Nodes.Add(rootNode);

                    rootNode.Nodes.Add("");
                }
            }
        }

        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            TreeNode node = e.Node;
            node.Nodes.Clear();
            string path = (string)node.Tag;

            try
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                foreach (DirectoryInfo subDir in dir.GetDirectories())
                {
                    // виключення прихованих папок
                    if (!subDir.Attributes.HasFlag(FileAttributes.Hidden))
                    {
                        TreeNode childNode = new TreeNode(subDir.Name);
                        childNode.Tag = subDir.FullName;
                        childNode.Nodes.Add(""); // заглушка для наступного рівня
                        node.Nodes.Add(childNode);
                    }
                }
            }
            catch (UnauthorizedAccessException) { }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Помилка"); }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string path = (string)e.Node.Tag;
            LoadFiles(path);
        }

        private void LoadFiles(string path)
        {
            listView1.Items.Clear();
            try
            {
                DirectoryInfo dir = new DirectoryInfo(path);

                FileInfo[] files = dir.GetFiles();

                foreach (FileInfo file in files)
                {
                    // виключення прихованих файлів
                    if (!file.Attributes.HasFlag(FileAttributes.Hidden))
                    {
                        ListViewItem item = new ListViewItem(file.Name);
                        item.SubItems.Add((file.Length / 1024).ToString());
                        item.SubItems.Add(file.Extension);
                        item.Tag = file.FullName;
                        listView1.Items.Add(item);
                    }
                }
            }
            catch (UnauthorizedAccessException) { }
            catch (DirectoryNotFoundException) { }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                string path = (string)treeView1.SelectedNode.Tag;
                string searchPattern = "*" + txtSearch.Text + "*";

                listView1.Items.Clear();

                listView1.BeginUpdate();

                RecursiveSearch(path, searchPattern);

                listView1.EndUpdate();

                if (listView1.Items.Count == 0)
                {
                    MessageBox.Show("За вашим запитом файлів не знайдено.", "Результат");
                }
            }
            else
            {
                MessageBox.Show("Будь ласка, оберіть папку зліва для здійснення пошуку.", "Увага");
            }
        }
        private void RecursiveSearch(string currentPath, string searchPattern)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(currentPath);

                FileInfo[] files = dir.GetFiles(searchPattern);
                foreach (FileInfo file in files)
                {
                    if (!file.Attributes.HasFlag(FileAttributes.Hidden))
                    {
                        ListViewItem item = new ListViewItem(file.Name);
                        item.SubItems.Add((file.Length / 1024).ToString());
                        item.SubItems.Add(file.Extension);
                        item.Tag = file.FullName;
                        listView1.Items.Add(item);
                    }
                }

                DirectoryInfo[] subDirs = dir.GetDirectories();
                foreach (DirectoryInfo subDir in subDirs)
                {
                    if (!subDir.Attributes.HasFlag(FileAttributes.Hidden) &&
                        !subDir.Attributes.HasFlag(FileAttributes.System))
                    {
                        RecursiveSearch(subDir.FullName, searchPattern);
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
            }
            catch (DirectoryNotFoundException)
            {
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                string filePath = (string)listView1.SelectedItems[0].Tag;

                try
                {
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo(filePath)
                    {
                        UseShellExecute = true
                    };

                    System.Diagnostics.Process.Start(startInfo);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Не вдалося відкрити файл.\nПричина: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
