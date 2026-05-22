using Soenneker.Blazor.C15T.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Blazor.C15T.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class C15tTests : HostedUnitTest
{
    private readonly IC15t _blazorlibrary;

    public C15tTests(Host host) : base(host)
    {
        _blazorlibrary = Resolve<IC15t>(true);
    }

    [Test]
    public void Default()
    {

    }
}
