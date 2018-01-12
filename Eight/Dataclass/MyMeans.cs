using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using MySql.Data;
using MySql.Data.MySqlClient;

namespace NoteBook.Dataclass
{
    class MyMeans
    {
        //定义全局变量  当前登录用户的编号
        public static string Login_ID = "";

        //用于判断数据库是否连接成功
        public static MySqlConnection My_con;

        public static string M_str_sqlcon = "server=localhost;user id=root;password=root;database=notebook";

        /// <summary>
        /// 建立数据库连接.
        /// </summary>
        /// <returns>返回MySqlConnection对象</returns>
        public MySqlConnection getcon()
        {
            My_con = new MySqlConnection(M_str_sqlcon);
            My_con.Open();
            return My_con;
        }
        /// <summary>
        /// 关闭连接
        /// </summary>
        public void con_close() {
            if (My_con.State == ConnectionState.Open)
            {
                My_con.Close();
                My_con.Dispose();
            }
        }
        /// <summary>
        /// 执行MySqlCommand   插入 删除 操作
        /// </summary>
        /// <param name="M_str_sqlstr">SQL语句</param>
        public int getcom(string M_str_sqlstr)
        {
            getcon();
            MySqlCommand mysqlcom = new MySqlCommand(M_str_sqlstr, My_con);
            int a = mysqlcom.ExecuteNonQuery();
            mysqlcom.Dispose();
            con_close();
            return a;
        }

        
        /// <summary>
        /// 创建一个MySqlDataReader对象 以只读的方式读取信息
        /// </summary>
        /// <param name="M_str_sqlstr">SQL语句</param>
        /// <returns>返回MySqlDataReader对象</returns>
        public MySqlDataReader getmysqlread(string M_str_sqlstr)
        {
            getcon();
            MySqlCommand mysqlcom = new MySqlCommand(M_str_sqlstr, My_con);
            MySqlDataReader mysqlread = mysqlcom.ExecuteReader(CommandBehavior.CloseConnection);
            return mysqlread;
        }


        /// <summary>
        /// DataSet
        /// </summary>
        public DataSet getDataSet(string SQLstr,string tableName) {
            getcon();
            MySqlDataAdapter SQLda = new MySqlDataAdapter(SQLstr,My_con);
            DataSet My_DataSet = new DataSet();
            SQLda.Fill(My_DataSet,tableName);//填充数据集
            con_close();
            return My_DataSet;//返回DataSet对象信息
        }
    }
}      