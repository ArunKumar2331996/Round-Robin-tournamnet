using System;
using System.Collections.Generic;
using System.Linq;

namespace Round_Robin_tournamnet
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Program program = new Program();
            Console.WriteLine("enter the number of teams");
            int teamSize = Convert.ToInt32(Console.ReadLine());
            if (teamSize <= 1 || teamSize > 9999)
            {
                Console.WriteLine("You're not really going to play a tournament, you bot!!!");
                return;
            }
            if (teamSize >= 1000)
            {
                Console.WriteLine("Are you really going to conduct a tournament? or testing me?");
            }
            var result = program.fixtures(teamSize);
            int totalCount = result.Count;
            for (int i = 0; i <= totalCount - 1; i += 2)
            {
                var match1 = result[i].getFixture();
                string match2 = "";

                if (i != totalCount - 1)
                {
                    match2 = result[i + 1].getFixture();
                }

                Console.WriteLine(" Todays match" + "[" + match1 + "," + match2 + "]" + "\n");
            }
            Console.ReadKey();
        }

        /// <summary>
        /// Method to decide the fixture based on round robin algorithm.
        /// </summary>
        /// <param name="team"></param>
        /// <returns></returns>
        public List<fixture> fixtures(int team)
        {
            List<fixture> fixture = new List<fixture>();
            List<int> totalNumberOfTeam = Enumerable.Range(1, team).ToList();

            if (totalNumberOfTeam.Count % 2 != 0)
            {
                totalNumberOfTeam.Add(0);
            }

            int halfSize = totalNumberOfTeam.Count / 2;

            List<int> roundRobinRotation = new List<int>();

            roundRobinRotation.AddRange(totalNumberOfTeam.Skip(halfSize).Take(halfSize));
            roundRobinRotation.AddRange(totalNumberOfTeam.Skip(1).Take(halfSize - 1).ToArray().Reverse());

            int teamsSize = roundRobinRotation.Count;

            for (int i = 0; i < totalNumberOfTeam.Count - 1; i++)
            {
                int teamIdx = i % teamsSize;
                fixture.Add(new fixture(roundRobinRotation[teamIdx], totalNumberOfTeam[0]));

                for (int j = 1; j < halfSize; j++)
                {
                    int homeTeam = (i + j) % teamsSize;
                    int awayTeam = (i + teamsSize - j) % teamsSize;
                    fixture.Add(new fixture(roundRobinRotation[homeTeam], roundRobinRotation[awayTeam]));
                }
            }
            return fixture;
        }

        /// <summary>
        /// Returns the fixture in array format.
        /// </summary>
        public class fixture
        {
            int _team1;
            int _team2;

            public fixture(int team1, int team2)
            {
                _team1 = team1;
                _team2 = team2;
            }

            public string getFixture()
            {
                return "[" + _team1 + "," + _team2 + "]";
            }

        }
    }
}
