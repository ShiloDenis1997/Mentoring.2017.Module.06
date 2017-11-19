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

        public override bool Equals(object obj)
        {
            NewsPaper newsPaper = obj as NewsPaper;
            if (newsPaper == null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (newsPaper.GetType() != typeof(NewsPaper))
            {
                return false;
            }

            return Name == newsPaper.Name;
        }

        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }
    }
}
