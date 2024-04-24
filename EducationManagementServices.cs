using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationManagementConsole
{
    public class EducationManagementServices
    {
        private DataHandler dataHandler;
        private Student student;
        private Course course;

        public EducationManagementServices() 
        { 
            dataHandler = new DataHandler();
            student = new Student();
            course = new Course();
        }
        
        public void DisplayMainMenu()
        {
            while (true)
            {
                Console.WriteLine("Menu Principal :");
                Console.WriteLine("Tapez 1 pour accéder au Menu de Gestion des élèves :");
                Console.WriteLine("Tapez 2 pour accéder au Menu de Gestion des cours :");
                Console.WriteLine("Tapez 0 pour Quitter :");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        student.ManageStudents();
                        break;

                    case "2":
                        course.ManageCourses();
                        break;

                    case "0":
                        dataHandler.SaveDataToFiles();
                        Console.WriteLine("Merci d'avoir utilisé l'application. A bientôt !");
                        return;

                    default:
                        Console.WriteLine("Choix invalide. Veuillez réessayer !");
                        break;
                }

            }

        } 
    }
}
