using BlasII.ModdingAPI;
using BlasII.ModdingAPI.UI;
using Il2CppTGK.Game;
using Il2CppTMPro;
using UnityEngine;

namespace BlasII.CheatConsole
{
    public class CheatConsole : BlasIIMod
    {
        public CheatConsole() : base(ModInfo.MOD_ID, ModInfo.MOD_NAME, ModInfo.MOD_AUTHOR, ModInfo.MOD_VERSION) { }

        protected override void OnInitialize()
        {
            LogError($"{ModInfo.MOD_NAME} is initialized");
        }

        protected override void OnSceneLoaded(string sceneName)
        {
            if (consoleObject == null && sceneName == "MainMenu")
                CreateConsoleUI();
        }

        RectTransform consoleObject;
        TextMeshProUGUI consoleText;
        private bool _enabled = false;

        private void CreateConsoleUI()
        {
            consoleObject = UIModder.CreateRect("CheatConsole")
                .SetXRange(Vector2.zero).SetYRange(Vector2.zero)
                .SetSize(800, 50)
                .SetPivot(Vector2.zero)
                .AddImage()
                .SetColor(new Color(0.15f, 0.15f, 0.15f, 0.9f))
                .rectTransform;

            TMP_FontAsset font = TMP_FontAsset.CreateFontAsset(Resources.GetBuiltinResource<Font>("Arial.ttf"));

            consoleText = UIModder.CreateRect("Text", consoleObject)
                .SetPosition(10, 0)
                .SetSize(790, 50)
                .AddText()
                .SetFontSize(28)
                .SetAlignment(TextAlignmentOptions.Left)
                .SetFont(font);

            consoleObject.gameObject.SetActive(false);
        }

        protected override void OnUpdate()
        {
            if (_enabled)
            {
                ProcessKeyInput();
            }

            if (Input.GetKeyDown(KeyCode.Backslash) && (!CoreCache.Input.InputBlocked && CoreCache.Room.CurrentRoom != null || _enabled))
            {
                _enabled = !_enabled;
                consoleObject.gameObject.SetActive(_enabled);

                if (_enabled)
                    OnEnable();
                else
                    OnDisable();
            }
        }

        private void OnEnable()
        {
            LogWarning("Enabling console");
            CoreCache.Input.SetInputBlock(true, false);
            ResetConsole();
        }

        private void OnDisable()
        {
            LogWarning("Disabling console");
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
                    ProcessCommand(_currentText);
                    ResetConsole();
                }
                // Regular character
                else
                {
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
            Log("Processing command: " + command);
        }

        private string _currentText = string.Empty;
    }
}
