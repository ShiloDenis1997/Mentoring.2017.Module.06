using System;
using System.Xml;

namespace CatalogSystem.Abstract
{
    public interface IEntityWriter
    {
        Type TypeToWrite { get; }

        void WriteEntity(XmlWriter xmlWriter, ICatalogEntity entity);
    }
}
