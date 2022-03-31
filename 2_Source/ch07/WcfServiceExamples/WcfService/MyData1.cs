﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WcfService
{
    //--------------------------------------------------------------------
    //这段代码只是为了说明概念
    //实际开发时，如果没有特殊需要，建议不要对私有成员声明DataMenber
    //--------------------------------------------------------------------

    [DataContract]
    public class MyData1
    {
        //不论是private还是public，只要声明Datamember就可以序列化
        public string MyName1 = "me1";  //未声明Datamember，无法序列化
        [DataMember]
        public string MyName2 = "me2";  //可序列化
        [DataMember]
        private string myName3 = "me3"; //可序列化

        [DataMember]
        public int Age { get; set; } //可序列化

        [DataMember]
        public List<Student> MyStudents { get; set; } //序列化为Student数组

        private string telephone = "null"; //无法序列化
        [DataMember]
        public string Telephone  //可序列化
        {
            get { return telephone; }
            set { telephone = value; }
        }
    }
}