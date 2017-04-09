using System.IO;

namespace Mjcheetham.KeyVaultCommandLine
{
    internal interface IConsole
    {
        TextWriter Out { get; }

        TextWriter Error { get; }

        TextReader In { get; }
    }
}
