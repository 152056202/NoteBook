using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections;
using System.Windows.Forms;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;


namespace NoteBook.ModuleClass
{
    //MyModule将系统中所有窗体的动态调用以及动态的添加、修改、删除和查询的SQL语句等全部封装到了指定的自定义方法中
    class MyModule
    {
        //为了调用它的方法
        Dataclass.MyMeans MyDataClass = new Dataclass.MyMeans();
        public static string ADDs = "";//用来存储或修改的SQL语句
        public static string FindValue = "";//存储查询条件

        //存储当前登录用户的编号
        public static string User_ID = "";

        /////////*******************************/////////
        //动态添加treeview控件相应的节点
        public void getTreeviewnode(TreeView treeV) {
            //存储从DataSet中获取的要用的数据
            ArrayList al = new ArrayList();
            string name, type;
            //从数据库获取到数据  标签 名称 内容
            DataSet ds = MyDataClass.getDataSet("select tb_notebook.NTNAME,tb_notetype.NTYPENAME from tb_notebook,tb_userhistory,tb_notetype WHERE(UHUSERID = '123' AND UHNOTEBOOKID = NTID AND NTYPEID = NTTYPE );","tb_notebook");
            //获取dataset的第一张table，取其他table只须改下标
            DataTable dt=ds.Tables[0];
            //遍历行
            foreach(DataRow dr in dt.Rows)
            {
                 name = dr[0].ToString();
                 type = dr[1].ToString();
                 al.Add(new Modle.CNoteBook(name, type));
            }
            //从数据库获取根节点 type
            foreach (object o in al)
            {
                TreeNode newNode1 = treeV.Nodes.Add(((Modle.CNoteBook)o).Type);
                TreeNode newNode2 = newNode1.Nodes.Add(((Modle.CNoteBook)o).Name);
            }
            //从数据库获取子节点
        }
        //添加数据时，自动获取添加数据的编号
        public string GetAutocoding(string TableName, string ID) {
            //查找指定表中id号为最大的记录
            MySqlDataReader MyDR = MyDataClass.getmysqlread("select max("+ID+") from "+TableName);
            int Num = 0;
            if(MyDR.HasRows){
                MyDR.Read();
                if(MyDR[0].ToString()=="")
                    return "0001";
                Num = Convert.ToInt32(MyDR[0].ToString());//找到最大值并转化为整数
                ++Num;
                string s = Num + "";
                return s;//返回自动生成的编号
            }
            return "0001";
        }

        /////////*******************************/////////
        //根据用户选中的节点输出笔记内容
        public string TreeMenuF(TreeNodeMouseClickEventArgs e) {
            string a = "";
            //存储从DataSet中获取的要用的数据
            ArrayList al = new ArrayList();
            string name, type,context;
            //从数据库获取到数据  标签 名称 内容
            DataSet ds = MyDataClass.getDataSet("select NTNAME,NTTYPE,NTCONTEXT from tb_notebook", "tb_notebook");
            //获取dataset的第一张table，取其他table只须改下标
            DataTable dt = ds.Tables[0];
            //遍历行
            foreach (DataRow dr in dt.Rows)
            {
                name = dr[0].ToString();
                type = dr[1].ToString();
                context = dr[2].ToString();
                al.Add(new Modle.CNoteBook(name, type,context));
            }
            foreach (object o in al)
            {
                if (((Modle.CNoteBook)o).Name == e.Node.Text) {
                    a = ((Modle.CNoteBook)o).Context;
                }
            }
            return a;
        }

        //将用户刚创建的笔记保存到数据库中
        public int save(Modle.CNoteBook a)
        {
            //获取相应语句，调用函数，执行插入功能
            //将笔记名称 内容存储到notebook表中
            //insert into tb_userhistory values('123','00002');
            //将笔记类型先存储到notetype表中
            string ntypeid = GetAutocoding("tb_notetype", "NTYPEID");
            MyDataClass.getcom("insert into tb_notetype values("+ntypeid+","+a.Type+");"+"insert NTID,NTNAME,NTCONTET into tb_notebook values()");

            return 1;
        }
    }
}

