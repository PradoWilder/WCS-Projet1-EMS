using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Menu Principal :");
                Console.ResetColor();
                Console.WriteLine("----------------------------------");
                Console.WriteLine("1 - Menu de Gestion des élèves :");
                Console.WriteLine("2 - Menu de Gestion des cours :");
                Console.WriteLine("0 - Quitter :");
                Console.WriteLine("----------------------------------");

                string choice = Console.ReadLine();
                Console.Clear();

                switch (choice)
                {
                    case "1":
                        student.ManageStudents();
                        dataHandler.LoadDataFromFiles();
                        dataHandler.SaveDataToFiles();
                        break;

                    case "2":
                        course.ManageCourses();
                        dataHandler.LoadDataFromFiles();
                        dataHandler.SaveDataToFiles();
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
