using System.Collections.Generic;

namespace Presentation.Web.Models.Student
{
    public class PagedData<T> where T : class
    {
        public IEnumerable<T> Data { get; set; }
        public int NumberOfPages { get; set; }
    }
}
