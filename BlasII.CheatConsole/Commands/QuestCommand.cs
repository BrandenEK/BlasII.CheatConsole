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

            if (bool.TryParse(args[2], out bool bvalue))
            {
                Write($"Setting quest {args[0]}.{args[1]} to {bvalue}");
                SetQuest(args[0], args[1], bvalue);
            }
            else if (int.TryParse(args[2], out int ivalue))
            {
                Write($"Setting quest {args[0]}.{args[1]} to {ivalue}");
                SetQuest(args[0], args[1], ivalue);
            }
            else
            {
                Write($"Setting quest {args[0]}.{args[1]} to '{args[2]}'");
                SetQuest(args[0], args[1], args[2]);
            }
        }

        private void SetQuest<T>(string quest, string variable, T value)
        {
            InputQuestVar input = CoreCache.Quest.GetInputQuestVar(quest, variable);
            CoreCache.Quest.SetQuestVarValue(input.questID, input.varID, value);
        }
    }
}
