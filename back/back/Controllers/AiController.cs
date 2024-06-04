using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class AIController : ControllerBase
{
    private static readonly HttpClient client = new HttpClient();

    [HttpPost("generate")]
    public async Task Ai([FromBody] AiRequest request)
    {
        var apiKey = "sk-b1aa0cd7dfde4cdda763efd7b2ca6d91";
        var requestMessage = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://dashscope.aliyuncs.com/api/v1/services/aigc/text-generation/generation"),
            Headers =
            {
                { "Authorization", $"Bearer {apiKey}" },
                { "Accept", "text/event-stream" },
                { "X-DashScope-SSE", "enable" }
            },
            Content = new StringContent($@"{{
                ""model"": ""qwen-turbo"",
                ""input"": {{
                    ""messages"": [
                        {{ ""role"": ""system"", ""content"": ""You are a helpful assistant in Computer Science."" }},
                        {{ ""role"": ""user"", ""content"": ""{request.UserQuestion}"" }}
                    ]
                }},
                ""parameters"": {{
                    ""result_format"": ""message"",
                    ""incremental_output"":true
                }}
            }}")
        };
        requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        using var response = await client.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead);
        await using var stream = await response.Content.ReadAsStreamAsync();

        using var reader = new StreamReader(stream);
        Response.Headers.Add("Content-Type", "text/event-stream");
        Response.Headers.Add("Cache-Control", "no-cache");
        Response.Headers.Add("Connection", "keep-alive");

        while (!reader.EndOfStream)
        {
            var line = await reader.ReadLineAsync();
            if (!string.IsNullOrEmpty(line))
            {
                await Response.WriteAsync($"data: {line}\n\n");
                await Response.Body.FlushAsync();
            }
        }
    }
}

public class AiRequest
{
    public string UserQuestion { get; set; }
}
