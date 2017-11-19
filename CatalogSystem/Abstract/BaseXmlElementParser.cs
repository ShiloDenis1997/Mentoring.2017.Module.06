using System;
using System.Globalization;
using System.Xml.Linq;
using CatalogSystem.Exceptions;

namespace CatalogSystem.Abstract
{
    public abstract class BaseXmlElementParser : IElementParser
    {
        public abstract string ElementName { get; }
        public abstract ICatalogEntity ParseElement(XElement element);

        protected string GetAttributeValue(XElement element, string name, bool isMandatory = false)
        {
            var attribute = element.Attribute(name);
            if (string.IsNullOrEmpty(attribute?.Value) && isMandatory)
            {
                throw new MandatoryAttributeMissedException($"{name}");
            }

            return attribute?.Value;
        }

        protected XElement GetElement(XElement parentElement, string name, bool isMandatory = false)
        {
            var element = parentElement.Element(name);
            if (string.IsNullOrEmpty(element?.Value) && isMandatory)
            {
                throw new MandatoryElementMissedException($"{name}");
            }

            return element;
        }

        protected DateTime GetDate(string dateInvariant)
        {
            if (string.IsNullOrEmpty(dateInvariant))
            {
                return default(DateTime);
            }

            return DateTime.ParseExact(dateInvariant, CultureInfo.InvariantCulture.DateTimeFormat.ShortDatePattern,
                CultureInfo.InvariantCulture.DateTimeFormat);
        }
    }
}
