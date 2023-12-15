namespace AppHost;

public class OpenSearchContainerResource(string name) : ContainerResource(name),
    IResourceWithConnectionString
{
    public string? GetConnectionString()
    {
        if (!this.TryGetAllocatedEndPoints(out var allocatedEndpoints))
        {
            throw new DistributedApplicationException("Expected allocated endpoints!");
        }

        var allocatedEndpoint = allocatedEndpoints.Single(x => x.Name == "Rest");

        return $"https://admin:admin@{allocatedEndpoint.Address}:{allocatedEndpoint.Port}";
    }
}