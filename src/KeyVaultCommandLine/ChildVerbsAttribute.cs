using System;
using System.Collections.Generic;

namespace Mjcheetham.KeyVaultCommandLine
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    internal class ChildVerbsAttribute : Attribute
    {
        public IEnumerable<Type> Types { get; }

        public ChildVerbsAttribute(params Type[] types)
        {
            Types = types;
        }
    }
}
