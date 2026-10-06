using System.Text.Json;
using System.Text.Json.Serialization;
using Soenneker.Blazor.C15t.Models;

namespace Soenneker.Blazor.C15t;

[JsonSourceGenerationOptions(JsonSerializerDefaults.Web)]
[JsonSerializable(typeof(C15tConsentState))]
[JsonSerializable(typeof(C15tOptions))]
internal partial class InteropJsonContext : JsonSerializerContext;
