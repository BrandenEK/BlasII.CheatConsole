﻿using BlasII.ModdingAPI.Assets;

namespace BlasII.CheatConsole.Commands.Stats;

internal abstract class BaseValueStatCommand(string name, string statName) : ModCommand(name)
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
        AssetStorage.PlayerStats.AddToCurrentValue(AssetStorage.ValueStats[_statName], amount);
    }
}

internal class TearsCommand : BaseValueStatCommand
{
    public TearsCommand() : base("tears", "Tears") { }
}

internal class MarksCommand : BaseValueStatCommand
{
    public MarksCommand() : base("marks", "Orbs") { }
}
