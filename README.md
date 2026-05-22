[![](https://img.shields.io/nuget/v/soenneker.blazor.c15t.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.c15t/)

# Soenneker.Blazor.C15t
### A minimal Blazor wrapper around c15t

## Installation

```bash
dotnet add package Soenneker.Blazor.C15t
```

## Usage

```csharp
services.AddC15t();
```

```razor
<C15tProvider Options="_options">
    @Body
</C15tProvider>

@code {
    private readonly C15tOptions _options = new()
    {
        Mode = "hosted",
        BackendUrl = "https://your-instance.c15t.dev",
        ConsentCategories = ["necessary", "measurement", "marketing"]
    };
}
```

