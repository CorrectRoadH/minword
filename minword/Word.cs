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
    public partial class Word : Form
    {
        private String fileName;
        public int id;
        private SaveFileDialog saveFileDialog = new SaveFileDialog();
        bool isSave = true;

        public Word(int id)
        {
            InitializeComponent();
            this.id = id;
            fileName = "";
        }

        public Word(int id,String fileName)
        {
            InitializeComponent();
            this.id = id;
            this.fileName = fileName;
            richTextBox1.LoadFile(fileName);
        }

        public int Find(String strSearch, int searchPos, bool isMatch)
        {
            return richTextBox1.Find(strSearch, searchPos, richTextBox1.Text.Length, isMatch?RichTextBoxFinds.MatchCase:RichTextBoxFinds.None);
        }

        public void AllSelect() {
            this.richTextBox1.Select(0, richTextBox1.Text.Length);
        }

        public void insertText(String text)
        {
            Clipboard.Clear();
            Clipboard.SetText(text);
            this.richTextBox1.Paste();
        }

        public void saveFile()
        {
            if (fileName.Equals(""))
            {
                DialogResult result = saveFileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    fileName = saveFileDialog.FileName.ToString();
                    Console.WriteLine(saveFileDialog.FileName.ToString());
                    richTextBox1.SaveFile(fileName);
                    isSave = true;
                }
            }
            else
            {
                richTextBox1.SaveFile(fileName);
                isSave = true;
            }
        }

        public void saveFileToAnother()
        {
            DialogResult result = saveFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                fileName = saveFileDialog.FileName.ToString();
                Console.WriteLine(saveFileDialog.FileName.ToString());
                richTextBox1.SaveFile(fileName);
                isSave = true;
            }
        }
        private void Word_Load(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "rtf document(*.txt)|*.rtf";
            saveFileDialog.FilterIndex = 2;
            saveFileDialog.FileName = "untitle";
            saveFileDialog.DefaultExt = "rtf";
        }

        public void SelectText(int searchPos, int length)
        {
            richTextBox1.Focus();
            richTextBox1.Select(searchPos, length);
        }
        public void CloseWindows() {
            if (!isSave)
            {
                DialogResult dr = MessageBox.Show("关闭", "是否保存更改", MessageBoxButtons.OKCancel);
                if(dr == DialogResult.OK)
                {
                    saveFile();
                }
            }
            this.Hide();
        }

        private void Word_Activated(object sender, EventArgs e)
        {
            Console.WriteLine("be focus");
            Form1.selectindex = this.id;
        }

        public void setSelectedColor(Color color)
        {
            richTextBox1.SelectionColor = color;
        }

        public void setSelectedFont(Font fs)
        {
            richTextBox1.SelectionFont = fs;
        }


        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            isSave = false;
        }

        private void Word_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseWindows();
        }
    }
}
