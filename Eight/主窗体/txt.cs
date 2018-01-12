using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

using System.Data.SqlClient;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace NoteBook
{

  
    public partial class txt : Form
    {
        private string Path = "";
        private string FileName="记事本";
        private bool IsChange;
        ModuleClass.MyModule MyMenu = new ModuleClass.MyModule();


        #region 构造函数
        public txt()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            
            int Position = textBox.SelectionStart + textBox.SelectionLength;
            Line.Text = FillStr("行数" + (textBox.GetLineFromCharIndex(Position) + 1).ToString(), 10, ' ');
            Column.Text = FillStr("列数" + (Position - textBox.GetFirstCharIndexFromLine(textBox.GetLineFromCharIndex(Position)) + 1).ToString(), 10, ' ');
            this.Text = FileName;         
            自动换行ToolStripMenuItem_CheckedChanged(null, null);
            IsChange = false;
        }

        public txt(string FullPath)
        {
            InitializeComponent();            
            this.Text = FileName;
            textBox.Text = OpenFile(FullPath);
            textBox.Select(textBox.TextLength, 0);
            int Position = textBox.SelectionStart + textBox.SelectionLength;
            Line.Text = FillStr("行数" + (textBox.GetLineFromCharIndex(Position) + 1).ToString(), 10, ' ');
            Column.Text = FillStr("列数" + (Position - textBox.GetFirstCharIndexFromLine(textBox.GetLineFromCharIndex(Position)) + 1).ToString(), 10, ' '); 
            int i = FullPath.LastIndexOf('\\');
            FileName = FullPath.Substring(i + 1);
            Path = FullPath.Substring(0, FullPath.Length - FileName.Length - 1);
            this.Text = FileName;            
            自动换行ToolStripMenuItem_CheckedChanged(null, null);
            IsChange = false;
        }
        #endregion

        #region 菜单栏
        #region 文件
        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new System.Threading.Thread(() =>
            {
                Application.Run(new note());
            }).Start();//this.Close();//关闭当前窗体
           
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //打开文件窗口    创建新线程
            Thread newThread = new Thread(new ThreadStart(OpenTxt));
            newThread.SetApartmentState(ApartmentState.STA);
            newThread.Start();
        }
        void OpenTxt()
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

            OpenFileDialog open = new OpenFileDialog();
            string patch = Application.StartupPath + "\\LOG\\";
            open.InitialDirectory = patch;
            open.Filter = "文本文件|*.txt";
            if (open.ShowDialog() == DialogResult.OK)
            {
                if (open.FileName != "")
                {
                    Path = open.FileName;
                    //显示文本文件内容
                    textBox.Text = File.ReadAllText(Path);
                    //获取文件标题
                    NoteName.Text = open.FileName.Substring(open.FileName.LastIndexOf('\\') + 1);
                }

            } 
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
        }
        void othersaveTxt() {
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
        #endregion 

        #region 编辑
        private void 撤销ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox.Undo();
        }

        private void 剪切ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox.SelectedText);
            textBox.SelectedText = "";
        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox.SelectedText.Length > 0)
            {
                Clipboard.Clear();
                Clipboard.SetText(textBox.SelectedText,TextDataFormat.Text);
            }
        }

        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox.SelectedText = Clipboard.GetText();
        }

        private void 查找ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            search frmsear = new search(textBox);
            frmsear.Show(this);
        }

        private void 替换ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            replace frmrepl = new replace(textBox);
            frmrepl.Show(this);
        }

        private void 全选ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox.SelectAll();
        }

        private void 日期ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox.SelectedText = DateTime.Now.ToString();
        }
        #endregion

        #region 工具
        private void 单词数ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Statistics s = new Statistics(textBox);
            s.Show();
        }

       

        private void 数据浏览ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDataView a = new FrmDataView();
            a.Show();
        }

        #region 文件操作
        private void 文件重命名ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmFileRename a = new FrmFileRename();
            a.Show();
        }
        #endregion

        #endregion

        #region 格式
        private void 自动换行ToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (自动换行ToolStripMenuItem.Checked == true)
            {
                textBox.WordWrap = true;
            }
            if (自动换行ToolStripMenuItem.Checked == false)
            {                
                textBox.WordWrap = false;
                textBox.ScrollBars = ScrollBars.Both;
            }
            int Position = textBox.SelectionStart + textBox.SelectionLength;
            Line.Text = FillStr("行数" + (textBox.GetLineFromCharIndex(Position) + 1).ToString(), 10, ' ');
            Column.Text = FillStr("列数" + (Position - textBox.GetFirstCharIndexFromLine(textBox.GetLineFromCharIndex(Position)) + 1).ToString(), 10, ' ');
        }

        private void 字体ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog font = new FontDialog();
            if (font.ShowDialog() == DialogResult.OK)
            {
                textBox.Font = font.Font;
            }
        }
        private void 颜色ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            ColorDialog color = new ColorDialog();
            if (color.ShowDialog() == DialogResult.OK)
            {
                textBox.ForeColor =color.Color;
            }

        }
        #endregion
        
        #region 查看
        private void 状态栏ToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (状态栏ToolStripMenuItem.Checked == false)
            {
                statusStrip.Visible = false;
            }
            if (状态栏ToolStripMenuItem.Checked == true)
            {
                statusStrip.Visible = true;
            }
        }

        #endregion
        
        #region 帮助
        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            about a = new about();
            a.ShowDialog();
        }
        #endregion

        #endregion

        private void txt_FormClosing(object sender, FormClosingEventArgs e)
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
                    保存ToolStripMenuItem_Click(sender, e);
                }
                if (tf.DialogResult == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            //IsChange = true;
            //this.Text = "*" + FileName;
        }

        private string OpenFile(string FullPath)
        {
            try
            {
                StreamReader streamread = new StreamReader(FullPath, System.Text.ASCIIEncoding.Default);
                string Text = "";
                int Size = 1;
               
                Text = streamread.ReadToEnd();
                streamread.Close();
                return Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            
        }

        //保存
        private void SaveFile(string FullPath)
        {
            try
            {
                StreamWriter streamwriter = new StreamWriter(FullPath, false, System.Text.ASCIIEncoding.Default, 1024000);
                streamwriter.Write(textBox.Text);//向创建的文件中写入内容
                streamwriter.Close();//关闭当前文件写入流
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string FillStr(string Str, int Length, char c)
        {
            if (Str.Length >= Length)
            {
                return Str;
            }
            string Result = Str;
            int i;
            for (i = Str.Length; i < Length; i++)
            {
                Result += c.ToString();
            }
            return Result;
        }


        private void textBox_MouseCaptureChanged(object sender, EventArgs e)
        {
            int Position = textBox.SelectionStart + textBox.SelectionLength;
            Line.Text = FillStr("行数" + (textBox.GetLineFromCharIndex(Position) + 1).ToString(), 10, ' ');
            Column.Text = FillStr("列数" + (Position - textBox.GetFirstCharIndexFromLine(textBox.GetLineFromCharIndex(Position)) + 1).ToString(), 10, ' ');
        }

        private void textBox_KeyUp(object sender, KeyEventArgs e)
        {
            int Position = textBox.SelectionStart + textBox.SelectionLength;
            Line.Text = FillStr("行数" + (textBox.GetLineFromCharIndex(Position) + 1).ToString(), 10, ' ');
            Column.Text = FillStr("列数" + (Position - textBox.GetFirstCharIndexFromLine(textBox.GetLineFromCharIndex(Position)) + 1).ToString(), 10, ' ');
        }

        private void txt_Load(object sender, EventArgs e)
        {
            MyMenu.getTreeviewnode(treeView1);
        }

        private void 文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void 状态栏ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 格式ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            NoteName.Text = e.Node.Text;
        }

        private void btn_delete_Click_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode.Text == "")
            {
                MessageBox.Show("请选择要删除的笔记或分类");
            }
            else
            {
                MessageBox.Show("确定要删除吗", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //当再次确认时，删除
               // if (MessageBox == MessageBoxButtons.OK)
                treeView1.Nodes.Remove(treeView1.SelectedNode);
            }
        }

        private void NoteName_Click(object sender, EventArgs e)
        {

        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            string a = MyMenu.TreeMenuF(e);
            textBox.Text = a;
        }

        private void btn_change_Click(object sender, EventArgs e)
        {
            if (IsChange)
            {
                IsChange = false;
                this.Text = FileName;
            }
            else {
                IsChange = true;
                this.Text = "*" + FileName;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
