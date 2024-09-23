using Identity.Domain.Messages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infra.Mappings;

public class HttpRequestLogMapping : IEntityTypeConfiguration<HttpRequestLog>
{
    public void Configure(EntityTypeBuilder<HttpRequestLog> builder)
    {
        {
            builder.HasKey(x => x.Id).HasName("id");
            builder.ToTable("endpoint_request_log", "public");

            builder.Property(x => x.Id)
                    .HasColumnName("id");
            builder.Property(x => x.RequestUrl)
                    .HasColumnName("request_url");
            builder.Property(x => x.RequestMethod)
                    .HasColumnName("request_method")
                    .HasMaxLength(250);
            builder.Property(x => x.ResponseStatus)
                    .HasColumnName("response_status");
            builder.Property(x => x.ResponseHeaders)
                    .HasColumnName("response_headers");
            builder.Property(x => x.ResponseBody)
                    .HasColumnName("response_body");
            builder.Property(x => x.ResponseTimestamp)
                    .HasColumnName("response_timestamp");
        }
    }
}