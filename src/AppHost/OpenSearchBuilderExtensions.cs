using System.Net.Sockets;

namespace AppHost;

public static class OpenSearchBuilderExtensions
{
    public static IResourceBuilder<OpenSearchContainerResource> AddOpenSearchContainer(this IDistributedApplicationBuilder builder, string name, int? port = null, string? password = null)
    {
        return builder.AddResource(new OpenSearchContainerResource(name))
            .WithAnnotation(new ContainerImageAnnotation
            {
                Image = "opensearchproject/opensearch",
                Tag = "2"
            })
            .WithAnnotation(new ServiceBindingAnnotation(ProtocolType.Tcp, port: 9200, containerPort: 9200, name: "Rest"))
            .WithAnnotation(new ServiceBindingAnnotation(ProtocolType.Tcp, port: 9600, containerPort: 9600, name: "Perf"))
            .WithAnnotation(new VolumeMountAnnotation("opensearch-data-1", "/usr/share/opensearch/data", VolumeMountType.Named))
            .WithEnvironment(x => x.EnvironmentVariables.Add("discovery.type", "single-node"));
    }
}