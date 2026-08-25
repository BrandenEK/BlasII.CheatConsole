using System;

namespace BlasII.CheatConsole.Attributes;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
internal class SubCommandAttribute : Attribute
{
}
