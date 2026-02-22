using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace Sample.DotNet6.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class VulnerableController : ControllerBase
{
    // 1. Hardcoded Credentials (S2068)
    private const string DatabasePassword = "superSecretPassword123";

    // 2. Command Injection (S2076)
    [HttpGet("command-injection")]
    public IActionResult CommandInjection(string input)
    {
        var process = new Process();
        process.StartInfo.FileName = "cmd.exe";
        process.StartInfo.Arguments = "/c " + input; // Vulnerable: input is directly concatenated
        process.Start();
        return Ok("Command executed");
    }

    // 3. Path Traversal (S2083)
    [HttpGet("path-traversal")]
    public IActionResult PathTraversal(string filename)
    {
        // Vulnerable: reading file based on user input without sanitization
        var content = System.IO.File.ReadAllText("/var/www/files/" + filename); 
        return Ok(content);
    }

    // 4. Weak Hashing (S2070, S4790)
    [HttpGet("weak-hashing")]
    public IActionResult WeakHashing(string input)
    {
        // Vulnerable: MD5 is weak
        using (var md5 = MD5.Create())
        {
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Ok(Convert.ToBase64String(hash));
        }
    }

    // 5. Insecure Randomness (S2245)
    [HttpGet("insecure-random")]
    public IActionResult InsecureRandom()
    {
        // Vulnerable: System.Random is not cryptographically secure
        var random = new Random();
        return Ok(random.Next());
    }

    // 6. Reflected XSS (S5131)
    [HttpGet("xss")]
    public IActionResult Xss(string input)
    {
        // Vulnerable: User input reflected directly in HTML
        return Content($"<html><body>Hello {input}</body></html>", "text/html");
    }

    // 7. Open Redirect (S6013)
    [HttpGet("open-redirect")]
    public IActionResult OpenRedirect(string url)
    {
        // Vulnerable: Redirecting to user-controlled URL
        return Redirect(url);
    }

    // 8. Simulated SQL Injection (S3649)
    [HttpGet("sql-injection")]
    public IActionResult SqlInjection(string username)
    {
        // Vulnerable: String concatenation for SQL query
        string query = "SELECT * FROM Users WHERE Username = '" + username + "'";
        
        // In a real scenario, this query would be executed against a database.
        // For demonstration, we just return the vulnerable query string.
        return Ok($"Query to be executed: {query}");
    }
}
