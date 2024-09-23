using Identity.Domain.Interfaces.ContextInterfaces;
using Identity.Domain.Messages;
using System.Text;

public class ProxyMiddleware
{
    private readonly RequestDelegate _next;

    public ProxyMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var originalBodyStream = context.Response.Body;

        using (var responseBody = new MemoryStream())
        {
            context.Response.Body = responseBody;

            try
            {
                await _next(context); // Processar a requisição
            }
            finally
            {
                context.Response.Body.Position = 0;

                // Ler a resposta
                var responseText = await new StreamReader(context.Response.Body).ReadToEndAsync();
                var requestUrl = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}{context.Request.QueryString}";

                StringBuilder headersRequest = new StringBuilder();

                foreach (var item in context.Request.Headers)
                {
                    headersRequest.AppendJoin("|", item);
                }

                context.Response.Body.Position = 0; // Resetar a posição do stream



                // Criar escopo para o repositório
                using (var scope = context.RequestServices.CreateScope())
                {
                    var httpRequestLogRepository = scope.ServiceProvider.GetRequiredService<IHttpRequestLogRepository>();

                    var log = new HttpRequestLog()
                    {
                        Id = Guid.NewGuid(),
                        RequestUrl = requestUrl,
                        RequestMethod = context.Request.Method,
                        ResponseBody = responseText,
                        ResponseStatus = context.Response.StatusCode,
                        ResponseHeaders = headersRequest.ToString()
                    };

                    await httpRequestLogRepository.Add(log);
                }

                // Escrever a resposta de volta ao corpo original
                await responseBody.CopyToAsync(originalBodyStream);
            }
        }
    }
}
