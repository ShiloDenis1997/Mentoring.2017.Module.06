using System.Collections.Generic;
using System.Linq;
using CatalogSystem.Abstract;

namespace CatalogSystem.CatalogEntities
{
    public class Book : ICatalogEntity
    {
        public string Name { get; set; }
        public List<Author> Authors { get; set; }
        public string PublicationCity { get; set; }
        public string PublisherName { get; set; }
        public int PublishYear { get; set; }
        public int PagesCount { get; set; }
        public string Note { get; set; }
        public string IsbnNumber { get; set; }
    }
}
