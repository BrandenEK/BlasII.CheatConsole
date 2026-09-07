using BlasII.CheatConsole.Attributes;

namespace BlasII.CheatConsole.Commands.Complex;

internal class ServantCommand : ModComplexCommand
{
    public ServantCommand() : base("servant") { }

    [SubCommand]
    private void List()
    {
        // List all servant ids and level and xp
    }

    [SubCommand]
    private void Unlock(string id)
    {
        // Unlocks the servant id
    }

    [SubCommand]
    private void Lock(string id)
    {
        // Locks the servant id
    }

    [SubCommand]
    private void Activate(string id)
    {
        // Activates the servant id
    }

    [SubCommand]
    private void GetXP()
    {
        // Prints the current xp
    }

    [SubCommand]
    private void AddXP(int xp)
    {
        // Adds to xp of current servant
    }
}
