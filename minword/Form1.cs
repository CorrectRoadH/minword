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
        private static int FormCount = 1;
        //定义此常量是为了统计MDI窗体数目，
        MainMenu mnuMain = new MainMenu();
        MenuItem FileMenu;
        MenuItem NewMenu;
        MenuItem ExitMenu;
        MenuItem WindowMenu;

        public Form1()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
            this.Text = "MDI演示程序";
            FileMenu = new MenuItem();
            FileMenu.Text = "文件";
            WindowMenu = new MenuItem();
            WindowMenu.Text = "窗口(&W)";
            WindowMenu.MenuItems.Add("堆叠(&C)", new EventHandler(Cascade_Click));
            WindowMenu.MenuItems.Add("平铺(&V)", new EventHandler(TileH_Click));
            WindowMenu.MdiList = true;
            //这一句比较重要，有了这一句就可以实现在新建一个MDI窗体后会在此主菜单项下显示存在的MDI窗体菜单项
            NewMenu = new MenuItem();
            NewMenu.Text = "新建窗体(&N)";
            NewMenu.Click += new EventHandler(New_Click);
            ExitMenu = new MenuItem();
            ExitMenu.Text = "退出(&X)";
            ExitMenu.Click += new EventHandler(Exit_Click);
            FileMenu.MenuItems.Add(NewMenu);
            FileMenu.MenuItems.Add(new MenuItem("-"));
            FileMenu.MenuItems.Add(ExitMenu);
            mnuMain.MenuItems.Add(FileMenu);
            mnuMain.MenuItems.Add(WindowMenu);
            this.Menu = mnuMain;

        }

        private void Cascade_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        

        private void TileH_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void New_Click(object sender, EventArgs e)
        {
            Form frmTemp = new Form();
            //新建一个窗体
            frmTemp.MdiParent = this;
            //定义此窗体的父窗体，从而此窗体成为一个MDI窗体
            frmTemp.Text = "新建文档" + FormCount.ToString();
            //设定MDI窗体的标题
            FormCount++;
            frmTemp.Show();
            //把此MDI窗体显示出来
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Application.Exit();
        }

    }
}
