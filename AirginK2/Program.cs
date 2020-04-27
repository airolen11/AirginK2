// Airgin Aironas GIneika iff-9/5
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirginK2
{
    public interface IParsable
    {
        /// <summary>
        /// Initializes properties of the current instance with the data, extracted from
        /// string type parameter.
        /// </summary>
        /// <param name="data">The data passed as string</param>
        void ParseFromString(string data);
    }

    /// <summary>
    /// Provides properties and interface implementations for the storing of a team data and
    /// manipulating them.
    /// THE STUDENT SHOULD DEFINE THE CLASS ACCORDING THE TASK.
    /// </summary>
    public class Team : IComparable<Team>, IParsable
    {
        public string TeamName { get; set; }
        public int GamesPlayed { get; set; }
        public int GamesWon { get; set; }

        public Team()
        {

        }

        public int CompareTo(Team other)
        {
            return this.GamesWon.CompareTo(other.GamesWon);
        }

        public void ParseFromString(string data)
        {
            string[] args = data.Split(new string[] { "; " }, StringSplitOptions.RemoveEmptyEntries);

            TeamName = args[0];
            GamesPlayed = int.Parse(args[1]);
            GamesWon = int.Parse(args[2]);
        }
        public override string ToString()
        {
            return String.Format($@"{TeamName};{GamesPlayed};{GamesWon};");
        }
    }

    /// <summary>
    /// Provides properties and interface implementations for the storing of a player data and
    /// manipulating them.
    /// THE STUDENT SHOULD DEFINE THE CLASS ACCORDING THE TASK.
    /// </summary>
    public class Player : IComparable<Player>, IParsable, IEquatable<string>, IEquatable<int>
    {
        public string TeamName { get; set; }
        public string FullName { get; set; }
        public int BirthYear { get; set; }
        public string Position { get; set; }
        public int GamesPlayed { get; set; }
        public int TotalPoints { get; set; }

        public Player()
        {

        }
        public int CompareTo(Player other)
        {
            //Points desc, gamesplayed asc
            if(this.TotalPoints == other.TotalPoints)
            {
                return this.GamesPlayed.CompareTo(other.GamesPlayed);
            }
            return 0 - this.TotalPoints.CompareTo(other.TotalPoints);

        }

        public void ParseFromString(string data)
        {
            string[] args = data.Split(new string[] { "; " }, StringSplitOptions.RemoveEmptyEntries);

            TeamName = args[0];
            FullName = args[1];
            BirthYear = int.Parse(args[2]);
            Position = args[3];
            GamesPlayed = int.Parse(args[4]);
            TotalPoints = int.Parse(args[5]);
        }
        public override string ToString()
        {
            return String.Format($@"{TeamName};{FullName};{BirthYear};{Position};{GamesPlayed};{TotalPoints};");
        }

        public bool Equals(string other)
        {
            return Position.ToLower() == other.ToLower();
        }

        public bool Equals(int other)
        {
            return GamesPlayed == other;
        }
    }

    /// <summary>
    /// Provides generic container where the data are stored in the linked list.
    /// THE STUDENT SHOULD APPEND CONSTRAINTS ON TYPE PARAMETER <typeparamref name="T"/>
    /// IF THE IMPLEMENTATION OF ANY METHOD REQUIRES IT.
    /// </summary>
    /// <typeparam name="T">The type of the data to be stored in the list. Data 
    /// class should implement some interfaces.</typeparam>
    public class LinkList<T> where T : IComparable<T>, IParsable
    {
        class Node
        {
            public T Data { get; set; }
            public Node Next { get; set; }
            public Node(T data, Node next)
            {
                Data = data;
                Next = next;
            }
        }

        /// <summary>
        /// All the time should point to the first element of the list.
        /// </summary>
        private Node begin;
        /// <summary>
        /// Shouldn't be used in any other methods except Begin(), Next(), Exist() and Get().
        /// </summary>
        private Node current;

        /// <summary>
        /// Initializes a new instance of the LinkList class with empty list.
        /// </summary>
        public LinkList()
        {
            begin = current = null;
        }
        /// <summary>
        /// One of four interface methods devoted to loop through a list and get value stored in it.
        /// Method should be used to move internal pointer to the first element of the list.
        /// </summary>
        public void Begin()
        {
            current = begin;
        }
        /// <summary>
        /// One of four interface methods devoted to loop through a list and get value stored in it.
        /// Method should be used to move internal pointer to the next element of the list.
        /// </summary>
        public void Next()
        {
            current = current.Next;
        }
        /// <summary>
        /// One of four interface methods devoted to loop through a list and get value stored in it.
        /// Method should be used to check whether the internal pointer points to the element of the list.
        /// </summary>
        /// <returns>true, if the internal pointer points to some element of the list; otherwise,
        /// false.</returns>
        public bool Exist()
        {
            return current != null;
        }
        /// <summary>
        /// One of four interface methods devoted to loop through a list and get value stored in it.
        /// Method should be used to get the value stored in the node pointed by the internal pointer.
        /// </summary>
        /// <returns>the value of the element that is pointed by the internal pointer.</returns>
        public T Get()
        {
            return current.Data;
        }

        /// <summary>
        /// Method appends new node to the beginning of the list and saves in it <paramref name="data"/>
        /// passed by the parameter.
        /// THE STUDENT SHOULD IMPLEMENT THIS METHOD ACCORDING THE TASK.
        /// </summary>
        /// <param name="data">The data to be stored in the list.</param>
        public void Add(T data)
        {
            if(begin == null)
            {
                begin = new Node(data, null);
            }
            Begin();
            while(current.Next != null)
            {
                Next();
            }
            current.Next = new Node(data, null);
        }

        /// <summary>
        /// Method sorts data in the list. The data object class should implement IComparable&lt;T&gt;
        /// interface though defining sort order.
        /// THE STUDENT SHOULD IMPLEMENT THIS METHOD ACCORDING THE TASK.
        /// </summary>
        public void Sort()
        {
            Begin();
            if (!Exist()) return;

            for (Node a = begin; a.Next != null; a = a.Next)
            {
                for (Node b = a.Next; b.Next != null; b = b.Next)
                {
                    if(a.Data.CompareTo(b.Data) == -1)
                    {
                        Node temp = new Node(a.Data, null);
                        a.Data = b.Data;
                        b.Data = temp.Data;
                    }
                }
            }
        }
    }

    public static class InOut
    {
        /// <summary>
        /// Creates the list containing data read from the text file.
        /// THE STUDENT SHOULD IMPLEMENT THIS GENERIC METHOD ACCORDING THE TASK.
        /// THE STUDENT SHOULDN'T CHANGE THE SIGNATURE OF THE METHOD!
        /// </summary>
        /// <typeparam name="T">The type of the data objects in the returning list.</typeparam>
        /// <param name="fileName">The name of the text file</param>
        /// <returns>List with data from file</returns>
        public static LinkList<T> ReadFromFile<T>(string fileName) where T : IComparable<T>, IParsable, new()
        {
            string[] lines = File.ReadAllLines(fileName);
            LinkList<T> list = new LinkList<T>();

            foreach(string line in lines)
            {
                T item = new T();
                item.ParseFromString(line);
                list.Add(item);
            }

            return list;
        }

        /// <summary>
        /// Appends CSV formatted rows from the data contained in the <paramref name="list"/>
        /// to the end of the text file.
        /// THE STUDENT SHOULD IMPLEMENT THIS GENERIC METHOD ACCORDING THE TASK.
        /// THE STUDENT SHOULD APPEND CONSTRAINTS ON TYPE PARAMETER <typeparamref name="T"/>
        /// IF THE IMPLEMENTATION REQUIRES IT.
        /// </summary>
        /// <typeparam name="T">The type of the data objects stored in the list</typeparam>
        /// <param name="fileName">The name of the text file</param>
        /// <param name="list">The list of the data to be stored in the file.</param>
        public static void PrintToFile<T>(string fileName, LinkList<T> list) where T : IParsable, IComparable<T>
        {
            if (!File.Exists(fileName)) File.Create(fileName);

            List<string> lines = new List<string>();

            list.Begin();
                
            for (list.Begin(); list.Exist(); list.Next())
            {
                T one = list.Get();
                lines.Add(one.ToString());
            }

            File.AppendAllLines(fileName, lines);
            
        }
    }

    public static class Task
    {
        /// <summary>
        /// The method finds the largest count of victories in the given list.
        /// THE STUDENT SHOULD IMPLEMENT THIS METHOD ACCORDING THE TASK.
        /// </summary>
        /// <param name="list">The data list to be searched.</param>
        /// <returns>The highest number of victories.</returns>
        public static int MaxVictories(LinkList<Team> list)
        {
            int max = 0;
            for (list.Begin(); list.Exist(); list.Next())
            {
                max = Math.Max(list.Get().GamesWon, max);
            }
            return max;
        }

        /// <summary>
        /// Filters data from the source list that meets filtering criterion and writes them
        /// into the new list.
        /// THE STUDENT SHOULD IMPLEMENT THIS GENERIC METHOD ACCORDING THE TASK.
        /// THE STUDENT SHOULDN'T CHANGE THE SIGNATURE OF THE METHOD!
        /// </summary>
        /// <typeparam name="TData">The type of the data objects stored in the list</typeparam>
        /// <typeparam name="TCriterion">The type of criteria</typeparam>
        /// <param name="source">The source list from which the result would be created</param>
        /// <param name="criterion">Criterion that the object from source list should meet in 
        /// order to fall in result list</param>
        /// <returns>The list that contains filtered data</returns>
        public static LinkList<TData> Filter<TData, TCriterion>(LinkList<TData> source, TCriterion criterion) where TData : IParsable, IComparable<TData>, IEquatable<TCriterion>
        {
            LinkList<TData> newList = new LinkList<TData>();
            for (source.Begin(); source.Exist(); source.Next())
            {
                if (source.Get().Equals(criterion)) newList.Add(source.Get());
            }
            return newList;
        }

    }

    class Program
    {
        /// <summary>
        /// THE STUDENT SHOULD IMPLEMENT THIS METHOD ACCORDING THE TASK.
        /// </summary>
        static void Main()
        {
            string teamPath = "Komandos.txt";
            string playerPath = "Zaidejai.txt";
            string resultPath = "Rezultatai.txt";


            LinkList<Team> teams = InOut.ReadFromFile<Team>(teamPath);
            LinkList<Player> players = InOut.ReadFromFile<Player>(playerPath);

            InOut.PrintToFile(resultPath, teams);
            File.AppendAllText(resultPath, "\n");
            InOut.PrintToFile(resultPath, players);
            File.AppendAllText(resultPath, "\n");

            Console.Write("Enter a position to be filtered by: ");
            string FirstPos = Console.ReadLine();
            Console.Write("Enter another position to be filtered by: ");
            string SecondPos = Console.ReadLine();
            Console.WriteLine();

            LinkList<Player> FirstPosPlayers = Task.Filter<Player, string>(players, FirstPos);
            FirstPosPlayers.Sort();
            LinkList<Player> SecondPosPlayers = Task.Filter<Player, string>(players, SecondPos);
            SecondPosPlayers.Sort();

            Console.WriteLine($"{FirstPos} players");
            Console.WriteLine(new string('-', 50));
            for (FirstPosPlayers.Begin(); FirstPosPlayers.Exist(); FirstPosPlayers.Next())
            {
                Console.WriteLine(FirstPosPlayers.Get());
            }
            Console.WriteLine();

            Console.WriteLine($"{SecondPos} players");
            Console.WriteLine(new string('-', 50));
            for (SecondPosPlayers.Begin(); SecondPosPlayers.Exist(); SecondPosPlayers.Next())
            {
                Console.WriteLine(SecondPosPlayers.Get());
            }
            Console.WriteLine();



            int maxWins = Task.MaxVictories(teams);
            Console.WriteLine("Most victories by any team: " + maxWins);
            File.AppendAllText(resultPath, $"\n{maxWins.ToString()}\n");
            Console.WriteLine();

            LinkList<Player> filtered = Task.Filter<Player, int>(players, maxWins);
            InOut.PrintToFile(resultPath, filtered);
            File.AppendAllText(resultPath, "\n");

            Console.WriteLine("Player's who have same number of games played as maxWins");
            Console.WriteLine(new string('-', 50));
            for (filtered.Begin(); filtered.Exist(); filtered.Next())
            {
                Console.WriteLine(filtered.Get());
            }
            Console.WriteLine();






            Console.ReadLine();
        }
    }
}
