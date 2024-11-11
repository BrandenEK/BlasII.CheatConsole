using BlasII.ModdingAPI.Assets;

namespace BlasII.CheatConsole.Commands;

internal class ModifyStatCommand(string name, string statName) : ModCommand(name)
{
    private readonly string _statName = statName;

    public override sealed void Execute(string[] args)
    {
        switch (args[0])
        {
            case "add":
                {
                    if (!ValidateParameterCount(args, 2))
                        return;

                    if (!ValidateIntParamater(args[1], out int amount))
                        return;

                    Add(amount);
                    break;
                }
            default:
                {
                    WriteFailure("Unknown subcommand: " + args[0]);
                    break;
                }
        }
    }

    private void Add(int amount)
    {
        Write($"Adding {amount} {Name}");
        AssetStorage.PlayerStats.AddBonus(AssetStorage.ModifiableStats[_statName], "cheat", amount, 0);
    }
}
