﻿//Aistis Jakutonis IFF3-1

using System.Text;

namespace _1Kontras
{
    class Program
    {
        const string rez = "Result.txt";

        static void Main(string[] args)
        {
            File.Delete(rez);

            Team team1 = InOutUtils.TeamRead(@"../../../Team11.txt");
            Team team2 = InOutUtils.TeamRead(@"../../../Team12.txt");

            InOutUtils.Print(team1, rez, team1.TeamName);
            InOutUtils.Print(team2, rez, team2.TeamName);

            TaskUtils.Remove(team1);
            TaskUtils.Remove(team2);

            team1.Selection();
            team2.Selection();

            InOutUtils.Print(team1, rez, team1.TeamName);
            InOutUtils.Print(team2, rez, team2.TeamName);

            if (team1 > team2)
            {
                File.AppendAllText(rez, team1.TeamName + " turi daugiau gerų žaidėjų", Encoding.UTF8);
            }
            else if (team1 == team2)
            {
                File.AppendAllText(rez, "Abi turi po lygiai gerų žaidėjų", Encoding.UTF8);
            }
            else
            {
                File.AppendAllText(rez, team2.TeamName + " turi daugiau gerų žaidėjų", Encoding.UTF8);
            }
        }
    }

    internal class Player
    {
        public string Surname { get; }
        public string Name { get; }
        private int[] Goals { get; }

        public Player(string surname, string name, int[] goals)
        {
            this.Surname = surname;
            this.Name = name;
            
            this.Goals = new int[goals.Length];

            for (int i = 0; i < goals.Length; i++)
            {
                this.Goals[i] = goals[i];
            }
        }

        public int GoalsCount()
        {
            return this.Goals.Length;
        }

        //bloga rekursija?
        public int CalcG(int ii)
        {
            if (ii < 0)
            {
                return 0;
            }
            else
            {
                return this.Goals[ii] + CalcG(ii - 1);
            }
        }

        //sumos rekursija
        public int Sum(int ii)
        {
            if (ii == 0)
            {
                return Goals[ii];
            }
            else
            {
                return this.Goals[ii] + Sum(ii - 1);
            }
        }

        //vidurkio rekursija
        public double Vidurk(int ii)
        {
            if (ii < 0)
            {
                return 0;
            }
            else
            {
                if (ii == GoalsCount() - 1)
                {
                    return (this.Goals[ii] + Vidurk(ii - 1)) / GoalsCount();
                }

                return this.Goals[ii] + Vidurk(ii - 1);
            }
        }

        //maximum rekursija
        public int Maxim(int ii)
        {
            if (ii == 0)
            {
                return Goals[ii];
            }

            return Math.Max(Goals[ii], Maxim(ii - 1));
        }

        //minimum rekursija
        public int Minim(int ii)
        {
            if (ii == 0)
            {
                return Goals[ii];
            }

            return Math.Min(Goals[ii], Minim(ii - 1));
        }

        //kiek maziau uz 5
        public int Less(int ii)
        {
            if (ii < 0)
            {
                return 0;
            }
            else if (this.Goals[ii] < 5)
            {
                return 1 + Less(ii - 1);
            }
            else
            {
                return 0 + Less(ii - 1);
            }
        }

        //kiek daugiau uz 5
        public int More(int ii)
        {
            if (ii < 0)
            {
                return 0;
            }
            else if (this.Goals[ii] >= 5)
            {
                return 1 + More(ii - 1);
            }
            else
            {
                return 0 + More(ii - 1);
            }
        }

        /*
        public int CompareTo(Player other)
        {
            int first = other.CalcG(other.GoalsCount() - 1).CompareTo(this.CalcG(this.GoalsCount() - 1));
            int second = this.Surname.CompareTo(other.Surname);

            if (first == 0)
            {
                if (second == 0)
                {
                    return this.Name.CompareTo(other.Name);
                }

                return second;
            }

            return first;
        }*/

        //vietoj compareTo method aprasyti operatoriai
        public static bool operator <(Player one, Player two)
        {
            if (one.CalcG(one.GoalsCount() - 1) < two.CalcG(two.GoalsCount() - 1))
            {
                return true;
            }
            else if (one.CalcG(one.GoalsCount() - 1) == two.CalcG(two.GoalsCount() - 1))
            {
                int first = one.Surname.CompareTo(two.Surname);

                if (first > 0)
                {
                    return true;
                }
                else if (first == 0)
                {
                    int second = one.Name.CompareTo(two.Name);

                    return (second > 0);
                }

                return false;
            }

            return false;
        }

        public static bool operator >(Player one, Player two)
        {
            if (one.CalcG(one.GoalsCount() - 1) > two.CalcG(two.GoalsCount() - 1))
            {
                return true;
            }
            else if (one.CalcG(one.GoalsCount() - 1) == two.CalcG(two.GoalsCount() - 1))
            {
                int first = one.Surname.CompareTo(two.Surname);

                if (first < 0)
                {
                    return true;
                }
                else if (first == 0)
                {
                    int second = one.Name.CompareTo(two.Name);

                    return (second < 0);
                }

                return false;
            }

            return false;
        }

        public override string ToString()
        {
            string line = "";

            line = String.Format($"| {this.Surname,-12} | {this.Name,-12} | {this.CalcG(GoalsCount() - 1),9} |");

            return line;
        }
    }

    internal class Team
    {
        public string TeamName { get; }
        private List<Player> players;

        public Team(string teamName)
        {
            TeamName = teamName;
            this.players = new List<Player>();
        }

        public void AddPlayer(Player player)
        {
            players.Add(player);
        }

        //sito reikia nes pas mane list yra private
        public int PlayerCount()
        {
            return players.Count;
        }

        public Player GetPlayer(int i)
        {
            return players[i];
        }

        //klaida - buvau blogai parases (kaip parasyt removeAt metoda)
        public void Remove(int index)
        {
            if (index >= 0 && index < players.Count())
            {
                players.RemoveAt(index);
            }
        }

        //put metodas (replasina)
        public void Put(Player player, int index)
        {
            if (index >= 0 && index < players.Count())
            {
                this.players[index] = player;
            }
        }

        //insert metodas (iterpia)
        public void Insert(Player player, int index)
        {
            if (index >= 0 && index < players.Count())
            {
                for (int i = players.Count(); i > index; i--)
                {
                    this.players[i] = players[i - 1];
                }

                this.players[index] = player;
            }
        }

        public void Selection()
        {
            for (int i = 0; i < PlayerCount() - 1; i++)
            {
                int m = i;

                for (int j = i + 1; j < PlayerCount(); j++)
                {
                    if (players[m] < players[j])
                    {
                        m = j;
                    }
                }

                Player player = players[i];
                players[i] = players[m];
                players[m] = player;
            }
        }

        //bubble sortas
        public void Bubble()
        {
            bool flag = true;

            while (flag)
            {
                flag = false;

                for (int i = 0; i < players.Count() - 1; i++)
                {
                    Player one = players[i];
                    Player two = players[i + 1];

                    if (one < two)
                    {
                        players[i] = two;
                        players[i + 1] = one;
                        flag = true;
                    }
                }
            }
        }

        public static bool operator >(Team a, Team b)
        {
            return a.PlayerCount() > b.PlayerCount();
        }

        public static bool operator <(Team a, Team b)
        {
            return a.PlayerCount() < b.PlayerCount();
        }

        public static bool operator ==(Team a, Team b)
        {
            return a.PlayerCount() == b.PlayerCount();
        }

        public static bool operator !=(Team a, Team b)
        {
            return a.PlayerCount() != b.PlayerCount();
        }
    }

    internal class InOutUtils
    {
        public static Team TeamRead(string FileName)
        {
            string[] lines = File.ReadAllLines(FileName);

            char[] separators = new char[] { ' ', ',' };

            string teamName = lines[0];

            Team team = new Team(teamName);

            for (int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(separators, StringSplitOptions.RemoveEmptyEntries);

                Player player = new Player(parts[0], parts[1], CollectGoals(parts));

                team.AddPlayer(player);
            }

            return team;
        }

        //reikia method kad nuskaityciau ivarcius
        private static int[] CollectGoals(string[] line)
        {
            int[] goals = new int[line.Length - 2];
            int x = 0;

            for (int i = 2; i < line.Length; i++)
            {
                goals[x] = int.Parse(line[i]);
                x++;
            }

            return goals;
        }

        public static void Print(Team Te, string FileName, string Header)
        {
            File.AppendAllText(FileName, Header, Encoding.UTF8);

            List<string> lines = new List<string>();

            lines.Add("");
            lines.Add(new string('-', 43));
            lines.Add(String.Format($"| {"Pavardė",-12} | {"Vardas",-12} | {"Įvarčiai",-9} |"));
            lines.Add(new string('-', 43));

            for (int i = 0; i < Te.PlayerCount(); i++)
            {
                lines.Add(Te.GetPlayer(i).ToString());
                lines.Add(new string('-', 43));
            }

            lines.Add("");

            File.AppendAllLines(FileName, lines, Encoding.UTF8);
        }
    }

    internal class TaskUtils
    {
        public static void Remove(Team Te)
        {
            for (int i = 0; i < Te.PlayerCount(); i++)
            {
                if (Te.GetPlayer(i).CalcG(Te.GetPlayer(i).GoalsCount() - 1) < Te.GetPlayer(i).GoalsCount())
                {
                    Te.Remove(i);
                }
            }
        }
    }
}
