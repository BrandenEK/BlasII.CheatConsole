using System;

namespace BlasII.CheatConsole.Attributes;

/// <summary>
/// Marks this method as a subcommand
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
public class SubCommandAttribute : Attribute
{
}
