using Microsoft.Azure.Documents.Client;
using PSAspNetMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSAspNetMvc.Data
{
    public class CourseStore
    {
        public CourseStore()
        {
            _client = new DocumentClient(new Uri("https://ps-docdb.documents.azure.com:443/"),
                                        "RsATPjsJcpOVDYHETIclY3r3C06LA7i0Dql3SNHbRZnWPJbP9KhYGu1En3NW3FzMS0ymQ23mU3JmCGIzyi1APQ==");
            _coursesLink = UriFactory.CreateDocumentCollectionUri("coursedb", "courses");
        }

        public IEnumerable<Course> GetAllCourses()
        {
            return _client.CreateDocumentQuery<Course>(_coursesLink)
                           .OrderBy(c => c.Title);
        }

        public async Task InsertCourses(IEnumerable<Course> courses)
        {
            foreach (var course in courses)
            {
                await _client.CreateDocumentAsync(_coursesLink, course);
            }
        }

        Uri _coursesLink;
        DocumentClient _client;
    }
}