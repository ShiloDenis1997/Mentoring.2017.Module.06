using CatalogSystem.Abstract;

namespace CatalogSystem.CatalogEntities
{
    public class Book : ICatalogEntity
    {
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            Book book = obj as Book;
            if (book == null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (book.GetType() != typeof(Book))
            {
                return false;
            }

            return Name == book.Name;
        }

        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }
    }
}
