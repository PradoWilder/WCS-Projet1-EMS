namespace EducationManagementConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            EducationManagementServices ems = new EducationManagementServices();
            ems.DisplayMainMenu();
        }
    }
}
