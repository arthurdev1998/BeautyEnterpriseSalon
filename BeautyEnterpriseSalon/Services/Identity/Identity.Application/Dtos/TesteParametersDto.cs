using BuildinBlocks.Core.DomainObjects;

namespace Identity.Application.Dtos;

public class TesteParametersDto : PaginationParams
{
    public required int TesteId { get; set; }
}
