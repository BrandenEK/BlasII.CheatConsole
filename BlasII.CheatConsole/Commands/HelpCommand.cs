using System.Linq;

namespace BlasII.CheatConsole.Commands;

internal class HelpCommand : ModCommand
{
    public HelpCommand() : base("help") { }

    public override bool NeedsParameters { get; } = false;

    public override void Execute(string[] args)
    {
        if (!ValidateParameterCount(args, 0))
            return;

        string commands = string.Join(", ", CommandRegister.Commands.Select(x => x.Name));
        Write($"Available commands: {commands}");
    }
}
