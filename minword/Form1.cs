using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace minword
{
    public partial class Form1 : Form
    {
        private int formCount = 1;
        private ToolStripMenuItem subItem;

        public Form1()
        {
            InitializeComponent();
            this.IsMdiContainer = true;

        }
        ToolStripMenuItem AddContextMenu(string text, ToolStripItemCollection cms, EventHandler callback)
        {
            if (text == "-")
            {
                ToolStripSeparator tsp = new ToolStripSeparator();
                cms.Add(tsp);
                return null;
            }
            else if (!string.IsNullOrEmpty(text))
            {
                ToolStripMenuItem tsmi = new ToolStripMenuItem(text);
                tsmi.Tag = text + "TAG";
                if (callback != null) tsmi.Click += callback;
                cms.Add(tsmi);
                return tsmi;
            }
            return null;
        }
        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frmTemp = new Form();
            //新建一个窗体
            frmTemp.MdiParent = this;
            //定义此窗体的父窗体，从而此窗体成为一个MDI窗体
            frmTemp.Text = "新建文档" + formCount.ToString();
            //设定MDI窗体的标题
            frmTemp.Show();

            AddContextMenu("新建文档" + formCount.ToString(), subItem.DropDownItems, new EventHandler(堆叠ToolStripMenuItem_Click));
            formCount++;

        }

        private void selectChildWindow()
        {

        }

        private void 堆叠ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);

        }

        private void 平铺ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            subItem = AddContextMenu("窗口", menuStrip1.Items, null);

            //添加子菜单 
            AddContextMenu("堆叠", subItem.DropDownItems, new EventHandler(堆叠ToolStripMenuItem_Click));
            AddContextMenu("平铺", subItem.DropDownItems, new EventHandler(平铺ToolStripMenuItem_Click));

        }
    }
}
