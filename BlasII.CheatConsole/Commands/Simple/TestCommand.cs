using BlasII.CheatConsole.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlasII.CheatConsole.Commands.Simple;

internal class TestCommand : ModSimpleCommand
{
    public TestCommand() : base("test") { }

    [MainCommand]
    private void Execute(string status, int numTimes)
    {
        Write($"Performing {status} {numTimes} times");
    }
}
