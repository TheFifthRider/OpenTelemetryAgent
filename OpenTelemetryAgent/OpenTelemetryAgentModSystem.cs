using Vintagestory.API.Client;
using Vintagestory.API.Server;
using Vintagestory.API.Common;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using Grafana.OpenTelemetry;
using System;
using System.Diagnostics.Metrics;

namespace OpenTelemetryAgent;

public class OpenTelemetryAgentModSystem : ModSystem
{

    // Called on server and client
    // Useful for registering block/entity classes on both sides
    public override void Start(ICoreAPI api)
    {
        Environment.SetEnvironmentVariable("OTEL_RESOURCE_ATTRIBUTES", "service.name=local-vs-server,service.namespace=local-vs-namespace,deployment.environment=production");
        Environment.SetEnvironmentVariable("OTEL_EXPORTER_OTLP_ENDPOINT", "https://otlp-gateway-prod-us-east-2.grafana.net/otlp");
        Environment.SetEnvironmentVariable("OTEL_EXPORTER_OTLP_HEADERS", "Authorization=Basic MTM3NTk3OTpnbGNfZXlKdklqb2lNVFV6TkRJek9DSXNJbTRpT2lKdmRHVnNZV2RsYm5RdGRHOXJaVzRpTENKcklqb2lUV0V6TXpsMGVqWTBhak0xYjJOT09XaEZZVFI2U2poRklpd2liU0k2ZXlKeUlqb2ljSEp2WkMxMWN5MWxZWE4wTFRBaWZYMD0=");
        Environment.SetEnvironmentVariable("OTEL_EXPORTER_OTLP_PROTOCOL", "http/protobuf");
        using var tracerProvider = Sdk.CreateTracerProviderBuilder()
            .UseGrafana()
            .Build();
        using var meterProvider = Sdk.CreateMeterProviderBuilder()
            .UseGrafana()
            .Build();
    }

    public override void StartServerSide(ICoreServerAPI api)
    {
    }

    public override void StartClientSide(ICoreClientAPI api)
    {
    }

}
