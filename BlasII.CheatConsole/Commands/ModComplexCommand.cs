using BlasII.CheatConsole.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BlasII.CheatConsole.Commands;

/// <summary>
/// A command that can have multiple actions
/// </summary>
public class ModComplexCommand : ModBaseCommand
{
    private readonly Dictionary<string, MethodInfo> _subcommands;

    /// <summary>
    /// Creates a new complex command
    /// </summary>
    public ModComplexCommand(string name) : base(name)
    {
        _subcommands = GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            .Where(x => x.IsDefined(typeof(SubCommandAttribute), false))
            .ToDictionary(x => x.Name.ToLower(), x => x);
    }

    /// <summary>
    /// Executes the proper subcommand
    /// </summary>
    public sealed override void Execute(string[] args)
    {
        // Typing no parameters or typing help will list the possible subcommands
        if (args.Length < 1 || args[0].ToLower() == "help")
        {
            DisplayHelp();
            return;
        }

        // Ensure there is a valid subcommand for the name
        if (!_subcommands.TryGetValue(args[0], out MethodInfo subcommand))
        {
            WriteFailure($"'{args[0]}' is not a valid subcommand.  Enter '{Name} help' to see a list of valid subcommands.");
            return;
        }

        ParameterInfo[] parameters = subcommand.GetParameters();

        // Ensure the number of parameters matches the subcommand
        if (args.Length - 1 != parameters.Length)
        {
            WriteFailure($"The subcommand '{Name} {args[0]}' expects {parameters.Length} parameters.  You passed {args.Length - 1}.");
            return;
        }

        object[] arguments = new object[parameters.Length];

        // Create and parse the list of parameters
        for (int i = 0; i < parameters.Length; i++)
        {
            string input = args[i + 1];
            Type type = parameters[i].ParameterType;

            try
            {
                arguments[i] = ParseParameter(input, type);
            }
            catch
            {
                WriteFailure($"Failed to parse '{input}' to a {type.Name}.");
                return;
            }
        }

        subcommand.Invoke(this, arguments);
    }

    private void DisplayHelp()
    {
        Write($"Possible subcommands for {Name}:");
        foreach (var method in _subcommands.Values)
        {
            string command = $"{Name} {method.Name.ToLower()}";
            foreach (var param in method.GetParameters())
                command += $" {{{param.Name.ToUpper()}}}";

            Write(command);
        }
        Write(string.Empty);
    }
}
