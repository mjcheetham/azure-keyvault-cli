using System;
using System.IO;

namespace Mjcheetham.KeyVaultCommandLine
{
    internal class StandardConsole : IConsole
    {
        #region IConsole

        public TextWriter Out => Console.Out;

        public TextWriter Error => Console.Error;

        public TextReader In => Console.In;

        #endregion
    }
}
