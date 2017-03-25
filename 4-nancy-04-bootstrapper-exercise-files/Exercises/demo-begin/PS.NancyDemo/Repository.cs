using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PS.NancyDemo
{
    public class Repository
    {
        static IList<Course> _courses;

        public ReadOnlyCollection<Course> Courses
        {
            get
            {
                if (_courses==null)
                    _courses = new List<Course>();
                return new ReadOnlyCollection<Course>(_courses);
            }
        }

        public Course AddCourse(string name, string author)
        {
            return AddCourse(name, author, new string[0]);
        }
        
        public Course AddCourse(string name, string author, string[] topics)
        {
            var course = new Course(_courses.NextId(), name, author);
            topics.ToList().ForEach(course.AddModule);
            _courses.Add(course);
            return course;
        }

        public void AddCourse(Course course)
        {
            course.Id = Courses.NextId();
            course.Modules.ToList().ForEach(m=>m.Id = course.Modules.NextId());
            _courses.Add(course);
        }

        public Course GetCourse(int id)
        {
            return _courses.SingleOrDefault(x => x.Id == id);
        }
    }
}