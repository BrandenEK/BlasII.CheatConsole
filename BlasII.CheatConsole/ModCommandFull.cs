using BlasII.CheatConsole.Attributes;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BlasII.CheatConsole;

internal class ModCommandFull : ModCommand
{
    private readonly Dictionary<string, MethodInfo> _subcommands;

    public override bool NeedsParameters => false;

    public ModCommandFull(string name) : base(name)
    {
        _subcommands = GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            .Where(x => x.IsDefined(typeof(SubCommandAttribute), false))
            .ToDictionary(x => x.Name.ToLower(), x => x);
    }

    public override void Execute(string[] args)
    {
        // Typing no parameters or typing help will list the possible subcommands
        if (args.Length < 1 || args[0].ToLower() == "help")
        {
            DisplayHelp();
            return;
        }

        Write("Executing command...");
        // Parse the first arg to see what subcommand it is
        // If no valid matches, display error
        // If valid match but wrong params, display error
        // If valid match and right params but fail to parse, display error
        // Otherwise, execute subcommand
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
