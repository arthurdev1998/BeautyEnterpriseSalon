using Identity.Domain.Interfaces.ContextInterfaces;
using Identity.Domain.Messages;

namespace Identity.Infra.Data.Repositories;

public class HttpRequestLogRepository : IHttpRequestLogRepository
{
    private readonly IdentityContext _context;

    public HttpRequestLogRepository(IdentityContext context)
    {
        _context = context;        
    }

    public async Task Add(HttpRequestLog log)
    {
        _context.HttpRequestLogs.Add(log);
        await _context.SaveChangesAsync();
    }
}
