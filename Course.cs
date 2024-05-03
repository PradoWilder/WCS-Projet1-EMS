using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EducationManagementConsole
{
    public class Course
    {
        private static List<Course> courses = new List<Course>();
        private static Course instance = null;

        public static Course Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Course();
                }
                return instance;
            }
        }

        public List<Course> Courses
        {
            get { return courses; }
        }
        //public List<Course> courses = new List<Course>();

        public int CourseId { get; set; }
        public string CourseName {  get; set; }

        public Course(int courseId, string courseName)
        {
            CourseId = courseId;
            CourseName = courseName;
        }

        public Course() 
        {
            
        }

        public void ManageCourses()
        {
            while (true) 
            {
                Console.WriteLine("");
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Menu de gestion des cours ");
                Console.ResetColor();
                Console.WriteLine("=======================================================================");
                //Console.WriteLine("");
                Console.WriteLine("1 - Afficher la liste des cours ");
                //Console.WriteLine("");
                Console.WriteLine("2 - Ajouter un nouveau cours ");
                //Console.WriteLine("");
                Console.WriteLine("3 - Supprimer un cours ");
                //Console.WriteLine("");
                Console.WriteLine("0 - Revenir au Menu principal");
                Console.WriteLine("=======================================================================");

                string choice = Console.ReadLine();
                Console.Clear();

                switch(choice) 
                {
                    case "1":
                        ListCourses();
                        break;
                    
                    case "2":
                        AddCourse();
                        break;

                    case "3":
                        ListCourses();
                        DeleteCourse();
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Choix invalide. Veuillez réessayer avec l'un des chiffres 1 / 2 / 3 / 0.");
                        break;
                }
                
            }
        }

        public void ListCourses()
        {
            Console.WriteLine("=======================================================================");
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Voici la liste des cours : ");
            Console.ResetColor();
            foreach (var course in courses)
            {
                Console.WriteLine($"ID: {course.CourseId}, Nom: {course.CourseName}");
            }
            Console.WriteLine("=======================================================================");
            Console.WriteLine("");
        }

        public void AddCourse()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Ajout d'un nouveau cours");
            Console.ResetColor();
            Console.WriteLine("=======================================================================");
            Console.Write("Entrez le nom du cours : ");
            string courseName = Console.ReadLine();
            //Mettre un contrôle ici pour valider le nom du cours - pas nécessaire

            int newCourseId = courses.Count > 0 ? courses.Max(c => c.CourseId) + 1 : 1;
            Course newCourse = new Course(newCourseId, courseName);
            courses.Add(newCourse);

            Console.WriteLine("Nouveau cours ajouté avec succès!");
            Console.WriteLine("=======================================================================");
            Console.WriteLine("");
        }

        public void DeleteCourse()
        {
            Console.WriteLine("");
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("ATTENTION !!! - Vous allez supprimer un cours. Opération irréversible!");
            Console.ResetColor();
            Console.WriteLine("Entrez l'ID du cours à supprimer :");
            int courseId;
            if(!int.TryParse(Console.ReadLine(), out courseId))
            {
                Console.WriteLine("ID invalide. Veuillez entrer un nombre entier :");
                return;
            }

            Course course = courses.Find(c => c.CourseId == courseId);
            if(course == null)
            {
                Console.WriteLine("Aucun cours trouvé avec cet ID. ");
                return;
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            bool confirmDelete = ConfirmAction($" Etes-vous sûr de bien vouloir supprimer ce cours ? ");
            Console.ResetColor();
            if (confirmDelete) 
            {
                courses.Remove(course);
                Console.WriteLine("Cours supprimé avec succès !");
            }
            else
            {
                Console.WriteLine("Suppression annulée !");
            }

        }

        public bool ConfirmAction(string message)
        {
            Console.WriteLine(message + "Oui/Non");
            string response = Console.ReadLine().ToLower();
            //Mettre un contrôle ici pour forcer l'utilisateur à écrire oui ou OUI ou Oui

            return response == "oui";
        }
    }
}
