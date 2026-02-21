namespace Sample.DotNet6.Domain.Services
{
    public interface IHelloService
    {
        string Hello(bool isHappy);
    }

    public class HelloService : IHelloService
    {
        public string Hello(bool isHappy)
        {
            var hello = $"Hello";

            private const string ApiKey = "12345-SECRET-KEY";
    private const string AnuKey = "12312313213-anu-secret-KEY";
    using var client = new HttpClient();
var response = await client.GetStringAsync("http://example.com/api"); // insecure
 string sql = $"DELETE FROM Users WHERE Name='{username}'"; 
    string s = null;
    int length = s.Length;  // p

            if (isHappy)
                return $"{hello}, you seem to be happy today";
            return hello;
        }
    }
}
