namespace StanbicIBTC.AccountOpening.Domain;

[JsonSerializable(typeof(OutboundLogDto))]
[JsonSourceGenerationOptions(GenerationMode = JsonSourceGenerationMode.Default,
   PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
public partial class OutboundLogDtoJsonContext : JsonSerializerContext
{

}
