using System.IO;

namespace Mjcheetham.KeyVaultCommandLine.Services
{
    internal interface IConsole
    {
        TextWriter Out { get; }

        TextWriter Error { get; }

        TextReader In { get; }
    }
}
