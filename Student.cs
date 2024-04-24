using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EducationManagementConsole
{
    public class Student
    {
        private int studentId;
        private string studentLastName;
        private string studentFirstName;
        private DateTime studentBirthDate;
        private DateTime studentGraduationDate;
        private Dictionary<int, Tuple<string, double, string>> studentMarks { get; set; } = new Dictionary<int, Tuple<string, double, string>>();

        public int StudentId { get { return studentId; } set { studentId = value; } }
        public string StudentLastName { get { return studentLastName; } set { studentLastName = value; } }
        public string StudentFirstName { get { return studentFirstName; } set { studentFirstName = value; } }
        public DateTime StudentBirthDate { get { return studentBirthDate; } set { studentBirthDate = value; } }
        public DateTime StudentGraduationDate { get { return studentGraduationDate; } set { studentGraduationDate = value; } }
        public Dictionary<int, Tuple <string, double, string>> StudentMarks { get {  return studentMarks; } set { studentMarks = value; } }

        public Student(int studentId, string studentFirstName, string studentLastName, DateTime studentBirthDate)
        {
            StudentId = studentId;
            StudentLastName = studentLastName;
            StudentFirstName = studentFirstName;
            StudentBirthDate = studentBirthDate;
        }

        public Student()
        {
            StudentMarks = new Dictionary<int, Tuple<string, double, string>>();
        }

        // List<Student> students = new List<Student>();
        // List<Course> courses = new List<Course>();
        //Ci-dessous les formes simplifiées des instanciations précédentes!

        List<Student> students = new();
        List<Course> courses = new();


        public void ManageStudents()
        {
            while (true)
            {
                Console.WriteLine("Vous êtes dans le Menu de gestion des élèves :");
                Console.WriteLine(" ");
                Console.WriteLine("Veuillez choisir une des options ci-dessous en fonction de votre besoin ");
                Console.WriteLine(" ");
                Console.WriteLine("Tapez 1 pour afficher la liste des élèves");
                Console.WriteLine(" ");
                Console.WriteLine("Tapez 2 pour créer un nouvel élève");
                Console.WriteLine(" ");
                Console.WriteLine("Tapez 3 pour consulter un élève existant");
                Console.WriteLine(" ");
                Console.WriteLine("Tapez 4 pour ajouter une note et une appréciation à un élève existant");
                Console.WriteLine(" ");
                Console.WriteLine("Tapez 5 pour avoir une fiche détaillée de l'étudiant");
                Console.WriteLine(" ");
                Console.WriteLine("Tapez 0 pour revenir au Menu principal");
                Console.WriteLine(" ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ListStudents();
                        break;

                    case "2":
                        CreateStudent();
                        break;

                    case "3":
                        ConsultStudent();
                        break;

                    case "4":
                        AddMarkToStudent();
                        break;

                    case "5":
                        GetStudentDetails();
                        break;

                    case "0":
                        return;

                }
            }
        }

        public void ListStudents()
        {
            Console.WriteLine("Voici la liste des élèves :");
            foreach (var student in students)
            {
                Console.WriteLine($"ID: {student.StudentId}, Prénom : {student.StudentFirstName}, Nom : {student.StudentLastName}");
            }
        }

        public void CreateStudent()
        {
            Console.WriteLine("Création d'un nouvel élève :");
            Console.WriteLine(" ");
            Console.WriteLine("Entrez le nom de l'élève :");
            string lastName = Console.ReadLine();
            //contrôle à implémenter ici sur le nom
            Console.WriteLine("Entrez le prénom de l'élève :");
            string firstName = Console.ReadLine();
            //contrôle à implémenter ici sur le prénom
            Console.WriteLine("Entrez la date de naissance de l'élève au format (jj/mm/aaaa.)");
            // DateTime studentBirthDate;
            if (!DateTime.TryParse(Console.ReadLine(), out studentBirthDate))
            {
                Console.WriteLine("Format de date incorrect. Veuillez entrer une date au format jj/mm/aaaa.");
                return;
            }

            int newStudentId;
            if (students.Count > 0)
            {
                int maxStudentId = students.Max(s => s.StudentId);
                newStudentId = maxStudentId + 1;
            }
            else
            {
                newStudentId = 1;
            }

            Student newStudent = new Student(newStudentId, firstName, lastName, studentBirthDate);
            students.Add(newStudent);

            Console.WriteLine("Nouvel élève ajouté avec succès !");
        }

        public void ConsultStudent()
        {
            Console.WriteLine("Entrez l'ID de l'élève à consulter :");
            //int studentId;
            if(!int.TryParse(Console.ReadLine(), out studentId)) 
            {
                Console.WriteLine("ID invalide. Veuillez entrer un nombre entier.");
                return;
            }

            Student student = students.Find(s => s.StudentId == studentId);
            if (student == null) 
            {
                Console.WriteLine("Aucun élève correspond à cet ID. ");
            }

            Console.WriteLine(student.GetStudentDetails());
        }

        public void AddMarkToStudent()
        {
            Console.WriteLine("Entrez l'ID de l'élève pour lequel vous souhaitez ajouter une note");
            //int studentId;
            if (!int.TryParse(Console.ReadLine(), out studentId))
            {
                Console.WriteLine("ID invalide. Veuillez entrer un nombre entier.");
                return;
            }
            Student student = students.Find(s => s.StudentId == studentId);
            if (student == null)
            {
                Console.WriteLine("Aucun élève trouvé avec cet ID. ");
                return;
            }
            Console.WriteLine("Choisissez un cours parmi les cours suivants :");
            foreach(var course in courses)
            {
                Console.WriteLine($"ID: {course.CourseId}, Nom: {course.CourseName}");
            }
            Console.Write("Entrez l'ID du cours :");
            int courseId;
            if (!int.TryParse(Console.ReadLine(), out courseId))
            {
                Console.WriteLine("ID de cours invalide. Veuillez entrer un nombre entier.");
                return;
            }
            //A vérifier à ce niveau pourquoi la valeur courseId est nulle
            Course selectedCourse = Course.Instance.Courses.Find(c=> c.CourseId == courseId);
            //Course selectedCourse = courses.Find(c => c.CourseId == courseId);
            if (selectedCourse == null)
            {
                Console.WriteLine("Aucun cours trouvé avec cet ID : ");
                return;
            }

            Console.Write("Entrez la note :");
            double mark;
            if (!double.TryParse(Console.ReadLine(), out mark))
            {
                Console.WriteLine("Note invalide. Veuillez entrer un nombre :");
                return;
            }

            Console.Write("Entrez l'appréciation du professeur (Facultatif) :");
            string appreciation = Console.ReadLine();
            //Un contrôle à implémenter ici pour l'appréciation

            string courseName = selectedCourse.CourseName;
            student.AddMark(courseId, courseName, mark, appreciation);
            Console.WriteLine("Note ajoutée avec succès !");
        }
        public void AddMark(int courseId, string courseName, double mark, string appreciation = "")
        {
            if(StudentMarks.ContainsKey(courseId))
            {
                Console.WriteLine("Ce cours a déjà une note pour cet étudiant ");
                return;
            }
            studentMarks.Add(courseId, Tuple.Create(courseName, mark, appreciation));
        }

        public string GetStudentDetails()
        {
            string details = $"Informations sur l'élève : \n\n";
            details += $"Prénom                     : {StudentFirstName} \n";
            details += $"Nom                        : {StudentLastName} \n";
            details += $"Date de naissance          : {StudentBirthDate.ToShortDateString()}\n\n";
            details += $"Résultats scolaires: \n\n";

            foreach (var kvp in StudentMarks)
            {
                int courseId = kvp.Key;
                string courseName = kvp.Value.Item1;
                double mark = kvp.Value.Item2;
                string appreciation = kvp.Value.Item3;

                details += $"       Cours : {courseName}\n";
                details += $"        Note : {kvp.Value.Item2}/20\n";
                details += $"        Appréciation : {kvp.Value.Item3}\n\n";
            }
            return details;
        }

    }

}
