using BlasII.ModdingAPI;
using BlasII.ModdingAPI.UI;
using Il2CppTMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using HarmonyLib;
using Il2CppTGK.Game;

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
            if (sceneName == "MainMenu")
                CreateConsoleUI();
        }

        public bool Enabled => _enabled;

        RectTransform console;
        private bool _enabled = false;

        private void CreateConsoleUI()
        {
            console = UIModder.CreateRect("CheatConsole")
                .SetXRange(Vector2.zero).SetYRange(Vector2.zero)
                .SetSize(800, 50)
                .SetPivot(Vector2.zero)
                .AddImage()
                .SetColor(new Color(0.15f, 0.15f, 0.15f, 0.9f))
                .rectTransform;

            //var child = UIModder.CreateRect("Input", console)
            //    .SetXRange(0.5f, 0.5f).SetYRange(0, 0)
            //    .SetPivot(0.5f, 0)
            //    .SetSize(400, 30);
            //.SetPosition(5, -5)
            //.AddImage()
            //.SetColor(Color.blue);

            TMP_FontAsset font = TMP_FontAsset.CreateFontAsset(Resources.GetBuiltinResource<Font>("Arial.ttf"));

            var text = UIModder.CreateRect("Text", console)
                //.SetXRange(0, 1).SetYRange(0, 1)
                //.SetPivot(0, 0)
                .SetPosition(10, 0)
                .SetSize(790, 50)
            //.AddImage()
            //.SetColor(new Color(1, 0, 0, 0.5f))
            //.rectTransform;
            .AddText()
            .SetFontSize(28)
            .SetAlignment(TextAlignmentOptions.Left)
            .SetContents("Test")
            .SetFont(font);

            console.gameObject.SetActive(false);
            //(Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font);

            //input = child.gameObject.AddComponent<TMP_InputField>();
            //input.transition = Selectable.Transition.None;
            //input.textViewport = text.rectTransform;
            //input.textComponent = text;

            //input.text = "Test";
        }

        protected override void OnUpdate()
        {
            if (console != null && Input.GetKeyDown(KeyCode.Backslash) && !CoreCache.Input.InputBlocked)
            {
                console.gameObject.SetActive(!_enabled);

                if (!_enabled)
                    OnEnable();
                else
                    OnDisable();
                _enabled = !_enabled;
            }

            LogWarning("Blocked: " + CoreCache.Input.InputBlocked);
            if (_enabled)
            {
                //EventSystem.current.SetSelectedGameObject(null);
            }
        }

        public void LateUpdate()
        {
            if (_enabled)
            {
                //EventSystem.current.SetSelectedGameObject(console.gameObject);
            }
        }

        private void OnEnable()
        {
            LogWarning("Console enabled");
            //CoreCache.Time.RequestApplicationPause();
            CoreCache.Input.SetInputBlock(true, false);
            //EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.sendNavigationEvents = false;//.GetComponent<StandaloneInputModule>().
            //input.OnPointerClick(new PointerEventData(EventSystem.current));
            //input.ActivateInputField();
        }

        private void OnDisable()
        {
            //EventSystem.current.GetComponent<StandaloneInputModule>().enabled = true;
            EventSystem.current.sendNavigationEvents = true;
            LogWarning("Console disabled");
            CoreCache.Input.ClearAllInputBlocks();
            //CoreCache.Time.RequestApplicationResume();
        }
    }

    [HarmonyPatch(typeof(EventSystem), nameof(EventSystem.SetSelectedGameObject), typeof(GameObject))]
    class test
    {
        public static bool Prefix()
        {
            return !Main.CheatConsole.Enabled;
        }
    }

    //private void CreateConsoleUI()
    //{
    //    console = UIModder.CreateRect("CheatConsole")
    //        .SetSize(400, 200)
    //        .SetPivot(Vector2.zero)
    //        .AddImage()
    //        .SetColor(new Color(0.15f, 0.15f, 0.15f, 0.8f))
    //        .rectTransform;

    //    var child = UIModder.CreateRect("Input", console)
    //        .SetXRange(0.5f, 0.5f).SetYRange(0, 0)
    //        .SetPivot(0.5f, 0)
    //        .SetSize(400, 30);
    //    //.SetPosition(5, -5)
    //    //.AddImage()
    //    //.SetColor(Color.blue);

    //    var text = UIModder.CreateRect("Text", child)
    //        //.SetXRange(0, 1).SetYRange(0, 1)
    //        //.SetPivot(0, 0)
    //        .SetPosition(10, 0)
    //        .SetSize(390, 30)
    //    //.AddImage()
    //    //.SetColor(new Color(1, 0, 0, 0.5f))
    //    //.rectTransform;
    //    .AddText()
    //    .SetFontSize(24)
    //    .SetAlignment(TextAlignmentOptions.Left);

    //    console.gameObject.SetActive(false);
    //    //(Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font);

    //    input = child.gameObject.AddComponent<TMP_InputField>();
    //    input.transition = Selectable.Transition.None;
    //    input.textViewport = text.rectTransform;
    //    input.textComponent = text;

    //    input.text = "Test";
    //}
}
