using BlasII.CheatConsole.Attributes;
using System.Linq;

namespace BlasII.CheatConsole.Commands;

internal class HelpCommand : ModSimpleCommand
{
    public HelpCommand() : base("help") { }

    [MainCommand]
    private void Execute()
    {
        string commands = string.Join(", ", CommandRegister.Commands.Select(x => x.Name));
        Write("Type 'help' after a command to see more detailed help for that specific command");
        Write($"Available commands: {commands}");
    }
}
