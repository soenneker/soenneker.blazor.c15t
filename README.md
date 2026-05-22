[![](https://img.shields.io/nuget/v/soenneker.blazor.c15t.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.c15t/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blazor.c15t/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.blazor.c15t/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.blazor.c15t.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.c15t/)
[![](https://img.shields.io/badge/Demo-Live-blueviolet?style=for-the-badge&logo=github)](https://soenneker.github.io/soenneker.blazor.c15t)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blazor.c15t/codeql.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.blazor.c15t/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Blazor.C15T
### A Blazor interop library for C15T, the consent management platform

## Installation

```bash
dotnet add package Soenneker.Blazor.C15T
```

## Setup

Register services in `Program.cs`:

```csharp
builder.Services.AddC15tAsScoped();
```

Inject the higher-level utility where you need it:

```csharp
@inject IC15t C15T
```

## Usage

Initialize the package once before first use:

```csharp
await C15T.Initialize();
```
