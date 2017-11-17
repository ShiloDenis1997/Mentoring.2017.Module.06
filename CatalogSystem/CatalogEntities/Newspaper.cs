using CatalogSystem.Abstract;

namespace CatalogSystem.CatalogEntities
{
    public class Newspaper : ICatalogEntity
    {
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            Newspaper newspaper = obj as Newspaper;
            if (newspaper == null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (newspaper.GetType() != typeof(Newspaper))
            {
                return false;
            }

            return Name == newspaper.Name;
        }

        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }
    }
}
