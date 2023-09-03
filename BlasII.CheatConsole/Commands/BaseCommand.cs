using System;
using System.Collections.Generic;

namespace BlasII.CheatConsole.Commands
{
    public abstract class BaseCommand
    {
        private readonly string _name;
        private Dictionary<string, Action<string[]>> _commands;

        public BaseCommand(string name) => _name = name;

        public string Name => _name;

        internal void Execute(string subcommand, string[] parameters)
        {
            _commands ??= RegisterSubcommands();

            if (_commands.ContainsKey(subcommand))
            {
                _commands[subcommand](parameters);
            }
            else
            {
                Main.CheatConsole.LogError($"Subcommand '{subcommand}' does not exist for command '{Name}'!");
            }
        }

        protected bool ValidateParameterCount(string subcommand, string[] paramaters, int num)
        {
            bool isValid = paramaters.Length == num;

            if (!isValid)
                Main.CheatConsole.LogError($"Subcommand '{subcommand}' requires {num} parameters!");

            return isValid;
        }

        protected bool ValidateIntParamater(string parameter, out int result)
        {
            bool isValid = int.TryParse(parameter, out result);

            if (!isValid)
                Main.CheatConsole.LogError($"Parameter '{parameter}' is not a valid integer!");

            return isValid;
        }

        protected abstract Dictionary<string, Action<string[]>> RegisterSubcommands();
    }
}
