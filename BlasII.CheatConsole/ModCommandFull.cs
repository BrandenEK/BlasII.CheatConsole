using BlasII.CheatConsole.Attributes;
using BlasII.ModdingAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BlasII.CheatConsole;

internal class ModCommandFull : ModCommand
{
    private readonly Dictionary<string, MethodInfo> _subcommands;

    public ModCommandFull(string name) : base(name)
    {
        // Use reflection to cache the subcommands

        _subcommands = GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            .Where(x => x.IsDefined(typeof(SubCommandAttribute), false))
            .ToDictionary(x => x.Name.ToLower(), x => x);

        foreach (var method in _subcommands.Values)
        {
            //ModLog.Warn(kvp.Key);
            //ModLog.Warn(kvp.Value.GetParameters().Length);
            //foreach (var param in kvp.Value.GetParameters())
            //{
            //    ModLog.Error(param.Name);
            //    ModLog.Error(param.ParameterType.Name);
            //}

            string command = $"weapon {method.Name.ToLower()}";
            foreach (var param in method.GetParameters())
            {
                command += $" {{{param.Name.ToUpper()}}}";
            }
            ModLog.Warn(command);
        }
    }

    public override void Execute(string[] args)
    {
        // help or no parameters will list the valid descriptions

        throw new NotImplementedException();
    }
}
