using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K1_pvz
{
    internal class TaskUtils
    {
        public static Faculty Select(Faculty faculty)
        {
            Faculty selected = new Faculty(faculty.facultyName, faculty.maxCredits);

            for (int i = 0; i < faculty.StudentCount(); i++)
            {
                if (faculty.GetStudent(i).Sum(faculty.GetStudent(i).CreditsCount() - 1) > faculty.maxCredits)
                {
                    selected.Add(faculty.GetStudent(i));
                }
            }

            return selected;
        }
    }
}
