using BlasII.CheatConsole.Commands;
using BlasII.CheatConsole.Hitboxes;
using BlasII.ModdingAPI;
using BlasII.ModdingAPI.UI;
using Il2CppTGK.Game;
using Il2CppTMPro;
using System.Collections.Generic;
using UnityEngine;

namespace BlasII.CheatConsole
{
    public class CheatConsole : BlasIIMod
    {
        private readonly Dictionary<string, BaseCommand> _commands = new();

        private RectTransform consoleObject;
        private TextMeshProUGUI consoleText;

        private bool _enabled = false;
        private string _currentText = string.Empty;

        public HitboxViewer HitboxViewer { get; private set; } = new();

        public CheatConsole() : base(ModInfo.MOD_ID, ModInfo.MOD_NAME, ModInfo.MOD_AUTHOR, ModInfo.MOD_VERSION) { }

        public void RegisterCommand(BaseCommand command)
        {
            if (command.Name.Length > 0 && !_commands.ContainsKey(command.Name))
            {
                _commands.Add(command.Name, command);
                Log("Registering new command: " + command.Name);
            }
        }

        protected override void OnInitialize()
        {
            HitboxViewer.Initialize();

            RegisterCommand(new BeadCommand());
            RegisterCommand(new LoadCommand());
        }

        protected override void OnSceneLoaded(string sceneName)
        {
            HitboxViewer.SceneLoaded();
        }

        protected override void OnSceneUnloaded(string sceneName)
        {
            HitboxViewer.SceneUnloaded();

            if (_enabled)
            {
                _enabled = false;
                GetConsoleObject().gameObject.SetActive(false);
            }
        }        

        protected override void OnUpdate()
        {
            HitboxViewer.Update();

            if (_enabled)
            {
                ProcessKeyInput();
            }

            if (Input.GetKeyDown(KeyCode.Backslash) && (!CoreCache.Input.InputBlocked && CoreCache.Room.CurrentRoom != null || _enabled))
            {
                _enabled = !_enabled;
                GetConsoleObject().gameObject.SetActive(_enabled);

                if (_enabled)
                    OnEnable();
                else
                    OnDisable();
            }
        }

        private void OnEnable()
        {
            CoreCache.Input.SetInputBlock(true, false);
            ResetConsole();
        }

        private void OnDisable()
        {
            CoreCache.Input.ClearAllInputBlocks();
        }

        private void ProcessKeyInput()
        {
            foreach (char c in Input.inputString)
            {
                // Backspace
                if (c == '\b')
                {
                    if (_currentText.Length > 0)
                        _currentText = _currentText[..^1];
                }
                // Confirm
                else if (c == '\n' || c == '\r')
                {
                    if (_currentText.Length > 0)
                    {
                        ProcessCommand(_currentText);
                        ResetConsole();
                    }
                }
                // Regular character
                else
                {
                    if (c != ' ' || _currentText.Length > 0)
                        _currentText += c;
                }
            }

            bool hasText = _currentText.Length > 0;

            consoleText.text = hasText ? _currentText : "Begin typing a command...";
            consoleText.fontStyle = hasText ? FontStyles.Normal : FontStyles.Italic;
            consoleText.color = hasText ? Color.white : Color.gray;
        }

        private void ResetConsole()
        {
            _currentText = string.Empty;
        }

        private void ProcessCommand(string command)
        {
            LogCustom("[CONSOLE] " + command, System.Drawing.Color.White);
            string[] parts = command.Trim().ToLower().Split(' ');

            if (parts.Length < 1)
                parts = new string[] { string.Empty };
            else if (parts.Length < 2)
                parts = new string[] { parts[0], string.Empty };

            if (!_commands.ContainsKey(parts[0]))
            {
                LogError($"[CONSOLE] Command '{parts[0]}' is not a valid command!");
                return;
            }

            _commands[parts[0]].Execute(parts[1], parts[2..]);
        }

        private GameObject GetConsoleObject()
        {
            // If console object already exists, return it
            if (consoleObject != null)
                return consoleObject.gameObject;

            // Create console background
            consoleObject = UIModder.CreateRect("CheatConsole")
                .SetXRange(Vector2.zero).SetYRange(Vector2.zero)
                .SetSize(800, 50)
                .SetPivot(Vector2.zero)
                .AddImage()
                .SetColor(new Color(0.15f, 0.15f, 0.15f, 0.9f))
                .rectTransform;

            // Create console text
            consoleText = UIModder.CreateRect("Text", consoleObject)
                .SetPosition(10, 0)
                .SetSize(790, 50)
                .AddText()
                .SetFontSize(28)
                .SetAlignment(TextAlignmentOptions.Left)
                .SetFont(UIModder.Fonts.Arial);

            consoleObject.gameObject.SetActive(false);
            return consoleObject.gameObject;
        }
    }
}
