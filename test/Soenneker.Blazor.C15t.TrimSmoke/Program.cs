using System.Text.Json;
using Soenneker.Blazor.C15t;

var state = JsonSerializer.Deserialize("""{"displayedConsents":[{"name":"analytics","description":"Measure usage","disabled":false,"display":true,"defaultValue":false,"extra":7}],"consents":{"analytics":true},"extraState":"kept"}""", InteropJsonContext.Default.C15tConsentState)!;
Check(state.DisplayedConsents is [{ Name: "analytics", Description: "Measure usage", Display: true }], "nested consent categories");
Check(state.DisplayedConsents![0].ExtensionData!["extra"].GetInt32() == 7, "category extension data");
Check(state.Consents!["analytics"] && state.ExtensionData!["extraState"].GetString() == "kept", "consent state");
var options = JsonSerializer.Deserialize("""{"backendURL":"https://consent.test","consentCategories":["analytics"],"custom":true}""", InteropJsonContext.Default.C15tOptions)!;
var json = JsonSerializer.SerializeToElement(options, InteropJsonContext.Default.C15tOptions);
Check(json.GetProperty("backendURL").GetString() == "https://consent.test" && json.GetProperty("custom").GetBoolean(), "option wire names and extension data");
Check(JsonSerializer.Deserialize("null", InteropJsonContext.Default.C15tConsentState) is null, "null state");

var module = new SmokeModule("""{"displayedConsents":[{"name":"analytics"}]}""");
await using var interop = new C15tInterop(new SmokeModuleImport(module));
var initialized = await interop.Initialize(options);
Check(initialized?.DisplayedConsents is [{ Name: "analytics" }], "initialization boundary");
Check(module.Arguments is [JsonElement], "options cross JS as generated JSON");
Check((await interop.GetState())?.DisplayedConsents is [{ Name: "analytics" }], "state boundary");

Console.WriteLine("Trimmed JSON smoke checks passed.");

static void Check(bool condition, string name)
{
    if (!condition) throw new InvalidOperationException(name);
}
