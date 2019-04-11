using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using URLAnalyzer.API.Models;
using URLAnalyzer.API.Repositories;

namespace URLAnalyzer.API.Services
{
    public class WebContentService : IContentService
    {
        IContentRepository contentRepository;
        public WebContentService(IContentRepository contentRepository)
        {
            this.contentRepository = contentRepository;
        }
        public PageElements GetContentElements(string location, char[] filters)
        {
            var model = new PageElements();
            // call repo to get HTML contemt, in this case thru an HTTP client
            var content = contentRepository.GetContent(location);

            var document = new HtmlDocument();

            if (!string.IsNullOrWhiteSpace(content))
            {
                document.LoadHtml(content);
                var mainNode = document.DocumentNode;

                // call helper methods to traverse thru the HTML structure
                model.ContentImages = GetContentImageData(mainNode, location);
                model.ContentWordCollection = GetContentWordData(mainNode, filters);
            }
            return model;
        }

        public IEnumerable<string> GetContentImageData(HtmlNode node, string baseUrl)
        {
            var imageSrcList = new List<string>();
            var imageNodes = node.SelectNodes("//img");
            if (imageNodes != null && imageNodes.Count > 0)
            {
                imageNodes.Select(img => img.Attributes["src"]).ToList()
                    .ForEach(attribute => {
                        if (attribute != null && !string.IsNullOrWhiteSpace(attribute.Value))
                        {
                            // when img src attributes contains full qualified domain name
                            if (attribute.Value.ToLowerInvariant().Contains(baseUrl.ToLowerInvariant()))
                            {
                                imageSrcList.Add(attribute.Value);
                            }
                            else // when img src is a relative url
                            {
                                var fullUrl = new Uri(new Uri(baseUrl), attribute.Value);
                                imageSrcList.Add(fullUrl.AbsoluteUri);
                            }
                        }
                    });
            }
            return imageSrcList;
        }

        public IEnumerable<KeyValuePair<string, long>> GetContentWordData(HtmlNode node, char[] filters)
        {
            // Get only text nodes, to get text from inside of tags

            var textNodes = node.SelectNodes("//text()").Where(t => !string.IsNullOrWhiteSpace(t.InnerText)).Select(t => t);
            var collection = new Dictionary<string, long>();
            foreach (HtmlNode textNode in textNodes)
            {
                if (textNode.ParentNode.Name.ToLowerInvariant().Contains("script"))
                {
                    // skip all text nodes inside an script tag
                    continue;
                }

                // split individual words from text, and remove special characters
                var allWords = textNode.InnerText.Split(filters, StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in allWords)
                {
                    var lowercaseWord = word.ToLowerInvariant();
                    if (collection.ContainsKey(lowercaseWord))
                    {
                        // increasing counter for a given word
                        collection[lowercaseWord]++;
                    }
                    else
                    {
                        // first time in collection
                        collection.Add(lowercaseWord, 1);
                    }
                }
            }
            return collection.ToList();
        }
    }
}