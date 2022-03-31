using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService
{
    //public的类、结构、接口、枚举默认都拥有数据协定
    public class Student
    {
        //同时具有get和set并且声明为public的属性默认都拥有成员协定
        public int ID { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public string OtherInfo { get; set; }

        public Student()
        {
            ID = 0;
            Name = "张三";
            Score = 50;
            OtherInfo = "无其他信息";
        }

        public override string ToString()
        {
            return string.Format(
                "学号：{0}，姓名：{1}, 成绩：{2}, {3}",
                ID, Name, Score, OtherInfo);
        }
    }
}