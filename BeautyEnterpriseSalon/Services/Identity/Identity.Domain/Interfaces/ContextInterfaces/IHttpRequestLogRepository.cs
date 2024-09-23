using Identity.Domain.Messages;

namespace Identity.Domain.Interfaces.ContextInterfaces;

public interface IHttpRequestLogRepository
{
    Task Add(HttpRequestLog log);
}
