using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NoteBook
{
    public partial class F_Regisiter : Form
    {
        Dataclass.MyMeans MyClass = new Dataclass.MyMeans();
        public F_Regisiter()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        private void regisiter_Click(object sender, EventArgs e)
        {
            if (regisiter_id.Text != "" & regisiter_password.Text != "")
            {
                string ID = regisiter_id.Text.Trim();
                string password = regisiter_password.Text.Trim();
                int a = MyClass.getcom("insert into tb_user values(" + ID + "," + password + ");");
                if (a > 0)
                {
                    MessageBox.Show("注册成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    new System.Threading.Thread(() =>
                    {
                        Application.Run(new F_Login());
                    }).Start();//this.Close();//关闭当前窗体

                }
                else
                {
                    MessageBox.Show("用户名已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    regisiter_id.Text = "";
                    regisiter_password.Text = "";

                }
            }
            else
            {
                MessageBox.Show("请将登录信息填写完整！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void regisiter_back_Click(object sender, EventArgs e)
        {
            this.Close();
            new System.Threading.Thread(() =>
            {
                Application.Run(new F_Login());
            }).Start();//this.Close();//关闭当前窗体

        }

        private void regisiter_password_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
