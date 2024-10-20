using BlasII.CheatConsole.Commands;
using BlasII.Framework.UI;
using BlasII.ModdingAPI;
using BlasII.ModdingAPI.Helpers;
using Il2CppTMPro;
using System.Collections.Generic;
using UnityEngine;

namespace BlasII.CheatConsole;

/// <summary>
/// Adds a console with various debug commands
/// </summary>
public class CheatConsole : BlasIIMod
{
    internal CheatConsole() : base(ModInfo.MOD_ID, ModInfo.MOD_NAME, ModInfo.MOD_AUTHOR, ModInfo.MOD_VERSION) { }

    private readonly Dictionary<string, BaseCommand> _commands = new();

    private RectTransform consoleObject;
    private TextMeshProUGUI consoleText;

    private bool _enabled = false;
    private string _currentText = string.Empty;

    public void RegisterCommand(BaseCommand command)
    {
        if (command.Name.Length > 0 && !_commands.ContainsKey(command.Name))
        {
            _commands.Add(command.Name, command);
            ModLog.Info("Registering new command: " + command.Name);
        }
    }

    /// <summary>
    /// Register and initialize handlers
    /// </summary>
    protected override void OnInitialize()
    {
        InputHandler.RegisterDefaultKeybindings(new Dictionary<string, KeyCode>()
        {
            { "ToggleConsole", KeyCode.Backslash }
        });

        RegisterCommand(new BeadCommand());
        RegisterCommand(new FigureCommand());
        RegisterCommand(new QuestItemCommand());
        RegisterCommand(new PrayerCommand());

        RegisterCommand(new AbilityCommand());

        RegisterCommand(new WeaponCommand());

        RegisterCommand(new HealthCommand());
        RegisterCommand(new FervourCommand());
        RegisterCommand(new FlaskCommand());
        RegisterCommand(new TearsCommand());
        RegisterCommand(new MarksCommand());
        RegisterCommand(new GuiltCommand());

        RegisterCommand(new PrieDieuCommand());
        RegisterCommand(new QuestCommand());

        RegisterCommand(new GodmodeCommand());
        RegisterCommand(new LoadCommand());
    }

    /// <summary>
    /// Disable console when leaving a scene
    /// </summary>
    protected override void OnSceneUnloaded(string sceneName)
    {
        if (_enabled)
        {
            _enabled = false;
            GetConsoleObject().gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Process input and update commands
    /// </summary>
    protected override void OnUpdate()
    {
        if (_enabled)
        {
            ProcessKeyInput();
        }

        bool canToggle = _enabled || (!InputHandler.InputBlocked && SceneHelper.GameSceneLoaded);

        if (InputHandler.GetKeyDown("ToggleConsole") && canToggle)
        {
            _enabled = !_enabled;
            GetConsoleObject().gameObject.SetActive(_enabled);

            if (_enabled)
                OnEnable();
            else
                OnDisable();
        }

        foreach (var cmd in _commands.Values)
        {
            cmd.Update();
        }
    }

    private void OnEnable()
    {
        InputHandler.InputBlocked = true;
        ResetConsole();
    }

    private void OnDisable()
    {
        InputHandler.InputBlocked = false;
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
        ModLog.Custom("[CONSOLE] " + command, System.Drawing.Color.White);
        string[] parts = command.Trim().Split(' ');

        if (parts.Length < 1)
        {
            ModLog.Error($"[CONSOLE] No command was entered!");
            return;
        }
        parts[0] = parts[0].ToLower();

        if (!_commands.ContainsKey(parts[0]))
        {
            ModLog.Error($"[CONSOLE] Command '{parts[0]}' is not a valid command!");
            return;
        }

        if (parts.Length < 2)
        {
            ModLog.Error($"[CONSOLE] Every command needs at least one parameter!");
            return;
        }

        _commands[parts[0]].Execute(parts[1..]);
    }

    private GameObject GetConsoleObject()
    {
        // If console object already exists, return it
        if (consoleObject != null)
            return consoleObject.gameObject;

        // Create console background
        consoleObject = UIModder.Create(new RectCreationOptions()
        {
            Name = "CheatConsole",
            Pivot = Vector2.zero,
            Position = Vector2.zero,
            Size = new Vector2(800, 50),
            XRange = Vector2.zero,
            YRange = Vector2.zero,
        }).AddImage(new ImageCreationOptions()
        {
            Color = new Color(0.15f, 0.15f, 0.15f, 0.9f)
        }).rectTransform;

        // Create console text
        consoleText = UIModder.Create(new RectCreationOptions()
        {
            Name = "Text",
            Parent = consoleObject,
            Position = new Vector2(10, 0),
            Size = new Vector2(790, 50),
        }).AddText(new TextCreationOptions()
        {
            Alignment = TextAlignmentOptions.Left,
            Font = UIModder.Fonts.Arial,
            FontSize = 28,
        });

        consoleObject.gameObject.SetActive(false);
        return consoleObject.gameObject;
    }
}
