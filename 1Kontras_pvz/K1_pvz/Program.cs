using System.Text;
using System.Text.RegularExpressions;

namespace K1_pvz
{
    class Program
    {
        const string printFile = "Rezult.txt";

        static void Main(string[] args)
        {
            File.Delete(printFile);

            Faculty faculty1 = InOut.ReadFaculty(@"../../../Duom1.txt");
            Faculty faculty2 = InOut.ReadFaculty(@"../../../Duom2.txt");

            InOut.PrintFaculty(faculty1, printFile, faculty1.facultyName);
            InOut.PrintFaculty(faculty2, printFile, faculty2.facultyName);

            faculty1 = TaskUtils.Select(faculty1);
            faculty2 = TaskUtils.Select(faculty2);

            faculty1.Sort();
            faculty2.Sort();

            InOut.PrintFaculty(faculty1, printFile, faculty1.facultyName);
            InOut.PrintFaculty(faculty2, printFile, faculty2.facultyName);

            if (faculty1 > faculty2)
            {
                File.AppendAllText(printFile, faculty1.facultyName + " fakultetas turi daugiau studentų viršijusių kreditų normą", Encoding.UTF8);
            }
            else if (faculty1 == faculty2)
            {
                File.AppendAllText(printFile, "Abiejuose fakultetuose yra po tiek pat studentų viršijusių kreditų normą", Encoding.UTF8);
            }
            else
            {
                File.AppendAllText(printFile, faculty2.facultyName + " fakultetas turi daugiau studentų viršijusių kreditų normą", Encoding.UTF8);
            }
        }
    }
}
