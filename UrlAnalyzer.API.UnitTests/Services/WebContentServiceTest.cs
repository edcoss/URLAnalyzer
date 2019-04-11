using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using URLAnalyzer.API.Models;
using URLAnalyzer.API.Services;

namespace UrlAnalyzer.API.UnitTests.Services
{
    [TestClass]
    public class WebContentServiceTest
    {
        [TestMethod]
        public void TestServices()
        {
            var mock = new Mock<IContentService>();

            var locationParam = "https://www.sitecore.com";
            var wordfilters = new char[] { ' ', '\n', '\r', '\t', ',', '.', '[', ']', '{', '}', '(', ')', ':', ';', '|', '/', '\\', '<', '>' };

            var returnedImages = new string[] {
                "https://sitecorecdn.azureedge.net/-/media/www/images/identity/sitecore.svg?la=en&amp;mh=50&amp;mw=150&amp;rev=64d07f4f-2201-4cb2-9bdf-c81f786c9efd&amp;date=1931190512&amp;hash=924736E8654E6A9200F6A0621C87AE221EBBBD94",
                "https://sitecorecdn.azureedge.net/-/media/www/images/icons/sitecore-icons-v2/svg/unknown_2.svg?mh=100&amp;mw=100&amp;rev=d708d543-1303-412e-8ca6-e12a454fa7ce&amp;date=1925270822&amp;hash=A0FDA4A094614DEB2A943195444D276E4A8B647B",
                "https://sitecorecdn.azureedge.net/-/media/www/images/customers/loreal-logo-r.png?mh=100&amp;mw=100&amp;rev=4e43be30-b400-4773-bc06-76c03c91b07d&amp;date=1953141107&amp;hash=07C613040A0AF88C9BE11D9C0A94D7BDA9327766",
            };

            var returnedCollection = new Dictionary<string, long>();
            returnedCollection.Add("test1", 10);
            returnedCollection.Add("test2", 5);
            returnedCollection.Add("test3", 8);

            var returnedValue = new PageElements()
            {
                ContentImages = returnedImages,
                ContentWordCollection = returnedCollection
            };

            mock.Setup(svc => svc.GetContentElements(locationParam, wordfilters)).Returns(returnedValue);
            IContentService service = mock.Object;
            var pageElements = service.GetContentElements(locationParam, wordfilters);

            Assert.IsTrue(new List<string>(pageElements.ContentImages).Count == 3);
            Assert.IsTrue(new List<KeyValuePair<string, long>>(pageElements.ContentWordCollection).Count == 3);
            Assert.IsFalse(pageElements.ContentWordCount == 100);
            Assert.IsTrue(pageElements.ContentWordCount == 23);

            mock.Verify(svc => svc.GetContentElements(locationParam, wordfilters), Times.AtLeastOnce());
        }
    }
}
