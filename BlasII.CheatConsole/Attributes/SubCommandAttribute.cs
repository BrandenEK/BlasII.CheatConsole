using System;

namespace BlasII.CheatConsole.Attributes;

//[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
//internal class SubCommandAttribute : Attribute
//{
//    public string Name { get; }

//    public int NumParameters { get; }

//    public SubCommandAttribute(string name, int numParameters)
//    {
//        Name = name;
//        NumParameters = numParameters;
//    }
//}

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
internal class SubCommandAttribute : Attribute
{
}
