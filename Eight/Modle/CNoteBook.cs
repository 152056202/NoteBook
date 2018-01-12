using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NoteBook.Modle
{
    class CNoteBook
    {
        //定义记事本类
        private string id;
        private string name;//定义记事本名字字段
        private string path;//定义记事本路径字段
        private string type;//定义记事本标签字段
        private string context;

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        

        public string Path
        {
            get { return path; }
            set { path = value; }
        }
        

        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        
        public string Context
        {
            get { return context; }
            set { context = value; }
        }


        //重载构造方法   名字  类型  内容
        public CNoteBook(string name, string type, string context)
        {
            //初始化  name  lable  path
            this.name= name;
            this.type = type;
            this.context = context;
        }
        public CNoteBook(string name, string type)
        {
            //初始化  name  lable  path
            this.name = name;
            this.type = type;
        }

    }
}
