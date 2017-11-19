using System;
using CatalogSystem.Abstract;

namespace CatalogSystem.CatalogEntities
{
    public class NewsPaper : ICatalogEntity
    {
        public string Name { get; set; }
        public string PublicationCity { get; set; }
        public string PublisherName { get; set; }
        public int PublishYear { get; set; }
        public int PagesCount { get; set; }
        public string Note { get; set; }
        public int Number { get; set; }
        public DateTime Date { get; set; }
        public string IssnNumber { get; set; }
    }
}
