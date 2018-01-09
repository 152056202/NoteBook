using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace CTestTwo
{
    class Program
    {
        static ArrayList al = new ArrayList();
        static string name = string.Empty;
        static string lable = string.Empty;
        static string content = string.Empty;
        static void Main(string[] args)
        {
            Console.Title="简单记事本SECOND";//设置控制台标题
            Boolean a = true;
            int b = 0;
            //while循环
            while (a)
            {
                //输出菜单
                Console.WriteLine("我的记事本[内测第一版 ]");
                Console.Write("*_*_*_*_*_*_*\n菜单栏------主界面\n1、新建笔记\n2、打开笔记\n3、新建分类\n4、管理分类\n5、退出\n*_*_*_*_*_*_*\n");
                //获取返回值
                b = int.Parse(Console.ReadLine());
                switch (b) { 
                    case 1:
                        newnote();
                        break;
                    case 2:
                        opennote();
                        break;
                    case 3:
                        newlable();
                        break;
                    case 4:
                        managelable();
                        break;
                    case 5:
                        //退出
                        a = false;
                        break;
                    default:
                        break; 
                }//switch
            }//结束while循环
        }
        //新建笔记
        static void newnote() {
            //清空控制台
            Console.Clear();
            //新建笔记
            Console.WriteLine("菜单栏--新建笔记\n");
            Console.Write("请输入笔记名称");
            //输入笔记名称
            name = Console.ReadLine();
            Console.Write("请输入笔记标签");
            //输入笔记标签
            lable = Console.ReadLine();
            Console.Write("请输入笔记内容");
            //输入笔记内容
            content = Console.ReadLine();
            //向集合中添加信息
            al.Add(new note() { Name = name, Lable = lable, Content = content });
            Console.Write("笔记保存成功\n");
        }
        //打开笔记
        static void opennote() {
            //清空控制台
            Console.Clear();
            //打开笔记
            Console.WriteLine("菜单栏--打开笔记\n");
            if (al.Count == 0)
                Console.Write("记事本为空");
            else
            {
                //显示所有笔记名称
                writeName();
                Console.WriteLine("请输入要打开的笔记名称");
                //2、输入笔记名称
                string strname = Console.ReadLine();
                //显示笔记内容
                writeContext(strname);
            }
        }
        //新建分类
        static void newlable() {
            //清空控制台
            Console.Clear();
            //新建分类
            Console.WriteLine("菜单栏--新建分类\n");
            Console.WriteLine("请输入新的分类名称");
            lable = Console.ReadLine();
            int s = 0;
            //分类已存在
            foreach (object o in al)
            {
                if (((note)o).Lable == lable)
                {
                    Console.WriteLine("分类已存在");
                    s = 1;
                }
            }
            if (s == 0)
            {
                //向集合中添加信息
                al.Add(new note() { Name = "", Lable = lable, Content = "" });
            }
            Console.Write("新建笔记分类成功\n");
        }
        //管理分类
        static void managelable() {
            //清空控制台
            Console.Clear();
            //管理分类
            Console.WriteLine("菜单栏--管理分类\n");
            //显示所有分类
            foreach (object o in al)
            {
                Console.WriteLine("分类名称:{0}", ((note)o).Lable);
            }
            //删除分类
            Console.WriteLine("是否要删除分类?[Y/N]");
            string strresult = Console.ReadLine();
            if (strresult == "Y" || strresult == "y")
            {
                //输入分类名称
                Console.WriteLine("请输入分类的名称");
                lable = Console.ReadLine();
                foreach (object o in al)
                {
                    //判断分类是否存在
                    if (((note)o).Lable == lable)
                    {
                        al.Remove(o);
                        Console.WriteLine("分类已成功删除");
                    }
                }
            }
        }
        //输出笔记本内容
        static void writeContext(string strname) {
            foreach (object o in al)
            {
                if (((note)o).Name == strname)
                {
                    Console.WriteLine("笔记内容:{0}", ((note)o).Content);
                }
            }
        }
        //输出笔记本名称
        static void writeName() {
            foreach (object o in al)
            {
                Console.WriteLine("笔记名称:{0}", ((note)o).Name);
            }
        }
    }
    class note
    {
        //定义记事本类
        public string Name;//定义记事本名字字段
        public string Lable;//定义记事本标签字段
        public string Content;//定义记事本内容字段
    }
}
