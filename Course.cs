using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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
                Console.WriteLine("Vous êtes dans le Menu de gestion des cours :");
                Console.WriteLine("En fonction de votre besoin, veuillez choisir une des options ci-desous :");
                Console.WriteLine("");
                Console.WriteLine("Tapez 1 pour voir la liste des cours :");
                Console.WriteLine("");
                Console.WriteLine("Tapez 2 pour ajouter un nouveau cours :");
                Console.WriteLine("");
                Console.WriteLine("Tapez 3 pour supprimer un cours :");
                Console.WriteLine("");
                Console.WriteLine("Enfin, tapez 0 pour revenir au Menu principal");

                string choice = Console.ReadLine();

                switch(choice) 
                {
                    case "1":
                        ListCourses();
                        break;
                    
                    case "2":
                        AddCourse();
                        break;

                    case "3":
                        DeleteCourse();
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Choix invalide. Veuillez réessayer.");
                        break;

                }
                
            }
        }

        public void ListCourses()
        {
            Console.WriteLine("Liste des cours : ");
            foreach(var course in courses)
            {
                Console.WriteLine($"ID: {course.CourseId}, Nom: {course.CourseName}");
            }
        }

        public void AddCourse()
        {
            Console.WriteLine("Ajout d'un nouveau cours :");
            Console.WriteLine("Entrez le nom du cours :");
            string courseName = Console.ReadLine();

            int newCourseId = courses.Count > 0 ? courses.Max(c => c.CourseId) + 1 : 1;
            Course newCourse = new Course(newCourseId, courseName);
            courses.Add(newCourse);

            Console.WriteLine("Nouveau cours ajouté avec succès!");
        }

        public void DeleteCourse()
        {
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

            bool confirmDelete = ConfirmAction("Etes-vous sûr de bien vouloir supprimer ce cours ? ");
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
            return response == "oui";
        }

    }
}
