using System.Collections;
using System.Reflection;

namespace Lab_5_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // об'єкт для демонстрації
            DeskLamp myLamp = new DeskLamp("Xiaomi Mi Smart", 12.5, new List<string> { "Читання", "Нічник" });

            DisplayObjectInTree(myLamp, treeView1);
        }

        // root
        public void DisplayObjectInTree(object obj, TreeView tree)
        {
            tree.Nodes.Clear();
            TreeNode rootNode = new TreeNode($"Об'єкт: {obj.GetType().Name}");

            AddPropertiesNode(obj, rootNode);
            AddMethodsNode(obj, rootNode);
            AddConstructorsNode(obj, rootNode);

            tree.Nodes.Add(rootNode);
            tree.ExpandAll();
        }

        // властивості
        private void AddPropertiesNode(object obj, TreeNode parent)
        {
            TreeNode propsBranch = new TreeNode("Властивості");
            PropertyInfo[] props = obj.GetType().GetProperties();

            foreach (var p in props)
            {
                object val = p.GetValue(obj) ?? "null";
                if (val is IEnumerable list && !(val is string))
                {
                    TreeNode listNode = new TreeNode($"{p.Name} ({p.PropertyType.Name})");
                    foreach (var item in list) listNode.Nodes.Add(new TreeNode(item.ToString()));
                    propsBranch.Nodes.Add(listNode);
                }
                else
                {
                    propsBranch.Nodes.Add($"{p.Name} [{p.PropertyType.Name}]: {val}");
                }
            }
            parent.Nodes.Add(propsBranch);
        }

        // методи
        private void AddMethodsNode(object obj, TreeNode parent)
        {
            TreeNode methodsBranch = new TreeNode("Методи");
            MethodInfo[] methods = obj.GetType().GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public |
                                                            BindingFlags.Instance | BindingFlags.NonPublic);

            foreach (var m in methods)
            {
                string parStr = string.Join(", ", Array.ConvertAll(m.GetParameters(), p => $"{p.ParameterType.Name} {p.Name}"));
                methodsBranch.Nodes.Add($"{m.ReturnType.Name} {m.Name}({parStr})");
            }
            parent.Nodes.Add(methodsBranch);
        }

        // конструктори
        private void AddConstructorsNode(object obj, TreeNode parent)
        {
            TreeNode ctorsBranch = new TreeNode("Конструктори");
            ConstructorInfo[] constructors = obj.GetType().GetConstructors();

            foreach (var c in constructors)
            {
                string parStr = string.Join(", ", Array.ConvertAll(c.GetParameters(), p => $"{p.ParameterType.Name} {p.Name}"));
                ctorsBranch.Nodes.Add($"{obj.GetType().Name}({parStr})");
            }
            parent.Nodes.Add(ctorsBranch);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
    }
}
