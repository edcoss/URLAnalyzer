# URLAnalyzer
Exercise to scrap web content

![alt text](https://github.com/edcoss1/URLAnalyzer/blob/master/Setup/urlanalyzer.local_.png "Screenshot")

# Setup

1. Clone or download the repo
2. Go to your setup folder and rdit websiteAPI.xml and website.xml, to change the virtual directory's physicalPath attribute
3. Use appcmd to import IIS settings

%windir%\system32\inetsrv\appcmd add apppool /in < [DRIVE]:\[YOUR_FOLDER]\Setup\websiteAPIAppPool.xml
%windir%\system32\inetsrv\appcmd add apppool /in < [DRIVE]:\[YOUR_FOLDER]\websiteAppPool.xml
%windir%\system32\inetsrv\appcmd add site /in < [DRIVE]:\[YOUR_FOLDER]\websiteAPI.xml
%windir%\system32\inetsrv\appcmd add site /in < [DRIVE]:\[YOUR_FOLDER]\website.xml

3. Open VS solution (created on 2017)
4. Restore Nuget packages
5. Publish to your webroot folder, same path you used for website.xml and websiteAPI.xml

Test website:
1. Open up a browser, and hit the url: http://urlanalyzer.local to bring up the page
2. Provide a website URL and check the word count, the images included in that page, and a graph with most used words

Test API:
1. Open up a browser or Postman tool
2. Use the following url, either in POST or GET: http://api.urlanalyzer.local/api/content?url=https://www.sitecore.com
