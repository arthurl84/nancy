using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace PS.NancyDemo
{
    [XmlRoot("course")]
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

        [XmlAttribute("id")]
        public int Id { get; set; }
        [XmlElement("name")]
        public string Name { get; set; }
        [XmlAttribute("author")]
        public string Author { get; set; }
        [XmlArray("modules"), XmlArrayItem(typeof(Module), ElementName = "module")]
        public List<Module> Modules { get; set; }

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

        [XmlAttribute("id")]
        public int Id { get; set;}
        [XmlElement("topic")]
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