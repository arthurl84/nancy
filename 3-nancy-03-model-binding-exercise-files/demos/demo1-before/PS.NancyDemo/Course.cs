using System.Collections.Generic;
using System.Linq;

namespace PS.NancyDemo
{
    public class Repository
    {
        public static IList<Course> Courses = new List<Course>();

        public static Course AddCourse(string name, string author)
        {
            return AddCourse(name, author, new string[0]);
        }
        public static Course AddCourse(string name, string author, string[] topics)
        {
            var course = new Course(Courses.NextId(), name, author);
            topics.ToList().ForEach(course.AddModule);
            Courses.Add(course);
            return course;
        }
        public static void AddCourse(Course course)
        {
            course.Id = Courses.NextId();
            course.Modules.ToList().ForEach(m=>m.Id = course.Modules.NextId());
            Courses.Add(course);
        }

        public static Course GetCourse(int id)
        {
            return Courses.SingleOrDefault(x => x.Id == id);
        }
    }

    public class Course: IEntity
    {
        // Parameterless constructor needed for model binding
        public Course()
        {
            Modules = new List<Module>();
        }

        public Course(int id, string name, string author)
        {
            Id = id;
            Name = name;
            Author = author;
            Modules = new List<Module>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public IList<Module> Modules { get; set; }

        public void AddModule(string topic)
        {
            Modules.Add(new Module(Modules.NextId(), topic));
        }
    }

    public class Module: IEntity
    {
        // Parameterless constructor needed for model binding
        public Module()
        {
        }

        public Module(int id, string topic)
        {
            Id = id;
            Topic = topic;
        }

        public int Id { get; set;}
        public string Topic { get; set;}
    }

    public interface IEntity
    {
        int Id { get; set; }
    }

    public static class Extensions
    {
        public static int NextId<T>(this IList<T> list) where T:IEntity
        {
            return list.Any() ? list.Max(x=>x.Id) + 1 :0;
        }
    }
}