using System;
using System.Linq;
using System.Xml.Linq;
using CatalogSystem.Abstract;
using CatalogSystem.CatalogEntities;

namespace CatalogSystem.ElementParsers
{
    public class PatentElementParser : BaseXmlElementParser
    {
        public override string ElementName => "patent";

        public override ICatalogEntity ParseElement(XElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException($"{nameof(element)} is null");
            }

            Patent patent = new Patent
            {
                Name = GetAttributeValue(element, "name"),
                PagesCount = int.Parse(GetAttributeValue(element, "pagesCount")),
                Note = GetElement(element, "note").Value,
                FilingDate = GetDate(GetAttributeValue(element, "filingDate")),
                Country = GetAttributeValue(element, "country"),
                PublishDate = GetDate(GetAttributeValue(element, "publishDate")),
                RegistrationNumber = GetAttributeValue(element, "registrationNumber"),
                Creators = GetElement(element, "creators").Elements("creator").Select(e => new Creator
                {
                    Name = GetAttributeValue(e, "name"),
                    Surname = GetAttributeValue(e, "surname")
                }).ToList()
            };
            return patent;
        }
    }
}
