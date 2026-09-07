using System;

namespace BlasII.CheatConsole.Attributes;

/// <summary>
/// Marks this method as the main command
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
public class MainCommandAttribute : Attribute
{
}
