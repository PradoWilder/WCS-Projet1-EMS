using System.Security.Cryptography;
using System;


namespace EducationManagementConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "EDUCATION MANAGEMENT CONSOLE";

            // Définit la couleur de fond et de texte de la console
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.Yellow;

            // Affiche le titre en grande lettre
            string text = "EDUCATION MANAGEMENT CONSOLE";
            int leftPadding = (Console.WindowWidth - text.Length)/2;
            Console.SetCursorPosition(leftPadding, Console.CursorTop);
            // Display the text in large font (double size)
            Console.WriteLine(text.ToUpper());

            // Réinitialise les couleurs à celles par défaut
            Console.ResetColor();

            Console.WriteLine("");

            // Continuer avec le programme...
            EducationManagementServices ems = new EducationManagementServices();
            ems.DisplayMainMenu();
        }
    }
}
