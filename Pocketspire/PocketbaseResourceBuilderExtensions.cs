using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;
using System.Reflection;

namespace Pocketspire;

/// <summary>
/// Provides extension methods for adding a Pocketbase container to the Aspire App Host.
/// </summary>
public static class PocketbaseResourceBuilderExtensions
{
    /// <summary>
    /// Adds a Pocketbase container to the Aspire App Host. Creation of a superuser is optional for non-production environments. A link is generated at runtime to create a superuser otherwise.
    /// </summary>
    /// <param name="builder">The distributed application builder.</param>
    /// <param name="superUserEmail">The email of the superuser. Not recommended for production environments.</param>
    /// <param name="superUserPassword">The password of the superuser. Not recommended for production environments.</param>
    /// <param name="name">The name of the Pocketbase resource.</param>
    /// <param name="exposedPort">The port to expose for the Pocketbase container. Leave blank for a random port.</param>
    /// <param name="pocketbaseVersion">The version of Pocketbase to use. Default is "0.24.1".</param>
    /// <returns>Returns an IResourceBuilder of ContainerResource.</returns>
    /// <exception cref="Exception">Thrown when the parent directory cannot be found.</exception>
    public static IResourceBuilder<ContainerResource> AddPocketbaseContainer(
        this IDistributedApplicationBuilder builder,
        string name,
        string? superUserEmail = "",
        string? superUserPassword = "",
        bool? arm64cpu = false,
        int? exposedPort = null,
        string? pocketbaseVersion = "0.24.1")
    {
        var path = new DirectoryInfo(Assembly.GetExecutingAssembly().Location);

        if (path.Parent == null)
            throw new Exception("Could not find parent directory");

        // TODO: ./pb/pocketbase superuser create $PB_SUPERUSER $PB_SUPERUSER_PASSWORD needs to run after serve starts
        return builder.AddDockerfile(name: $"pocketbase-{name}", path.Parent.FullName)
            .WithBuildArg("PB_VERSION", pocketbaseVersion ?? throw new ArgumentNullException(nameof(pocketbaseVersion)))
            .WithBuildArg("CPU_ARCH", arm64cpu.HasValue && arm64cpu.Value ? "arm64" : "amd64")
            .WithArgs("PB_SU", superUserEmail!)
            .WithArgs("PB_SU_PW", superUserPassword!)
            .WithHttpEndpoint(targetPort: 8080, port: exposedPort, name: $"pocketbase-{name}")
            .WithExternalHttpEndpoints();
    }
}
