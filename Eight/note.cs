using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Threading;
using System.IO;


namespace NoteBook
{
    public partial class note : Form
    {
        private string Path = "";
        private string FileName = "记事本";
        private bool IsChange;
        public note()
        {
            InitializeComponent();
        }

        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsChange == true)
            {

                SaveTF tf;
                if (Path == "")
                {
                    tf = new SaveTF();
                }
                else
                {
                    tf = new SaveTF(Path + "\\" + FileName);
                }
                tf.ShowDialog();
                if (tf.DialogResult == DialogResult.OK)
                {
                    //保存ToolStripMenuItem_Click(sender, e);
                }
                if (tf.DialogResult == DialogResult.Cancel)
                {
                    return;
                }
            }
            textBox1.Text = "";
            Path = "";
            IsChange = false;
            FileName = "记事本";
            this.Text = FileName;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            IsChange = true;
        }
        void OpenTxt() {
            if (IsChange == true)
            {
                SaveTF tf;
                if (Path == "")
                {
                    tf = new SaveTF();
                }
                else
                {
                    tf = new SaveTF(Path + "\\" + FileName);
                }
                tf.ShowDialog();
                if (tf.DialogResult == DialogResult.OK)
                {
                    //保存ToolStripMenuItem_Click(sender, e);
                }
                if (tf.DialogResult == DialogResult.Cancel)
                {
                    return;
                }
            }

            OpenFileDialog open = new OpenFileDialog();
            string patch = Application.StartupPath + "\\LOG\\";
            open.InitialDirectory = patch;
            open.Filter = "文本文件|*.txt";
            if (open.ShowDialog() == DialogResult.OK)
            {
                if (open.FileName != "")
                {
                    //显示文本文件内容
                    textBox1.Text = File.ReadAllText(open.FileName);
                    //获取文件标题
                    note_name.Text = open.FileName.Substring(open.FileName.LastIndexOf('\\') + 1);
                }

            }
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //打开文件窗口    创建新线程
            Thread newThread = new Thread(new ThreadStart(OpenTxt));
            newThread.SetApartmentState(ApartmentState.STA);
            newThread.Start();
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Path != "" && FileName != "")
            {
                SaveFile(Path + "\\" + FileName);
                this.Text = FileName;
                IsChange = false;
            }
            else
            {
                另存为ToolStripMenuItem_Click(sender, e);
            }
            string name,lable,context;
            name = note_name.Text;
            lable = textBox3.Text;
            context = textBox1.Text;
            //保存到数据库中
            Modle.CNoteBook a = new Modle.CNoteBook(name,lable,context);
            //文件名
            //标签
            //笔记内容
            new ModuleClass.MyModule().save(a);

        }
        //保存
        private void SaveFile(string FullPath)
        {
            try
            {
                StreamWriter streamwriter = new StreamWriter(FullPath, false, System.Text.ASCIIEncoding.Default, 1024000);
                streamwriter.Write(textBox1.Text);//向创建的文件中写入内容
                streamwriter.Close();//关闭当前文件写入流
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void othersaveTxt()
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "文本文件|*.txt";
            if (save.ShowDialog() == DialogResult.OK)
            {
                SaveFile(save.FileName);
                FileName = save.FileName.Substring(save.FileName.LastIndexOf('\\') + 1);
                Path = save.FileName.Substring(0, save.FileName.Length - FileName.Length - 1);
                this.Text = FileName;
                IsChange = false;
            }
        }
        private void 另存为ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //打开文件窗口    创建新线程
            Thread newThread = new Thread(new ThreadStart(othersaveTxt));
            newThread.SetApartmentState(ApartmentState.STA);
            newThread.Start();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 撤销ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Undo();
        }

        private void 剪切ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox1.SelectedText);
            textBox1.SelectedText = "";
        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.SelectedText.Length > 0)
            {
                Clipboard.Clear();
                Clipboard.SetText(textBox1.SelectedText, TextDataFormat.Text);
            }
        }

        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.SelectedText = Clipboard.GetText();
        }

        private void 查找ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            search frmsear = new search(textBox1);
            frmsear.Show(this);
        }

        private void 替换ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            replace frmrepl = new replace(textBox1);
            frmrepl.Show(this);
        }

        private void 全选ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.SelectAll();
        }

        private void 日期ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.SelectedText = DateTime.Now.ToString();
        }

        private void 单词数ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Statistics s = new Statistics(textBox1);
            s.Show();
        }

        

        private void 文件重命名ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmFileRename a = new FrmFileRename();
            a.Show();
        }

        private void 数据浏览ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDataView a = new FrmDataView();
            a.Show();
        }

        private void 字体ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog font = new FontDialog();
            if (font.ShowDialog() == DialogResult.OK)
            {
                textBox1.Font = font.Font;
            }
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            about a = new about();
            a.ShowDialog();
        }

        private void 状态栏ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
