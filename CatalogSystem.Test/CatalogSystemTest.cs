using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CatalogSystem.Abstract;
using CatalogSystem.CatalogEntities;
using CatalogSystem.ElementParsers;
using CatalogSystem.EntityWriters;
using NUnit.Framework;

namespace CatalogSystem.Test
{
    [TestFixture]
    public class CatalogSystemTest
    {
        private CatalogSystem _catalog;

        [SetUp]
        public void Init()
        {
            _catalog = new CatalogSystem();
            _catalog.AddParsers(new BookElementParser(), new NewsPaperElementParser(), new PatentElementParser());
            _catalog.AddWriters(new BookWriter(), new NewsPaperWriter(), new PatentWriter());
        }

        [Test]
        public void Test_Books_Read()
        {
            TextReader sr = new StringReader(@"<?xml version=""1.0"" encoding=""utf-16""?>" +
                                             "<catalog>" +
                                             GetBookXml() +
                                             "</catalog>");

            IEnumerable<ICatalogEntity> books = _catalog.ReadFrom(sr).ToList();

            CollectionAssert.AreEqual(books, new[]
            {
                CreateBook()
            }, new BooksComparer());

            sr.Dispose();
        }

        [Test]
        public void Test_Papers_Read()
        {
            TextReader sr = new StringReader(@"<?xml version=""1.0"" encoding=""utf-16""?>" +
                                             "<catalog>" +
                                             GetNewspaperXml() +
                                             "</catalog>");

            IEnumerable<ICatalogEntity> newspapers = _catalog.ReadFrom(sr);

            CollectionAssert.AreEqual(newspapers, new[]
            {
                CreateNewspaper()
            }, new NewsPaperComparer());

            sr.Dispose();
        }

        [Test]
        public void Test_Patents_Read()
        {
            TextReader sr = new StringReader(@"<?xml version=""1.0"" encoding=""utf-16""?>" +
                                             "<catalog>" +
                                                GetPatentXml() +
                                             "</catalog>");

            IEnumerable<ICatalogEntity> newspapers = _catalog.ReadFrom(sr);

            CollectionAssert.AreEqual(newspapers, new[]
            {
                CreatePatent()
            }, new PatentComparer());

            sr.Dispose();
        }

        [Test]
        public void Test_MixedEntities_Read()
        {
            TextReader sr = new StringReader(@"<?xml version=""1.0"" encoding=""utf-16""?>" +
                                             "<catalog>" +
                                                GetPatentXml() +
                                                GetBookXml() + 
                                                GetNewspaperXml() +
                                             "</catalog>");

            List<ICatalogEntity> entities = _catalog.ReadFrom(sr).ToList();

            Assert.IsTrue(new PatentComparer().Compare(entities[0], CreatePatent()) == 0);
            Assert.IsTrue(new BooksComparer().Compare(entities[1], CreateBook()) == 0);
            Assert.IsTrue(new NewsPaperComparer().Compare(entities[2], CreateNewspaper()) == 0);

            sr.Dispose();
        }

        [Test]
        public void Test_MixedEntities_Write()
        {
            StringBuilder actualResult = new StringBuilder();
            StringWriter sw = new StringWriter(actualResult);
            var book = CreateBook();
            var newspaper = CreateNewspaper();
            var patent = CreatePatent();
            var entities = new ICatalogEntity[]
            {
                book,
                newspaper,
                patent
            };
            string expectedResult = @"<?xml version=""1.0"" encoding=""utf-16""?>" +
                "<catalog>" +
                    GetBookXml() +
                    GetNewspaperXml() +
                    GetPatentXml() +
                "</catalog>";

            _catalog.WriteTo(sw, entities);
            sw.Dispose();

            Assert.AreEqual(expectedResult, actualResult.ToString());
        }

        private NewsPaper CreateNewspaper()
        {
            return new NewsPaper
            {
                Name = "Times",
                PublicationCity = "London",
                PublisherName = "London typography",
                PublishYear = 1904,
                PagesCount = 14,
                Date = new DateTime(1905, 5, 16),
                IssnNumber = "0317-8471",
                Number = 14,
                Note = "The most popular London newspaper"
            };
        }

        private Patent CreatePatent()
        {
            return new Patent
            {
                Name = "Airplane",
                Country = "USA",
                RegistrationNumber = "D0000126",
                FilingDate = new DateTime(1905, 12, 24),
                PublishDate = new DateTime(1906, 1, 20),
                PagesCount = 100,
                Note = "First airplane in the world",
                Creators = new List<Creator>
                {
                    new Creator {Name = "Orville", Surname = "Wright"},
                    new Creator {Name = "Wilbur", Surname = "Wright"}
                }
            };
        }

        private Book CreateBook()
        {
            return new Book
            {
                Name = "A song of Ice and Fire",
                PublicationCity = "New-York",
                PublisherName = "Typography",
                PublishYear = 1999,
                PagesCount = 500,
                IsbnNumber = "978-1-56619-909-4",
                Note = "This book is about history of Seven Kingdom.",
                Authors = new List<Author>
                {
                    new Author {Name = "George", Surname = "Martin"}
                }
            };
        }

        private string GetBookXml()
        {
            return @"<book name=""A song of Ice and Fire"" " +
                       @"publicationCity=""New-York"" " +
                       @"publisherName=""Typography"" " +
                       @"publishYear=""1999"" " +
                       @"pagesCount=""500"" " +
                       @"isbn=""978-1-56619-909-4"">" +
                       "<note>This book is about history of Seven Kingdom.</note>" +
                       "<authors>" +
                            @"<author name=""George"" surname=""Martin"" />" +
                       "</authors>" +
                   "</book>";
        }

        private string GetNewspaperXml()
        {
            return "<newspaper name=\"Times\" " +
                       "publicationCity=\"London\" " +
                       "publisherName=\"London typography\" " +
                       "publishYear=\"1904\" " +
                       "pagesCount=\"14\" " +
                       "date=\"05/16/1905\" " +
                       "issn=\"0317-8471\" " +
                       "number=\"14\">" +
                       "<note>The most popular London newspaper</note>" +
                   "</newspaper>";
        }

        private string GetPatentXml()
        {
            return "<patent name=\"Airplane\" " +
                       "country=\"USA\" " +
                       "registrationNumber=\"D0000126\" " +
                       "filingDate=\"12/24/1905\" " +
                       "publishDate=\"01/20/1906\" " +
                       "pagesCount=\"100\">" +
                       "<note>First airplane in the world</note>" +
                       "<creators>" +
                            "<creator name=\"Orville\" surname=\"Wright\" />" +
                            "<creator name=\"Wilbur\" surname=\"Wright\" />" +
                       "</creators>" +
                   "</patent>";
        }
    }

    internal class BooksComparer : IComparer, IComparer<Book>
    {
        public int Compare(Book x, Book y)
        {
            return x.PagesCount == y.PagesCount
                   && x.Name == y.Name
                   && x.IsbnNumber == y.IsbnNumber
                   && x.Note == y.Note
                   && x.PagesCount == y.PagesCount
                   && x.PublishYear == y.PublishYear
                   && x.PublicationCity == y.PublicationCity
                   && x.PublisherName == y.PublisherName ? 0 : 1;
        }

        public int Compare(object x, object y)
        {
            return Compare(x as Book, y as Book);
        }
    }

    internal class NewsPaperComparer : IComparer, IComparer<NewsPaper>
    {
        public int Compare(object x, object y)
        {
            return Compare(x as NewsPaper, y as NewsPaper);
        }

        public int Compare(NewsPaper x, NewsPaper y)
        {
            return x.Name == y.Name &&
                   x.PublicationCity == y.PublicationCity &&
                   x.PublisherName == y.PublisherName &&
                   x.PublishYear == y.PublishYear &&
                   x.PagesCount == y.PagesCount &&
                   x.Note == y.Note &&
                   x.Number == y.Number &&
                   x.Date == y.Date &&
                   x.IssnNumber == y.IssnNumber ? 0 : 1;
        }
    }

    internal class PatentComparer : IComparer, IComparer<Patent>
    {
        public int Compare(object x, object y)
        {
            return Compare(x as Patent, y as Patent);
        }

        public int Compare(Patent x, Patent y)
        {
            return x.Name == y.Name &&
                   x.Country == y.Country &&
                   x.RegistrationNumber == y.RegistrationNumber &&
                   x.FilingDate == y.FilingDate &&
                   x.PublishDate == y.PublishDate &&
                   x.PagesCount == y.PagesCount &&
                   x.Note == y.Note ? 0 : 1;
        }
    }
}
