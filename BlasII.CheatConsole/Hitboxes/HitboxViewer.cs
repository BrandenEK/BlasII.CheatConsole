using Il2CppInterop.Runtime;
using Il2CppTGK.Game.Components;
using Il2CppTGK.Game.Components.Persistence;
using System.Collections.Generic;
using UnityEngine;

namespace BlasII.CheatConsole.Hitboxes
{
    public class HitboxViewer
    {
        private const float LINE_WIDTH = 0.03f;
        private const string GEOMETRY_NAME = "GEO_Block";

        private readonly Dictionary<int, HitboxComponent> _activeHitboxes = new();

        private bool _showHitboxes = false;

        private readonly float _searchDelay = 1f;
        private float _currentDelay = 0f;
        private bool inGame = false;

        public Sprite HitboxImage { get; private set; }

        private bool ShowHitboxes
        {
            set
            {
                _showHitboxes = value;
                if (value)
                    AddHitboxes();
                else
                    RemoveHitboxes();
            }
        }

        public void CreateHitboxImage()
        {
            var tex = new Texture2D(1, 1, TextureFormat.RGB24, false);
            tex.SetPixel(0, 0, Color.white);
            tex.Apply();
            HitboxImage = Sprite.Create(tex, new Rect(0, 0, 1, 1), Vector2.zero, 1, 0, SpriteMeshType.FullRect);
        }

        public void SceneLoaded(string sceneName)
        {
            inGame = true;
            CreateHitboxImage();

            if (_showHitboxes)
                AddHitboxes();
        }

        public void SceneUnloaded()
        {
            RemoveHitboxes();

            inGame = false;
        }

        public void Update()
        {
            if (_showHitboxes && inGame)
            {
                _currentDelay += Time.deltaTime;
                if (_currentDelay >= _searchDelay)
                    AddHitboxes();
            }

            if (Input.GetKeyDown(KeyCode.Keypad8))
                ShowHitboxes = !_showHitboxes;
        }

        private void AddHitboxes()
        {
            int beforeCount = _activeHitboxes.Count;

            // Remove any colliders that dont exist anymore
            //List<GameObject> toRemove = new();
            //foreach (int colliderId in _activeHitboxes.Keys)
            //{
            //    if (collider == null)
            //        toRemove.Add(collider);
            //}
            //foreach (GameObject collider in toRemove)
            //{
            //    _activeHitboxes.Remove(collider);
            //}

            int midCount = _activeHitboxes.Count;

            // Foreach collider in the scene, add it if it doesn't already exist
            foreach (BoxCollider2D collider in Object.FindObjectsOfType<BoxCollider2D>(true))
            {
                if (collider.name.StartsWith(GEOMETRY_NAME))
                    continue;

                if (_activeHitboxes.ContainsKey(collider.gameObject.GetInstanceID()))
                    continue;

                var obj = new GameObject("Hitbox");
                obj.transform.parent = collider.transform;
                obj.transform.localPosition = Vector3.zero;
                HitboxComponent hitbox = obj.AddComponent(Il2CppType.From(typeof(HitboxComponent))).Cast<HitboxComponent>();
                hitbox.Setup(collider);

                hitbox.UpdateColors();

                _activeHitboxes.Add(collider.gameObject.GetInstanceID(), hitbox);
            }

            int afterCount = _activeHitboxes.Count;

            Main.CheatConsole.LogWarning($"Removed {beforeCount - midCount} destroyed colliders from list");
            Main.CheatConsole.LogWarning($"Added {afterCount - midCount} new colliders to list");
            Main.CheatConsole.LogError($"Total active hitboxes: " + _activeHitboxes.Count);

            ResetTimer();
        }

        private void RemoveHitboxes()
        {
            foreach (HitboxComponent hitbox in _activeHitboxes.Values)
            {
                if (hitbox != null && hitbox.gameObject != null)
                    Object.Destroy(hitbox.gameObject);
            }

            _activeHitboxes.Clear();
        }

        private void ResetTimer()
        {
            _currentDelay = 0;
        }
    }
}
