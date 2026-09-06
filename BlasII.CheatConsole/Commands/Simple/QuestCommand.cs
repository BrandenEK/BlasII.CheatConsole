using BlasII.CheatConsole.Attributes;
using Il2CppTGK.Framework.Quest;
using Il2CppTGK.Game;

namespace BlasII.CheatConsole.Commands.Simple;

internal class QuestCommand : ModSimpleCommand
{
    public QuestCommand() : base("quest") { }

    [MainCommand]
    private void Execute(string quest, string variable, string value)
    {
        string display = quest + "." + variable;
        if (bool.TryParse(value, out bool bvalue))
        {
            if (SetQuest(quest, variable, bvalue))
                Write($"Setting quest {display} to {bvalue}");
            else
                WriteFailure("Failed to get quest: " + display);
        }
        else if (int.TryParse(value, out int ivalue))
        {
            if (SetQuest(quest, variable, ivalue))
                Write($"Setting quest {display} to {ivalue}");
            else
                WriteFailure("Failed to get quest: " + display);
        }
        else
        {
            if (SetQuest(quest, variable, value))
                Write($"Setting quest {display} to '{value}'");
            else
                WriteFailure("Failed to get quest: " + display);
        }
    }

    private bool SetQuest<T>(string quest, string variable, T value)
    {
        InputQuestVar input = CoreCache.Quest.GetInputQuestVar(quest, variable);
        if (input == null || input.questID == 0 || input.varID == 0)
            return false;

        CoreCache.Quest.SetQuestVarValue(input.questID, input.varID, value);
        return true;
    }
}
