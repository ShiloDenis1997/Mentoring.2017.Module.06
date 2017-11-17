using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatalogSystem.Abstract;

namespace CatalogSystem.CatalogEntities
{
    public class Patent : ICatalogEntity
    {
        public string Name { get; set; }

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
