using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Service
{
    public class StudentsOneWay : IStudentsOneWay
    {
        private Students data = new Students();

        public void Clear()
        {
            data.StudentList.Clear();
        }

        public void Add(Student student)
        {
            data.StudentList.Add(student.ID, student);
        }

        public void Remove(int studentID)
        {
            if (data.StudentList.Keys.Contains(studentID))
            {
                data.StudentList.Remove(studentID);
            }
        }

        public string GetStudentsValue()
        {
            return data.ToString();
        }
    }
}
