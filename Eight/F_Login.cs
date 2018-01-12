using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MySql.Data;
using MySql.Data.MySqlClient;
namespace NoteBook
{
   
    public partial class F_Login : Form
    {
        Dataclass.MyMeans MyClass = new Dataclass.MyMeans();
        protected override void WndProc(ref Message m)
        {
            const int WM_NCHITTEST = 0x84;
            const int HTCLIENT = 0x01;
            const int HTCAPTION = 0x02;
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_MAXMIZE = 0xF030;
            switch (m.Msg)
            {
                case WM_NCHITTEST:
                    this.DefWndProc(ref m);
                    if (m.Result.ToInt32() == HTCLIENT)
                        m.Result = new IntPtr(HTCAPTION);
                    break;
              
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        public F_Login()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
         
        }
     

        //private void textBox2_TextChanged(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar == '\r')
        //    {
        //        button1.Focus();
        //    }
        //}
        private void button1_Click(object sender, EventArgs e)
        {
            if (login_userid.Text != "" & login_userpassword.Text != "")
            {
                string ID = login_userid.Text.Trim();
                string password = login_userpassword.Text.Trim();
                MySqlDataReader temDR = MyClass.getmysqlread("select * from tb_user where USERID = " + ID + " and USERPASSWORD  = " + password + ";");// 
                bool ifcom = temDR.Read();
                if (ifcom)
                {
                    Dataclass.MyMeans.My_con.Close();
                    Dataclass.MyMeans.My_con.Dispose();

                    this.Close();
                    new System.Threading.Thread(() =>
                    {
                        Application.Run(new txt());
                    }).Start();//this.Close();//关闭当前窗体

                }
                else
                {
                    MessageBox.Show("用户名或密码错误！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    login_userid.Text = "";
                    login_userpassword.Text = "";

                }
                MyClass.con_close();
            }
            else
            {
                MessageBox.Show("请将登录信息填写完整！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        //注册
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            new System.Threading.Thread(() =>
            {
                Application.Run(new F_Regisiter());
            }).Start();//this.Close();//关闭当前窗体
        }
        private void textName_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void F_Login_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Height >= 386)
                timer1.Stop();
            this.Width += 4;
            this.Height += 4;
        }
        protected override void OnClick(EventArgs e)
        {
            timer1.Start();
        }

        private void login_userid_TextChanged(object sender, EventArgs e)
        {

        }

        private void login_userpassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        //private void textName_TextChanged(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar == '\r')
        //    {
        //        textBox2.Focus();
        //    }
        //}
    }
}
