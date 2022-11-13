using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace minword
{
    public partial class FindForm : Form
    {
        private int searchFormId;
        public String strSearch;
        private int searchPos = 0, lastSearchPos = 0;// 前者表示当前的查找位置，后者表示记录上次的查找位置
        private bool find;

        public FindForm()
        {
            InitializeComponent();
        }

        public FindForm(int searchFormId)
        {
            InitializeComponent();
            this.searchFormId = searchFormId;
        }



        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SearchText();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void findText_TextChanged(object sender, EventArgs e)
        {

        }

        private void FindForm_Load(object sender, EventArgs e)
        {

        }


        public bool SearchText()
        {
            strSearch = findText.Text;//查找的文本
            if (checkCase.Checked)// 表示区分大小写查找
            {
                this.searchPos = Form1.words[Form1.selectindex].Find(strSearch, searchPos, true);
            }
            else// 不区分大小写进行查找
            {
                if (Form1.selectindex == -1)
                {
                    MessageBox.Show("没有打开的文档");
                    return false;
                }
                else
                {
                    this.searchPos = Form1.words[Form1.selectindex].Find(strSearch, searchPos, false);
                }

            }

            if (this.searchPos < 0)//如果未找到
            {
                this.searchPos = this.lastSearchPos;//回到上次查找位置
                find = false;//表示未找到
                MessageBox.Show("搜索完毕");
            }
            else//找到文本
            {
                Console.WriteLine(searchPos);
                var length = this.findText.Text.Trim().Length;// 获取关键字的长度
                //Form1.words[Form1.selectindex].FocusText();// RichTextBox文本框获得焦点
                Form1.words[Form1.selectindex].SelectText(searchPos, length);
                this.lastSearchPos = this.searchPos;//开始查找，把查找位置保存

                this.searchPos += this.strSearch.Length;//新的查找位置是本次开始的查找位置加上查找到文本的长度
            }

            if (searchPos == lastSearchPos)//如果重复查找,则回到起点
            {
                searchPos = 0;
                lastSearchPos = 0;
            }
            return find;
        }
    }
}
