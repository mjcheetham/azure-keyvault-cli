using System;
using System.IO;
using Newtonsoft.Json;

namespace Mjcheetham.KeyVaultCommandLine
{
    internal static class TextWriterExtensions
    {
        public static void WriteJson(this TextWriter textWriter, object obj)
        {
            if (textWriter == null)
            {
                throw new ArgumentNullException(nameof(textWriter));
            }

            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            var json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            textWriter.WriteLine(json);
        }
    }
}
