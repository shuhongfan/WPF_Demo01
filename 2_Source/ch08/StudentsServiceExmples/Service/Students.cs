using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Service
{
    public class Students
    {
        public Dictionary<int, Student> StudentList { get; set; }

        public Students()
        {
            StudentList = new Dictionary<int, Student>();
            StudentList.Add(13001, new Student { ID = 13001, Name = "张三", Score = 70 });
            StudentList.Add(13002, new Student { ID = 13002, Name = "李四", Score = 80 });
            StudentList.Add(13003, new Student { ID = 13003, Name = "王五", Score = 90 });
        }

        public void UpdateScore(Student student, int newScore)
        {
            if (StudentList.Keys.Contains(student.ID))
            {
                var a = StudentList.Where((t) => t.Key == student.ID).First();
                a.Value.Score = newScore;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var v in StudentList.Values)
            {
                sb.AppendFormat("学号：{0}，姓名：{1}, 成绩：{2}, {3}",
                    v.ID, v.Name, v.Score, v.OtherInfo);
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}