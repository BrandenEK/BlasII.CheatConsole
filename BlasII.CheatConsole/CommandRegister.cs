using BlasII.ModdingAPI;
using System.Collections.Generic;
using System.Linq;

namespace BlasII.CheatConsole;

/// <summary>
/// Registers a new console command
/// </summary>
public static class CommandRegister
{
    private static readonly List<ModCommand> _commands = [];
    internal static IEnumerable<ModCommand> Commands => _commands;

    /// <summary> Registers a new console command </summary>
    public static void RegisterCommand(this ModServiceProvider provider, ModCommand command)
    {
        if (provider == null)
            return;

        if (string.IsNullOrEmpty(command.Name) || _commands.Any(cmd => cmd.Name == command.Name))
            return;

        _commands.Add(command);
        ModLog.Info($"Registered command: {command.Name}");
    }
}
