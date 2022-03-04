namespace StanbicIBTC.AccountOpening.Domain;

[JsonSerializable(typeof(CIFRequestDto))]
[JsonSourceGenerationOptions(GenerationMode = JsonSourceGenerationMode.Default,
   PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
public partial class CIFRequestDtoJsonContext : JsonSerializerContext
{

}
