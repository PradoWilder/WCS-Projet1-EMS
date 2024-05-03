using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace EducationManagementConsole
{
    public class DataHandler
    {
        private const string dataFile = "data.json";
        private const string logFile = "data.log";

        public List<Student> Students { get; set; } = new List<Student>();
        public List<Course> Courses { get; set; } = new List<Course>();

        public void LoadDataFromFiles()
        {
            if (File.Exists(dataFile))
            {
                string jsonData = File.ReadAllText(dataFile);
                var data = JsonConvert.DeserializeObject<Data>(jsonData);
                Students = data.StudentData;
                Courses = data.CourseData;
            }
        }

        public void SaveDataToFiles()
        {
            var data = new Data
            {
                StudentData = Students,
                CourseData = Courses
            };

            string jsonData = JsonConvert.SerializeObject(data);
            File.WriteAllText(dataFile, jsonData);
        }

        public void LogAction(string action)
        {
            using (StreamWriter writer = new StreamWriter(logFile, true))
            {
                writer.WriteLine($"{DateTime.Now}: {action}");
            }
        }
    }

    public class Data
    {
        public List<Student> StudentData { get; set; } = new List<Student>();
        public List<Course> CourseData { get; set; } = new List<Course>();
    }
}
