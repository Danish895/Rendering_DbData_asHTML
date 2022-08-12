using StudentAPI.Controllers;

namespace StudentAPI.Extensions
{
    static class HtmlExtensions
    {
        public static void extendedMethod(this StudentDetailsController sdc)
        {
            Console.WriteLine("Method Name: extendedMethod");
        }
    }
}
