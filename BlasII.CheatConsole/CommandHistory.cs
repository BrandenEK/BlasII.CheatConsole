using System.Collections.Generic;

namespace BlasII.CheatConsole;

/// <summary>
/// Tracks the history of commands that have been entered
/// </summary>
public class CommandHistory
{
    private readonly List<string> _pastCommands = new List<string>();

    private int _currIdx = -1;

    /// <summary>
    /// Logic whenever a command is entered
    /// </summary>
    public void OnEnterCommand(string command)
    {
        _currIdx = -1;
        _pastCommands.Insert(0, command);

        if (_pastCommands.Count > MAX_COMMANDS)
            _pastCommands.RemoveAt(MAX_COMMANDS);
    }

    /// <summary>
    /// Retrieves the previous command in the history
    /// </summary>
    public string GetPreviousCommand()
    {
        if (_pastCommands.Count == 0)
            return string.Empty;

        if (_currIdx >= _pastCommands.Count - 1)
            return _pastCommands[_pastCommands.Count - 1];

        return _pastCommands[++_currIdx];
    }

    /// <summary>
    /// Retrieves the next command in the history
    /// </summary>
    public string GetNextCommand()
    {
        if (_pastCommands.Count == 0)
            return string.Empty;

        if (_currIdx <= 0)
            return _pastCommands[0];

        return _pastCommands[--_currIdx];
    }

    private const int MAX_COMMANDS = 10;
}
