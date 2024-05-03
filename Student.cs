using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
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
                Console.Write(" ");
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Menu de gestion des élèves :");
                Console.ResetColor();
                Console.WriteLine("================================================================");
                //Console.Write(" ");
                //Console.WriteLine(" ");
                Console.WriteLine("1 - Afficher la liste des élèves");
                //Console.WriteLine(" ");
                Console.WriteLine("2 - Créer un nouvel élève");
                //Console.WriteLine(" ");
                Console.WriteLine("3 - Consulter un élève existant");
                //Console.WriteLine(" ");
                Console.WriteLine("4 - Ajouter une note et une appréciation à un élève existant");
                //Console.WriteLine(" ");
                Console.WriteLine("5 - Afficher une fiche détaillée de l'étudiant");
                //Console.WriteLine(" ");
                Console.WriteLine("0 - Revenir au Menu principal");
                Console.WriteLine("================================================================");
                

                string choice = Console.ReadLine();
                Console.Clear();

                switch (choice)
                {
                    case "1":
                        ListStudents();
                        break;

                    case "2":
                        CreateStudent();
                        break;

                    case "3":
                        ListStudents();
                        ConsultStudent();
                        break;

                    case "4":
                        ListStudents();
                        Course.Instance.ListCourses();
                        Console.WriteLine();
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
            Console.WriteLine("================================================================");
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Voici la liste des élèves :");
            Console.ResetColor();
            foreach (var student in students)
            {
                Console.WriteLine($"ID: {student.StudentId}: Prénom : {student.StudentFirstName, -15}  Nom : {student.StudentLastName, -15}");
            }
            Console.WriteLine("================================================================");
            Console.WriteLine("");
        }

        public void CreateStudent()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Création d'un nouvel élève :");
            Console.ResetColor();

            while (true)
            {
                Console.WriteLine("================================================================");
                Console.Write("Entrez le nom de l'élève : ");
                string lastName = Console.ReadLine();

                if (!StringValidation(lastName))
                {
                    Console.WriteLine("Le nom saisi n'est pas valide. Réessayez! ");
                    Console.WriteLine("");
                    return;
                }

                Console.Write("Entrez le prénom de l'élève : ");
                string firstName = Console.ReadLine();

                if (!StringValidation(firstName))
                {
                    Console.WriteLine("Le prénom saisi n'est pas valide");
                    return;
                }

                Console.Write("Entrez la date de naissance de l'élève au format (jj/mm/aaaa.) ");
                // DateTime studentBirthDate;
                if (!DateTime.TryParse(Console.ReadLine(), out studentBirthDate))
                {
                    Console.WriteLine("Format de date incorrect. Veuillez entrer une date au format jj/mm/aaaa.");
                    return;
                }

                //Vérification si la date de naissance est postérieure à la date du jour
                if(studentBirthDate > DateTime.Today)
                {
                    Console.WriteLine("Erreur: La date de naissance ne peut pas être postérieure à la date du jour");
                    return;
                }

                //Vérification si l'élève a moins de 6 ans
                int age = DateTime.Today.Year - studentBirthDate.Year;
               
                if(age < 6)
                {
                    Console.WriteLine("L'élève n'est même pas encore en classe primaire. Veuillez vérifier la date de naissance.");
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
                Console.WriteLine("============================================================");
                Console.WriteLine("");
                break;
            }
        }

        public void ConsultStudent()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Consultation d'un élève : ");
            Console.ResetColor();
            Console.WriteLine("================================================================");
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
                Console.WriteLine();
                return;
            }

            Console.WriteLine(student.GetStudentDetails());
            Console.WriteLine("================================================================");
        }

        public void AddMarkToStudent()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Ajout d'une note et d'une appréciation à un élève :");
            Console.ResetColor();
            Console.WriteLine("================================================================");
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
                Console.WriteLine();
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
                Console.WriteLine("");
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
            //Un contrôle à implémenter ici pour l'appréciation - pas nécessaire!

            string courseName = selectedCourse.CourseName;
            student.AddMark(courseId, courseName, mark, appreciation);
            Console.WriteLine("Note ajoutée avec succès !");
            Console.WriteLine("=========================================================");
            Console.WriteLine("");
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
            Console.WriteLine("****************************************************************");
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
                //details += $"*****************************************************************\n\n";
            }

            double average = CalculateAverage();
            details += $"Moyenne : {average:F2} / 20 ";
            Console.WriteLine("");
            return details;
            
        }

        public double CalculateAverage()
        {
            if (studentMarks.Count == 0)
            {
                Console.WriteLine("Aucune note disponible pour calculer la moyenne.");
                return 0;
            }

            double sum = 0;
            foreach (var kvp in studentMarks)
            {
                sum += kvp.Value.Item2;
            }

            return sum / studentMarks.Count;
        }


        public bool StringValidation(string input)
        {
            if (input == null) return false;

            // Autorise les noms avec des lettres, des apostrophes, des tirets et des espaces multiples entre les mots
            string regex = @"^[A-Za-zÀ-ÖØ-öøüß'’\-]+(\s+[A-Za-zÀ-ÖØ-öøüß'’\-]+)*$";
            return Regex.IsMatch(input, regex);
        }



        public bool ValidBirthDateFormat(string birthDateString)
        {
            if (birthDateString == null) return false;

            string regex = @"^(0?[1-9]|[1-2][0-9]|3[0-1])\/(0?[1-9]|1[0-2])\/(19[0-9][0-9]|20[0-2][0-9])$";
            return Regex.IsMatch(birthDateString, regex);
        }
    }
}
