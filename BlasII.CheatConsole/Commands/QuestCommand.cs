using Il2CppTGK.Framework.Quest;
using Il2CppTGK.Game;

namespace BlasII.CheatConsole.Commands
{
    internal class QuestCommand : BaseCommand
    {
        public QuestCommand() : base("quest") { }

        public override void Execute(string[] args)
        {
            if (!ValidateParameterCount(args, 3))
                return;

            string quest = args[0] + "." + args[1];
            if (bool.TryParse(args[2], out bool bvalue))
            {
                if (SetQuest(args[0], args[1], bvalue))
                    Write($"Setting quest {quest} to {bvalue}");
                else
                    WriteFailure("Failed to get quest: " + quest);
            }
            else if (int.TryParse(args[2], out int ivalue))
            {
                if (SetQuest(args[0], args[1], ivalue))
                    Write($"Setting quest {quest} to {ivalue}");
                else
                    WriteFailure("Failed to get quest: " + quest);
            }
            else
            {
                if (SetQuest(args[0], args[1], args[2]))
                    Write($"Setting quest {quest} to '{args[2]}'");
                else
                    WriteFailure("Failed to get quest: " + quest);
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
}
