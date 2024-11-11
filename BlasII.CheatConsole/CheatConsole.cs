using BlasII.CheatConsole.Commands;
using BlasII.Framework.UI;
using BlasII.ModdingAPI;
using BlasII.ModdingAPI.Assets;
using BlasII.ModdingAPI.Helpers;
using Il2CppTGK.Inventory;
using Il2CppTMPro;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BlasII.CheatConsole;

/// <summary>
/// Adds a console with various debug commands
/// </summary>
public class CheatConsole : BlasIIMod
{
    internal CheatConsole() : base(ModInfo.MOD_ID, ModInfo.MOD_NAME, ModInfo.MOD_AUTHOR, ModInfo.MOD_VERSION) { }

    private RectTransform consoleObject;
    private TextMeshProUGUI consoleText;

    private bool _enabled = false;
    private string _currentText = string.Empty;

    /// <summary>
    /// Register and initialize handlers
    /// </summary>
    protected override void OnInitialize()
    {
        InputHandler.RegisterDefaultKeybindings(new Dictionary<string, KeyCode>()
        {
            { "ToggleConsole", KeyCode.Backslash }
        });
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

        foreach (var cmd in CommandRegister.Commands)
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

        ModCommand cmd = CommandRegister.Commands.FirstOrDefault(x => x.Name == parts[0]);
        if (cmd == null)
        {
            ModLog.Error($"[CONSOLE] Command '{parts[0]}' is not a valid command!");
            return;
        }

        if (cmd.NeedsParameters && parts.Length < 2)
        {
            ModLog.Error($"[CONSOLE] Command '{parts[0]}' needs at least one parameter!");
            return;
        }

        cmd.Execute(parts[1..]);
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

    /// <summary>
    /// Register all built-in commands
    /// </summary>
    protected override void OnRegisterServices(ModServiceProvider provider)
    {
        provider.RegisterCommand(new HelpCommand());

        provider.RegisterCommand(new ItemCommand<RosaryBeadItemID>("bead", AssetStorage.Beads));
        provider.RegisterCommand(new ItemCommand<FigureItemID>("figure", AssetStorage.Figures));
        provider.RegisterCommand(new ItemCommand<PrayerItemID>("prayer", AssetStorage.Prayers));
        provider.RegisterCommand(new ItemCommand<QuestItemID>("questitem", AssetStorage.QuestItems));

        provider.RegisterCommand(new AbilityCommand());
        provider.RegisterCommand(new WeaponCommand());

        provider.RegisterCommand(new RangeStatCommand("health", "Health"));
        provider.RegisterCommand(new RangeStatCommand("fervour", "Fervour"));
        provider.RegisterCommand(new RangeStatCommand("flask", "Flask"));
        provider.RegisterCommand(new ValueStatCommand("tears", "Tears"));
        provider.RegisterCommand(new ValueStatCommand("marks", "Orbs"));
        provider.RegisterCommand(new ValueStatCommand("pmarks", "MarksPreceptor"));
        provider.RegisterCommand(new ModifyStatCommand("attack", "BasePhysicalattack"));
        provider.RegisterCommand(new ModifyStatCommand("defense", "GlobalDefense"));
        provider.RegisterCommand(new GuiltCommand());

        provider.RegisterCommand(new PrieDieuCommand());
        provider.RegisterCommand(new QuestCommand());

        provider.RegisterCommand(new GodmodeCommand());
        provider.RegisterCommand(new LoadCommand());
    }
}
