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

    //ZAD 3 _____________________________________________________________________________________________________
    class Topic
    {
        public int Id { private set; get; }
        public string Name { private set; get; }

        public Topic(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return $"{Id}".PadLeft(2, '0') + $": {Name}";
        }
    }

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
        static List<string> TopicsByMostPopular(List<StudentWithTopics> students)
        {
            return (
                from t in students.SelectMany(s => s.Topics)
                group t by t into g
                orderby g.Count() descending
                select g.Key
                ).ToList();
        }

        static List<StudentWithTopics> AllWithGender(List<StudentWithTopics> students, Gender enm)
        {
            return students.Where(s => s.Gender == enm).ToList();
        }

        static IEnumerable<(Gender, IEnumerable<StudentWithTopics>)> GetGenderedList(List<StudentWithTopics> students)
        {
            var gendered = from s in students group s by s.Gender into Temp select (Temp.Key, from s in students where s.Gender == Temp.Key select s);
            return gendered;
        }
        static void Zad2()
        {
            List<StudentWithTopics> students = Generator.GenerateStudentsWithTopicsEasy();
            List<StudentWithTopics> males = AllWithGender(students, Gender.Male);
            List<StudentWithTopics> females = AllWithGender(students, Gender.Female);

            Console.WriteLine("all --------------");
            TopicsByMostPopular(students).ForEach(Console.WriteLine);

            //Console.WriteLine("\nfemales --------------");
            //females.ForEach(Console.WriteLine);
            //TopicsByMostPopular(females).ForEach(Console.WriteLine);

            //Console.WriteLine("\nmales --------------");
            //males.ForEach(Console.WriteLine);
            //TopicsByMostPopular(males).ForEach(Console.WriteLine);

            IEnumerable<(Gender, IEnumerable<StudentWithTopics>)> gendered = GetGenderedList(students);
            var genderedTopics = gendered.Select(el => (el.Item1, TopicsByMostPopular(el.Item2.ToList())));
            //IEnumerable<Tuple<Gender, IEnumerable<Topic>>> genderedTopics = gendered.Select(el => (el.Key, TopicsByMostPopular(gendered.SelectMany(el => el).ToList())));

            foreach (var gender in genderedTopics)
            {
                Console.WriteLine("\n--------------");
                Console.WriteLine(gender.Item1);
                gender.Item2.ForEach(Console.WriteLine);
            }
        }

        //ZAD 3 _____________________________________________________________________________________________________
        // a - generacja listy tematów poprzez zapytanie
        static IEnumerable<Topic> GetAllTopics(List<StudentWithTopics> studentsWithTopic)
        {
            return (
                from t in studentsWithTopic.SelectMany(s => s.Topics)
                group t by t into g
                select g.Key
                )
                .Select((topicString, possition) => new Topic(possition, topicString))
                ;
        }

        static IEnumerable<int> StringTopicToId(List<string> topicNames, IEnumerable<Topic> topics)
        {
            return topicNames
                .SelectMany(topicStr => topics.Where(tTopic => tTopic.Name == topicStr))
                .Select(t => t.Id)
                ;
        }
        
        static List<Student> FromStudentWithTopic(List<StudentWithTopics> studentsWithTopic, List<Topic> topics)
        {
            return studentsWithTopic
                .Select(s => new Student(s.Id, s.Index, s.Name, s.Gender, s.Active, s.DepartmentId,
                StringTopicToId(s.Topics, topics).ToList()
                    ))
                .ToList();
        }
        // b - generacja listy tematów i listy nowego typu studentów w jednym zapytaniu
        static (List<Student>, List<Topic>) FromStudentWithTopic(List<StudentWithTopics> studentsWithTopic)
        {
            return studentsWithTopic.Aggregate((
                students: new List<Student>(), 
                topics: studentsWithTopic.SelectMany(s => s.Topics).Distinct().Select((topicString, possition) => new Topic(possition, topicString)).ToList()),

                (tupp, s) =>
                {
                    tupp.students.Add(new Student(s.Id, s.Index, s.Name, s.Gender, s.Active, s.DepartmentId, 
                        s.Topics.SelectMany(topicStr => tupp.topics.Where(tTopic => tTopic.Name == topicStr))
                        .Select(t => t.Id)
                        .ToList()));
                    return tupp;
                }
                );
        }

        static void Zad3()
        {
            List<StudentWithTopics> studentsWithTopic = Generator.GenerateStudentsWithTopicsEasy();
            studentsWithTopic.ForEach(Console.WriteLine);

            //a.  Dla chętnych – generacja listy tematów poprzez zapytanie 
            List<Topic> topics = GetAllTopics(studentsWithTopic).ToList();
            List<Student> translatedToStudent = FromStudentWithTopic(studentsWithTopic, topics);

            Console.WriteLine("-------------------");
            topics.ForEach(Console.WriteLine);
            Console.WriteLine("-------------------");
            translatedToStudent.ForEach(Console.WriteLine);

            //b.Dla chętnych – generacja listy tematów i listy nowego typu studentów w jednym zapytaniu.
            var (translatedToStudent2, topics2) = FromStudentWithTopic(studentsWithTopic);
            Console.WriteLine("-------------------");
            topics2.ForEach(Console.WriteLine);
            Console.WriteLine("-------------------");
            translatedToStudent2.ForEach(Console.WriteLine);
        }

        //ZAD 3c _____________________________________________________________________________________________________
        class StudentExtraExcercise
        {
            public int Id { get; set; }
            public int Index { get; set; }
            public string Name { get; set; }
            public Gender Gender { get; set; }
            public bool Active { get; set; }
            public int DepartmentId { get; set; }

            public StudentExtraExcercise(int id, int index, string name, Gender gender, bool active,
                int departmentId)
            {
                this.Id = id;
                this.Index = index;
                this.Name = name;
                this.Gender = gender;
                this.Active = active;
                this.DepartmentId = departmentId;
            }

            public override string ToString()
            {
                var result = $"{Id,2}) {Index,5}, {Name,11}, {Gender,6},{(Active ? "active" : "no active"),9},{DepartmentId,2}";
                return result;
            }
        }

        class StudentToTopic
        {
            public int StudentId { get; set; }
            public int TopicId { get; set; }

            public StudentToTopic(int studentId, int topicId)
            {
                StudentId = studentId;
                TopicId = topicId;
            }

            public override string ToString()
            {
                var result = $"Student: {StudentId}; Topic: {TopicId}";
                return result;
            }
        }

        static (List<StudentExtraExcercise>, List<StudentToTopic>) GetAllStudentExtraExcercise(List<StudentWithTopics> studentsWithTopic, List<Topic> topics)
        {
            return studentsWithTopic.Aggregate((
                students: new List<StudentExtraExcercise>(),
                topics: new List<StudentToTopic>()),

                (tupp, s) =>
                {
                    tupp.students.Add(new StudentExtraExcercise(s.Id, s.Index, s.Name, s.Gender, s.Active, s.DepartmentId));
                    s.Topics.ForEach(x => tupp.topics.Add(new StudentToTopic(tupp.students[^1].Id, topics.Where(t => t.Name == x).Select(t => t.Id).ToList()[0])));
                    
                    return tupp;
                }
                );
            //return studentWithTopics.Select(s => new StudentExtraExcercise(s.Id, s.Index, s.Name, s.Gender, s.Active, s.DepartmentId)).ToList();
        }

        static void zad3c() {
            List<StudentWithTopics> studentsWithTopic = Generator.GenerateStudentsWithTopicsEasy();
            List<Topic> topics = GetAllTopics(studentsWithTopic).ToList();
            var (studentExtraExcercises, studentToTopicss) = GetAllStudentExtraExcercise(studentsWithTopic, topics);

            studentExtraExcercises.ForEach(Console.WriteLine);
            studentToTopicss.ForEach(Console.WriteLine);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("\nZAD 1 -----------------------------------------------------------------------------------------------------");
            Zad1();
            Console.WriteLine("\nZAD 2 -----------------------------------------------------------------------------------------------------");
            Zad2();
            Console.WriteLine("\nZAD 3 -----------------------------------------------------------------------------------------------------");
            Zad3();
            Console.WriteLine("\nZAD 3c -----------------------------------------------------------------------------------------------------");
            zad3c();

            //List<StudentWithTopics> studentsWithTopic = Generator.GenerateStudentsWithTopicsEasy();
            //List<Topic> topics = 
            //studentsWithTopic.SelectMany(s => s.Topics).Distinct().Select((topicString, possition) => new Topic(possition, topicString)).ToList();
            //topics.ForEach(Console.WriteLine);
        }
    }
}
 