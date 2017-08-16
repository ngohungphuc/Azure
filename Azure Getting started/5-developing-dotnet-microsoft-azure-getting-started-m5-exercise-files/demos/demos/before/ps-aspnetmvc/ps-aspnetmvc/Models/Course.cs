using System.Collections.Generic;

namespace PSAspNetMvc.Models
{
    public class Course
    {        
        public string id { get; set; }
        public string Title { get; set; }
        public ICollection<Module> Modules { get; set; }
    }

    public class Module
    {
        public string Title { get; set; }
        public ICollection<Clip> Clips { get; set; }
    }

    public class Clip
    {
        public string Name { get; set; }
        public int Length { get; set; }
    }

    public class SampleData
    {
        public IEnumerable<Course> GetCourses()
        {
            yield return new Course
            {
                Title = "Azure for .NET Developers",
                Modules = new List<Module>
                {
                    new Module
                    {
                        Title = "Introduction to Azure",
                        Clips = new List<Clip>
                        {
                            new Clip { Length=30, Name = "Overview" },
                            new Clip { Length=35, Name = "Azure Portal" },
                            new Clip { Length=40, Name = "More Azure" }
                        }
                    },
                    new Module
                    {
                        Title = "Web Applications in Azure",
                        Clips = new List<Clip>
                        {
                            new Clip { Length=25, Name = "Hello Web!" },
                            new Clip { Length=30, Name = "App Services" },
                            new Clip { Length=55, Name = "Scaling" }
                        }
                    }

                }
            };

            yield return new Course
            {
                Title = "C# Fundamentals",
                Modules = new List<Module>
                {
                    new Module
                    {
                        Title = "Introduction to C#",
                        Clips = new List<Clip>
                        {
                            new Clip { Length=30, Name = "Overview" },
                            new Clip { Length=35, Name = "Making Classes" },
                            new Clip { Length=40, Name = "Modern OOP" }
                        }
                    },
                    new Module
                    {
                        Title = "Crazy C# Compiler Tricks",
                        Clips = new List<Clip>
                        {
                            new Clip { Length=25, Name = "Introduction" },
                            new Clip { Length=30, Name = "Dynamic Compilation" },
                            new Clip { Length=55, Name = "Abstract Syntax Trees" }
                        }
                    }

                }
            };
        }
    }
}