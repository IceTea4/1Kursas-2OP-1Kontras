using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K1_pvz
{
    internal class Faculty
    {
        public string facultyName { get; set; }
        public int maxCredits { get; set; }
        private List<Student> students;

        public Faculty(string facultyName, int maxCredits)
        {
            this.students = new List<Student>();
            this.facultyName = facultyName;
            this.maxCredits = maxCredits;
        }

        public void Add(Student student)
        {
            this.students.Add(student);
        }

        public int StudentCount()
        {
            return this.students.Count;
        }

        public Student GetStudent(int index)
        {
            return this.students[index];
        }

        public void Sort()
        {
            for (int i = 0; i < this.StudentCount() - 1; i++)
            {
                int m = i;

                for (int j = i + 1; j < this.StudentCount(); j++)
                {
                    if (students[m].CompareTo(students[j]) > 0)
                    {
                        m = j;
                    }
                }

                Student c = this.students[i];
                this.students[i] = this.students[m];
                this.students[m] = c;
            }
        }

        public static bool operator >(Faculty a, Faculty b)
        {
            return a.StudentCount() > b.StudentCount();
        }

        public static bool operator <(Faculty a, Faculty b)
        {
            return a.StudentCount() < b.StudentCount();
        }

        public static bool operator ==(Faculty a, Faculty b)
        {
            return a.StudentCount() == b.StudentCount();
        }

        public static bool operator !=(Faculty a, Faculty b)
        {
            return a.StudentCount() != b.StudentCount();
        }

        /*
        public void Sort()
        {
            bool flag = true;

            while (flag)
            {
                flag = false;

                for (int i = 0; i < StudentCount() - 1; i++)
                {
                    Student one = students[i];
                    Student two = students[i + 1];

                    if (one.CompareTo(two) > 0)
                    {
                        students[i] = two;
                        students[i + 1] = one;
                        flag = true;
                    }
                }
            }
        }*/
    }
}
