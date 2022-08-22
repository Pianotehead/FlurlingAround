using Flurl;
using Flurl.Http;

const string baseUrl = "https://jsonplaceholder.typicode.com";

// Noteworthy, that adding a slash at the end, will not produce two slashes.
// Flurl checks if a slash is present, and adds it if it is not there. =>
// var url = ...jsonplaceholder...com/".AppendPathSegment("posts") would also work;
var url = "https://jsonplaceholder.typicode.com:8080"
    .AppendPathSegment("posts");

Console.WriteLine(url);
Console.WriteLine(url.Scheme);
Console.WriteLine(url.Host);
Console.WriteLine(url.Port);
Console.WriteLine(url.Query);

Console.WriteLine(url.IsRelative);
Console.WriteLine(url.IsSecureScheme);

url.SetQueryParams(new
{
    api_key = "somekey",
    client = "ted.se",
    sometext = "this is some text that get's encoded correctly!"
});

Console.WriteLine(url);

foreach (var (Name, Value) in url.QueryParams)
{
    Console.WriteLine($"Name - {Name}, Value - {Value}");
}

Console.WriteLine("-------------------------------");
Console.WriteLine();
Console.WriteLine("-------------------------------");

url.SetQueryParam("paging", new[] {1, 2, 3});

foreach (var (Name, Value) in url.QueryParams)
{
    Console.WriteLine($"Name - {Name}, Value - {Value}");
}

Console.WriteLine("-------------------------------");
Console.WriteLine("The url has changed a lot.");
Console.WriteLine("-------------------------------");


Console.WriteLine(url);

Console.WriteLine("\n------------------------------------");
Console.WriteLine("Let's try some Flurl.http stuff!!!");
Console.WriteLine("------------------------------------\n");

var result = await baseUrl.AppendPathSegment("posts").GetAsync();
Console.WriteLine(result.StatusCode);

foreach(var (Name, Value) in result.Headers)
{
    Console.WriteLine($"Name - {Name}, Value - {Value}");
}

Console.WriteLine("\n----Print JSON data----\n");

var posts = await result.GetJsonAsync<IEnumerable<Post>> ();

foreach (var post in posts)
{
    Console.WriteLine(post.Title);
}

class Post
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}

