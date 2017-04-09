using System;

namespace Mjcheetham.KeyVaultCommandLine
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    internal class ChildVerbsAttribute : Attribute
    {
        public Type[] Types { get; }

        public ChildVerbsAttribute(params Type[] types)
        {
            Types = types;
        }
    }
}
