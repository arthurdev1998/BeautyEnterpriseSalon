using Identity.Domain.Interfaces.ContextInterfaces;
using Identity.Domain.Messages;
using System.Text;

public class ProxyMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IHttpRequestLogRepository _httpRequestLogRepository;

    public ProxyMiddleware(RequestDelegate next, IHttpRequestLogRepository httpRequestLogRepository)
    {
        _next = next;
        _httpRequestLogRepository = httpRequestLogRepository;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var originalBodyStream = context.Response.Body;

        using (var responseBody = new MemoryStream())
        {
            context.Response.Body = responseBody;

            context.Response.Body.Position = 0;

            StringBuilder headersRequest = new StringBuilder();

            foreach (var item in context.Request.Headers)
            {
                headersRequest.AppendJoin("|", item);
            }

            // Ler a resposta
            var responseText = await new StreamReader(context.Response.Body).ReadToEndAsync();
            var requestUrl = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}{context.Request.QueryString}";


            var log = new HttpRequestLog()
            {
                Id = Guid.NewGuid(),
                RequestUrl = requestUrl,
                RequestMethod = context.Request.Method,
                ResponseBody = responseText,
                ResponseStatus = context.Response.StatusCode,
                ResponseHeaders = headersRequest.ToString()
            };

            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                log.ResponseBody = ex.Message;
                log.ResponseStatus = 400;

            }
            finally
            {
                await _httpRequestLogRepository.Add(log);
                await responseBody.CopyToAsync(originalBodyStream);
            }
        }
    }
}
