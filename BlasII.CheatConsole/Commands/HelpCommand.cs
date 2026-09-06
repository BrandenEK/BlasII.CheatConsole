using System.Linq;

namespace BlasII.CheatConsole.Commands;

internal class HelpCommand : ModCommand
{
    public HelpCommand() : base("help") { }

    public override bool NeedsParameters => false;

    public override void Execute(string[] args)
    {
        string commands = string.Join(", ", CommandRegister.Commands.Select(x => x.Name));
        Write("Type 'help' after a command to see more detailed help for that specific command");
        Write($"Available commands: {commands}");
    }
}
