using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;

namespace L08_3
{
    public class Department
    {
        public int Id { get; set; }
        public String Name { get; set; }

        public Department(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public override string ToString()
        {
            return $"{Id,2}), {Name,11}";
        }

    }

    public enum Gender
    {
        Female,
        Male
    }

    public class StudentWithTopics
    {
        public int Id { get; set; }
        public int Index { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public bool Active { get; set; }
        public int DepartmentId { get; set; }

        public List<string> Topics { get; set; }
        public StudentWithTopics(int id, int index, string name, Gender gender, bool active,
            int departmentId, List<string> topics)
        {
            this.Id = id;
            this.Index = index;
            this.Name = name;
            this.Gender = gender;
            this.Active = active;
            this.DepartmentId = departmentId;
            this.Topics = topics;
        }

        public override string ToString()
        {
            var result = $"{Id,2}) {Index,5}, {Name,11}, {Gender,6},{(Active ? "active" : "no active"),9},{DepartmentId,2}, topics: ";
            foreach (var str in Topics)
                result += str + ", ";
            return result;
        }
    }

    public static class Generator
    {
        public static int[] GenerateIntsEasy()
        {
            return new int[] { 5, 3, 9, 7, 1, 2, 6, 7, 8 };
        }

        public static int[] GenerateIntsMany()
        {
            var result = new int[10000];
            Random random = new Random();
            for (int i = 0; i < result.Length; i++)
                result[i] = random.Next(1000);
            return result;
        }

        public static List<string> GenerateNamesEasy()
        {
            return new List<string>() {
                "Nowak",
                "Kowalski",
                "Schmidt",
                "Newman",
                "Bandingo",
                "Miniwiliger"
            };
        }
        public static List<StudentWithTopics> GenerateStudentsWithTopicsEasy()
        {
            return new List<StudentWithTopics>() {
            new StudentWithTopics(1,12345,"Nowak", Gender.Female,true,1,
                    new List<string>{"C#","PHP","algorithms"}),
            new StudentWithTopics(2, 13235, "Kowalski", Gender.Male, true,1,
                    new List<string>{"C#","C++","fuzzy logic"}),
            new StudentWithTopics(3, 13444, "Schmidt", Gender.Male, false,2,
                    new List<string>{"Basic","Java"}),
            new StudentWithTopics(4, 14000, "Newman", Gender.Female, false,3,
                    new List<string>{"JavaScript","neural networks"}),
            new StudentWithTopics(5, 14001, "Bandingo", Gender.Male, true,3,
                    new List<string>{"Java","C#"}),
            new StudentWithTopics(6, 14100, "Miniwiliger", Gender.Male, true,2,
                    new List<string>{"algorithms","web programming"}),
            new StudentWithTopics(11,22345,"Nowak", Gender.Female,true,2,
                    new List<string>{"C#","PHP","algorithms"}),
            new StudentWithTopics(12, 23235, "Nowak", Gender.Male, true,1,
                    new List<string>{"C#","C++","fuzzy logic"}),
            new StudentWithTopics(13, 23444, "Schmidt", Gender.Male, false,1,
                    new List<string>{"Basic","Java"}),
            new StudentWithTopics(14, 24000, "Newman", Gender.Female, false,1,
                    new List<string>{"JavaScript","neural networks"}),
            new StudentWithTopics(15, 24001, "Bandingo", Gender.Male, true,3,
                    new List<string>{"Java","C#"}),
            new StudentWithTopics(16, 24100, "Bandingo", Gender.Male, true,2,
                    new List<string>{"algorithms","web programming"}),
            };
        }

        public static List<Department> GenerateDepartmentsEasy()
        {
            return new List<Department>() {
            new Department(1,"Computer Science"),
            new Department(2,"Electronics"),
            new Department(3,"Mathematics"),
            new Department(4,"Mechanics")
            };
        }

    }

    class Topic
    {
        public int Id { private set; get; }
        public string Name { private set; get; }

        Topic(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return $"{Id}".PadLeft(3, '0') + $": {Name}";
        }

        public static List<Topic> GenerateListTopic()
        {
            List<Topic> topics = new List<Topic>();
            foreach(var (id, name) in new (int, string)[] {
                (1, "algorithms"), 
                (2, "C#"),
                (3, "C++"),
                (4, "fuzzy logic"),
                (5, "web programming")
            })
            {
                topics.Add(new Topic(id, name));
            }
            return topics;
        }
    }
    //class StudentToTopic
    //{
    //    private List<KeyValuePair<int, int>> =
    //}
    class Student
    {   public int Id { get; set; }
        public int Index { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public bool Active { get; set; }
        public int DepartmentId { get; set; }

        public List<int> Topics { get; set; }
        public Student(int id, int index, string name, Gender gender, bool active,
            int departmentId, List<int> topics)
        {
            this.Id = id;
            this.Index = index;
            this.Name = name;
            this.Gender = gender;
            this.Active = active;
            this.DepartmentId = departmentId;
            this.Topics = topics;
        }

        public override string ToString()
        {
            var result = $"{Id,2}) {Index,5}, {Name,11}, {Gender,6},{(Active ? "active" : "no active"),9},{DepartmentId,2}, topics: ";
            foreach (var str in Topics)
                result += str + ", ";
            return result;
        }
    }

    class Program
    {
        //ZAD 1 _____________________________________________________________________________________________________
        static List<StudentWithTopics> OrderByNameThenIndex(List<StudentWithTopics> students)
        {
            return students
                .OrderBy(s => s.Name)
                .ThenBy(s => s.Index)
                .ToList();
        }

        static List<List<StudentWithTopics>> GrupIntoSubgrupes(List<StudentWithTopics> students, int sizeOfSubsection)
        {
            List<List<StudentWithTopics>> listOfLists = new List<List<StudentWithTopics>>();
            students.Aggregate(listOfLists,
                (List<List<StudentWithTopics>> a, StudentWithTopics b) => {
                    if (a.Count == 0 || a[^1].Count == sizeOfSubsection)
                    {
                        a.Add(new List<StudentWithTopics>());
                    }
                    a[^1].Add(b);
                    return a;
                }
            );
            return listOfLists;
        }

        static void Zad1()
        {
            List<StudentWithTopics> studentsWithTopics = Generator.GenerateStudentsWithTopicsEasy();
            List<StudentWithTopics> studentsOrdered = OrderByNameThenIndex(studentsWithTopics);

            Console.WriteLine("3 ______________");
            foreach (List<StudentWithTopics> inner in GrupIntoSubgrupes(studentsOrdered, 3))
            {
                inner.ForEach(Console.WriteLine);
                Console.WriteLine("-------------------");
            }

            Console.WriteLine("5 ______________");
            foreach (List<StudentWithTopics> inner in GrupIntoSubgrupes(studentsOrdered, 5))
            {
                inner.ForEach(Console.WriteLine);
                Console.WriteLine("-------------------");
            }
        }

        //ZAD 2 _____________________________________________________________________________________________________
        static List<KeyValuePair<string, int>> TopicsCounter(List<StudentWithTopics> students)
        {
            Dictionary<string, int> dictionaryTopics = new Dictionary<string, int>();
            List<String> allTopicsFlat = students.SelectMany(s => s.Topics).ToList();

            var groups =
                from t in allTopicsFlat
                group t by t into g
                orderby g.Count() descending
                select new KeyValuePair<string, int>(g.Key, g.Count())
                ;
            return groups.ToList();
        }

        static List<StudentWithTopics> AllWithGender(List<StudentWithTopics> students, Gender enm)
        {
            return students.Where(s => s.Gender == enm).ToList();
        }
        static void Zad2()
        {
            List<StudentWithTopics> students = Generator.GenerateStudentsWithTopicsEasy();
            List<StudentWithTopics> males = AllWithGender(students, Gender.Male);
            List<StudentWithTopics> females = AllWithGender(students, Gender.Female);

            Console.WriteLine("all --------------");
            TopicsCounter(students).ForEach(x => Console.WriteLine(x));

            Console.WriteLine("\nmales --------------");
            males.ForEach(Console.WriteLine);
            TopicsCounter(males).ForEach(x => Console.WriteLine(x));

            Console.WriteLine("\nfemales --------------");
            females.ForEach(Console.WriteLine);
            TopicsCounter(females).ForEach(x => Console.WriteLine(x));
        }

        //ZAD 3 _____________________________________________________________________________________________________
        static List<int> StringTopicToId(List<string> topicNames, List<Topic> topics)
        {
            return topicNames
                .SelectMany(topicStr => topics.Where(tTopic => tTopic.Name == topicStr))
                .Select(t => t.Id)
                .ToList();
        }

        static List<Student> FromStudentWithTopic(List<StudentWithTopics> studentsWithTopic, List<Topic> topics)
        {
            return studentsWithTopic
                .Select(s => new Student(s.Id, s.Index, s.Name, s.Gender, s.Active, s.DepartmentId,
                StringTopicToId(s.Topics, topics)
                    ))
                .ToList();
        }

        static void Zad3()
        {
            List<Topic> topics = Topic.GenerateListTopic();
            List<StudentWithTopics> studentsWithTopic = Generator.GenerateStudentsWithTopicsEasy();
            List<Student> translatedToStudent = FromStudentWithTopic(studentsWithTopic, topics);

            studentsWithTopic.ForEach(Console.WriteLine);
            Console.WriteLine("-------------------");
            translatedToStudent.ForEach(Console.WriteLine);

        }
        static void Main(string[] args)
        {
            //Zad1();
            //Zad2();
            Zad3();
        }
    }
}
