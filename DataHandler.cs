using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace EducationManagementConsole
{
    public class DataHandler
    {
        private const string studentsFile = "students.json";
        private const string coursesFile = "courses.json";
        private const string logFile = "log.txt";

        public List<Student> students = new List<Student>();
        public List<Course> courses = new List<Course>();

        public void LoadDataFromFiles() 
        {
            if (File.Exists(studentsFile))
            {
                string studentsJson = File.ReadAllText(studentsFile);
                students = JsonSerializer.Deserialize<List<Student>>(studentsJson);    
            }

            if(File.Exists(coursesFile))
            {
                string coursesJson = File.ReadAllText(coursesFile);
                courses = JsonSerializer.Deserialize<List<Course>>(coursesJson);
            }
        }

        public void SaveDataToFiles()
        {
            string studentsJson = JsonSerializer.Serialize(students);
            File.WriteAllText(studentsFile, studentsJson);

            string coursesJson = JsonSerializer.Serialize(courses);
            File.WriteAllText(coursesFile, coursesJson);
        }
    }
}
