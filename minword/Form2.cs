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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.textBox1.Text = "\r\n\r\n迷你记事本V1.1\r\n\r\n乐山师范学院\r\n计信系**级 某某某\r\n\r\n版权所有\r\n\r\nCopyright 2008";
            this.label1.Text = "当前显示图片1";

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
