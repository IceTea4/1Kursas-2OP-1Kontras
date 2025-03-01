using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace K1_pvz
{
    internal class InOut
    {
        public static Faculty ReadFaculty(string fileName)
        {
            string[] Lines = File.ReadAllLines(fileName, Encoding.UTF8);

            string[] firstLine = Regex.Split(Lines[0], "[,\\s]");
            string facultyName = firstLine[0];
            int maxCredits = int.Parse(firstLine[1]);

            Faculty faculty = new Faculty(facultyName, maxCredits);

            for (int i = 1; i < Lines.Count(); i++)
            {
                string[] splits = Regex.Split(Lines[i], "[,\\s]");

                Student student = new Student(splits[0], splits[1], splits[2], Credits(splits));

                faculty.Add(student);
            }

            return faculty;
        }

        private static int[] Credits(string[] splits)
        {
            int[] credits = new int[splits.Count() - 3];
            int x = 0;

            for (int i = 3; i < splits.Count(); i++)
            {
                credits[x] = int.Parse(splits[i]);
                x++;
            }

            return credits;
        }

        public static void PrintFaculty(Faculty faculty, string fileName, string header)
        {
            File.AppendAllText(fileName, header, Encoding.UTF8);

            List<string> lines = new List<string>();

            lines.Add(" " + faculty.maxCredits);
            lines.Add(new string('-', 53));
            lines.Add(String.Format($"| {"Pavardė",-15} | {"Vardas",-11} | {"Grupė",-6} | {"Kreditai",-8} |"));
            lines.Add(new string('-', 53));

            for (int i = 0; i < faculty.StudentCount(); i++)
            {
                lines.Add(String.Format($"| {faculty.GetStudent(i).Surname,-15} | {faculty.GetStudent(i).Name,-11} | {faculty.GetStudent(i).Group,-6} | {faculty.GetStudent(i).Sum(faculty.GetStudent(i).CreditsCount() - 1),8} |"));
                lines.Add(new string('-', 53));
            }

            lines.Add("");

            File.AppendAllLines(fileName, lines, Encoding.UTF8);
        }
    }
}
