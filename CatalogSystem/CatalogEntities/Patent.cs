using System;
using System.Collections.Generic;
using CatalogSystem.Abstract;

namespace CatalogSystem.CatalogEntities
{
    public class Patent : ICatalogEntity
    {
        public string Name { get; set; }
        public List<Creator> Creators { get; set; }
        public string Country { get; set; }
        public string RegistrationNumber { get; set; }
        public DateTime FilingDate { get; set; }
        public DateTime PublishDate { get; set; }
        public int PagesCount { get; set; }
        public string Note { get; set; }

        public override bool Equals(object obj)
        {
            Patent patent = obj as Patent;
            if (patent == null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (patent.GetType() != typeof(Patent))
            {
                return false;
            }

            return Name == patent.Name;
        }

        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }
    }
}
