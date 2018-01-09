using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTestThree
{
    class note
    {
        //定义记事本类
        public string Name;//定义记事本名字字段
        public string Lable;//定义记事本标签字段
        public string Path;//定义记事本路径字段

        //重载构造方法
        public note(string name, string lable, string path)
        {
            //初始化  name  lable  path
            this.Name = name;
            this.Lable = lable;
            this.Path = path;
        }
    }
}
