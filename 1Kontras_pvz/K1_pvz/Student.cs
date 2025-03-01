using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K1_pvz
{
    internal class Student
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Group { get; set; }
        private int[] Credits { get; set; }

        public Student(string surname, string name, string group, int[] credits)
        {
            this.Surname = surname;
            this.Name = name;  
            this.Group = group;

            this.Credits = new int[credits.Length];

            for (int i = 0; i < credits.Length; i++)
            {
                this.Credits[i] = credits[i];
            }
        }

        public int CreditsCount()
        {
            return this.Credits.Length;
        }

        public int Sum(int ii)
        {
            if (ii < 0)
            {
                return 0;
            }
            else
            {
                return this.Credits[ii] + Sum(ii - 1);
            }
        }

        public int CompareTo(Student other)
        {
            int temp = this.Group.CompareTo(other.Group);

            if (temp == 0)
            {
                return other.Surname.CompareTo(this.Surname);
            }

            return temp;
        }
    }
}
