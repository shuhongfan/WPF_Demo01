using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Service
{
    public class StudentsService : IStudentsService
    {
        protected Students data = new Students();

        public Students GetData()
        {
            return data;
        }

        public string Hello(string name)
        {
            return "Hello，" + name;
        }

        public void UpdateScore(Student student, int newScore)
        {
            data.UpdateScore(student, newScore);
        }

        public string GetStudentsValue()
        {
            return data.ToString();
        }
    }
}
