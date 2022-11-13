using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace minword
{
    public partial class Form1 : Form
    {
        private static int FormCount = 0;
        public static List<Word> words = new List<Word>();
        public static int selectindex = -1; // 当前选中的是哪个word;  

        //定义此常量是为了统计MDI窗体数目，
        MainMenu mnuMain = new MainMenu();
        MenuItem FileMenu;

        MenuItem ExitMenu;
        MenuItem EditMenu;
        MenuItem SytleMenu;
        MenuItem AutomationNewLineMenu;



        MenuItem WindowMenu;
        MenuItem HelpMenu;

        private PageSetupDialog pageSetupDialog = new PageSetupDialog();
        private PrintDialog printDialog = new PrintDialog();
        private PrintDocument printDocument = new PrintDocument();
        private PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();


        public Form1()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
            this.Text = "miniword";
            FileMenu = new MenuItem();
            FileMenu.Text = "文件(&F)";
            FileMenu.MenuItems.Add("新建", new EventHandler(New_Click));
            FileMenu.MenuItems[0].Shortcut = Shortcut.CtrlN;
            FileMenu.MenuItems.Add("打开", new EventHandler(OpenFile));
            FileMenu.MenuItems[1].Shortcut = Shortcut.CtrlO;
            FileMenu.MenuItems.Add("保存", new EventHandler(saveFile));
            FileMenu.MenuItems[2].Shortcut = Shortcut.CtrlS;
            FileMenu.MenuItems.Add("另存为", new EventHandler(saveFileToAnother));
            FileMenu.MenuItems.Add("全部保存", new EventHandler(saveALLFile));
            FileMenu.MenuItems.Add("关闭", new EventHandler(closeCurrent));
            FileMenu.MenuItems.Add("全部关闭", new EventHandler(closeAll));
            FileMenu.MenuItems.Add(new MenuItem("-"));
            FileMenu.MenuItems.Add("页面设置", new EventHandler(PageSetupToolStripMenuItem_Click));
            FileMenu.MenuItems.Add("打印预览", new EventHandler(PrintPreviewToolStripMenuItem_Click));
            FileMenu.MenuItems.Add("打印", new EventHandler(printCtrlPToolStripMenuItem_Click));
            FileMenu.MenuItems[10].Shortcut = Shortcut.CtrlQ;

            FileMenu.MenuItems.Add(new MenuItem("-"));
            
            ExitMenu = new MenuItem();
            ExitMenu.Shortcut = Shortcut.CtrlQ;
            ExitMenu.Text = "退出";
            ExitMenu.Click += new EventHandler(Exit_Click);
            FileMenu.MenuItems.Add(ExitMenu);

            EditMenu = new MenuItem();
            EditMenu.Text = "编辑(&E)";

            SytleMenu = new MenuItem();
            SytleMenu.Text = "设置文本";
            EditMenu.MenuItems.Add(SytleMenu);
            SytleMenu.MenuItems.Add("颜色", new EventHandler(selectColor));
            SytleMenu.MenuItems.Add("字体", new EventHandler(selectFont));

            EditMenu.MenuItems.Add("查找", new EventHandler(openFindForm));
            EditMenu.MenuItems[1].Shortcut = Shortcut.CtrlF;

            EditMenu.MenuItems.Add("查找下一个", new EventHandler(findNext));
            EditMenu.MenuItems[2].Shortcut = Shortcut.F3;

            EditMenu.MenuItems.Add("插入时间/日期", new EventHandler(InsterData));
            AutomationNewLineMenu = new MenuItem();
            AutomationNewLineMenu.Text = "自动换行";
            AutomationNewLineMenu.Checked = false;
            AutomationNewLineMenu.Click += new EventHandler(togglNewline);
            EditMenu.MenuItems.Add(AutomationNewLineMenu);

            EditMenu.MenuItems.Add("全选", new EventHandler(AllSelect));
            EditMenu.MenuItems[5].Shortcut = Shortcut.CtrlA;



            WindowMenu = new MenuItem();
            WindowMenu.Text = "窗口(&W)";
            WindowMenu.MenuItems.Add("堆叠", new EventHandler(Cascade_Click));
            WindowMenu.MenuItems.Add("平铺", new EventHandler(TileH_Click));
            WindowMenu.MdiList = true;
            //这一句比较重要，有了这一句就可以实现在新建一个MDI窗体后会在此主菜单项下显示存在的MDI窗体菜单项


            HelpMenu = new MenuItem();
            HelpMenu.Text = "帮助(&H)";
            HelpMenu.MenuItems.Add("关于", new EventHandler(aboutToolStripMenuItem_Click));


            mnuMain.MenuItems.Add(FileMenu);
            mnuMain.MenuItems.Add(EditMenu);
            mnuMain.MenuItems.Add(WindowMenu);
            mnuMain.MenuItems.Add(HelpMenu);

            this.Menu = mnuMain;

        }

        private void selectColor(object sender, EventArgs e)
        {
            if (selectindex != -1)
            {

                ColorDialog cd = new ColorDialog();
                DialogResult rs = 
                    cd.ShowDialog();
                if(rs == DialogResult.OK)
                {
                    words[selectindex].setSelectedColor(cd.Color);
                    }
                }
            else
            {
                MessageBox.Show("当前无有效文档");

            }
        }

        private void selectFont(object sender, EventArgs e)
        {
            if (selectindex != -1)
            {
                FontDialog fd = new FontDialog();
                DialogResult rs =
                    fd.ShowDialog();
                if (rs == DialogResult.OK)
                {
                    words[selectindex].setSelectedFont(fd.Font);
                }
            }
            else
            {
                MessageBox.Show("当前无有效文档");

            }
        }

        private void openFindForm(object sender, EventArgs e)
        {
            new FindForm().Show();

        }

        private void findNext(object sender, EventArgs e)
        {
            new FindForm().Show();
        }

        private void togglNewline(object sender, EventArgs e)
        {
            if (selectindex != -1)
            {
                AutomationNewLineMenu.Checked = !AutomationNewLineMenu.Checked;
                words.ForEach((Word word) =>
                {
                    word.toggleAutomationNewLine(AutomationNewLineMenu.Checked);
                });
            }
            else
            {
                MessageBox.Show("当前无有效文档");
            }
        }



        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
        }

        private void saveFile(object sender, EventArgs e)
        {
            if(selectindex != -1)
            {
                words[selectindex].saveFile();

            }
            else
            {
                MessageBox.Show("当前无有效文档");

            }

        }

        private void saveALLFile(object sender, EventArgs e)
        {
            words.ForEach((Word word) =>
            {
                word.saveFile();
            });

        }

        private void saveFileToAnother(object sender, EventArgs e)
        {
            if (selectindex != -1)
            {
                words[selectindex].saveFileToAnother();
            }
            else
            {
                MessageBox.Show("当前无有效文档");

            }

        }

        private void closeCurrent(object sender, EventArgs e)
        {
            if (selectindex != -1)
            {
                words[selectindex].CloseWindows();
            }
            else
            {
                MessageBox.Show("当前无有效文档");

            }


        }
        private void closeAll(object sender, EventArgs e)
        {
            words.ForEach((Word word) =>
            {
                word.CloseWindows();
            });
        }

        private void PageSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.pageSetupDialog.ShowDialog();
        }

        private void printCtrlPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.printDialog.ShowDialog();
        }

        private void PrintPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.printPreviewDialog.ShowDialog();
        }


        private void OpenFile(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }
        private void Cascade_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void InsterData(object sender, EventArgs e)
        {
            if (selectindex != -1)
                words[selectindex].insertText(DateTime.Now.ToLocalTime().ToString());
        }

        private void AllSelect(object sender, EventArgs e)
        {
            if (selectindex != -1)
                words[selectindex].AllSelect();
        }



        private void TileH_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void New_Click(object sender, EventArgs e)
        {
            Word frmTemp = new Word(FormCount);
            //新建一个窗体
            frmTemp.MdiParent = this;
            //定义此窗体的父窗体，从而此窗体成为一个MDI窗体
            frmTemp.Text = "新建文档" + (FormCount+1).ToString();
            //设定MDI窗体的标题
            FormCount++;
            frmTemp.Show();
            words.Add(frmTemp);
            //把此MDI窗体显示出来
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.pageSetupDialog.Document = printDocument;
            this.pageSetupDialog.AllowMargins = true;
            this.pageSetupDialog.AllowOrientation = true;
            this.pageSetupDialog.AllowPaper = true;
            this.pageSetupDialog.AllowPrinter = true;
            this.printPreviewDialog.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog.SetAutoScrollMargin(0, 0);
            this.printPreviewDialog.ClientSize = new Size(400, 300);
            this.printPreviewDialog.Document = printDocument;
            this.printDialog.Document = printDocument;
            this.printDialog.AllowSomePages = true;
            this.printDialog.AllowSelection = true;
            this.printDialog.AllowPrintToFile = true;
            AutomationNewLineMenu.Checked = true;

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            Word frmTemp = new Word(FormCount,openFileDialog1.FileName);
            frmTemp.MdiParent = this;
            frmTemp.Text = openFileDialog1.FileName;
            frmTemp.Show();
            FormCount++;
            words.Add(frmTemp);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            New_Click(sender,e);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            OpenFile(sender, e);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            saveFile(sender, e);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            saveALLFile(sender, e);
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            printCtrlPToolStripMenuItem_Click(sender,e);

        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            Exit_Click(sender, e);
        }
    }
}
