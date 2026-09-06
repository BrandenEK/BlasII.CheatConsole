using BlasII.CheatConsole.Attributes;
using System;
using System.Linq;
using System.Reflection;

namespace BlasII.CheatConsole.Commands;

/// <summary>
/// A command that only has one action
/// </summary>
public class ModSimpleCommand : ModCommand
{
    private readonly MethodInfo _command;

    public override bool NeedsParameters => false;

    /// <summary>
    /// Creates a new simple command
    /// </summary>
    public ModSimpleCommand(string name) : base(name)
    {
        _command = GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            .FirstOrDefault(x => x.IsDefined(typeof(MainCommandAttribute), false)) ?? throw new InvalidOperationException($"The command {Name} is missing a {nameof(MainCommandAttribute)} method");
    }

    /// <summary>
    /// Executes the main command
    /// </summary>
    public sealed override void Execute(string[] args)
    {
        // Typing no parameters or typing help will list the possible subcommands
        if (args.Length < 1 || args[0].ToLower() == "help")
        {
            DisplayHelp();
            return;
        }

        ParameterInfo[] parameters = _command.GetParameters();

        // Ensure the number of parameters matches the command
        if (args.Length != parameters.Length)
        {
            WriteFailure($"The command '{Name}' expects {parameters.Length} parameters.  You passed {args.Length}.");
            return;
        }

        object[] arguments = new object[parameters.Length];

        // Create and parse the list of parameters
        for (int i = 0; i < parameters.Length; i++)
        {
            string input = args[i];
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

        _command.Invoke(this, arguments);
    }

    private void DisplayHelp()
    {
        Write($"Syntax for {Name}:");

        string command = $"{Name}";
        foreach (var param in _command.GetParameters())
            command += $" {{{param.Name.ToUpper()}}}";

        Write(command);
        Write(string.Empty);
    }
}
