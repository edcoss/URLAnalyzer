using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using URLAnalyzer.API.Controllers;
using URLAnalyzer.API.Repositories;

namespace URLAnalizer.API.UnitTests.Repositories
{
    [TestClass]
    public class WebContentRepositoryTest
    {
        [TestMethod]
        public void TestRepositories()
        {
            var mock = new Mock<IContentRepository>();

            var returnedValue = "< !DOCTYPE html >< html lang = \"en\" xmlns = \"http://www.w3.org/1999/xhtml\" ><head><meta charset=\"utf-8\"/><title>Page Title</title></head><body>Content from a website</body></html>";
            var getContentParam = "https://www.sitecore.com";

            mock.Setup(repo => repo.GetContent(getContentParam)).Returns(returnedValue);
            IContentRepository repository = mock.Object;
            string response = repository.GetContent(getContentParam);

            Assert.IsNotNull(response);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(response));

            mock.Verify(repo => repo.GetContent(getContentParam), Times.AtLeastOnce());
        }
    }
}
