using Il2CppTGK.Game.Components;
using Il2CppTGK.Game.Components.Persistence;
using System.Collections.Generic;
using UnityEngine;

namespace BlasII.CheatConsole
{
    public class HitboxViewer
    {
        private const float LINE_WIDTH = 0.03f;
        private const string GEOMETRY_NAME = "GEO_Block";

        private bool _loadedImage = false;
        private Sprite _hitboxImage;
        private readonly Dictionary<int, GameObject> _activeHitboxes = new();

        private bool _showHitboxes = false;

        private readonly float _searchDelay = 1f;
        private float _currentDelay = 0f;
        private bool inGame = false;

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

        public void Initialize()
        {
            Main.CheatConsole.LogWarning("Creating hitbox image");
            var tex = new Texture2D(1, 1, TextureFormat.RGB24, false);
            tex.SetPixel(0, 0, Color.white);
            tex.Apply();
            _hitboxImage = Sprite.Create(tex, new Rect(0, 0, 1, 1), Vector2.zero, 1, 0, SpriteMeshType.FullRect);
            //_loadedImage = true;
        }

        public void SceneLoaded(string sceneName)
        {
            inGame = true;

            if (!_loadedImage && sceneName != "MainMenu")
                Initialize();

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
            GameObject baseHitbox = CreateBaseHitbox();

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

                GameObject hitbox = Object.Instantiate(baseHitbox, collider.transform);
                Transform side;
                hitbox.transform.localPosition = Vector3.zero;

                Color color;
                if (!collider.isActiveAndEnabled)
                    color = Color.gray;
                else if (HasComponentInParent<PlayerPersistentComponent>(collider.gameObject))
                    color = Color.cyan;
                else if (HasComponentInParent<AliveEntity>(collider.gameObject))
                    color = Color.red;
                else if (collider.isTrigger)
                    color = Color.green;
                else
                    color = Color.yellow;

                float xPos = collider.offset.x;
                float yPos = collider.offset.y;
                float width = LINE_WIDTH / collider.transform.localScale.x;
                float height = LINE_WIDTH / collider.transform.localScale.y;

                side = hitbox.transform.GetChild(0);
                side.localPosition = new Vector3(xPos - collider.size.x / 2, yPos + collider.size.y / 2, 0);
                side.localScale = new Vector3(collider.size.x + LINE_WIDTH, height, 0);
                side.GetComponent<SpriteRenderer>().color = color;

                side = hitbox.transform.GetChild(1);
                side.localPosition = new Vector3(xPos - collider.size.x / 2, yPos - collider.size.y / 2, 0);
                side.localScale = new Vector3(width, collider.size.y, 0);
                side.GetComponent<SpriteRenderer>().color = color;

                side = hitbox.transform.GetChild(2);
                side.localPosition = new Vector3(xPos + collider.size.x / 2, yPos - collider.size.y / 2, 0);
                side.localScale = new Vector3(width, collider.size.y, 0);
                side.GetComponent<SpriteRenderer>().color = color;

                side = hitbox.transform.GetChild(3);
                side.localPosition = new Vector3(xPos - collider.size.x / 2, yPos - collider.size.y / 2, 0);
                side.localScale = new Vector3(collider.size.x + LINE_WIDTH, height, 0);
                side.GetComponent<SpriteRenderer>().color = color;

                _activeHitboxes.Add(collider.gameObject.GetInstanceID(), hitbox);
            }

            int afterCount = _activeHitboxes.Count;

            Main.CheatConsole.LogWarning($"Removed {beforeCount - midCount} destroyed colliders from list");
            Main.CheatConsole.LogWarning($"Added {afterCount - midCount} new colliders to list");
            Main.CheatConsole.LogError($"Total active hitboxes: " + _activeHitboxes.Count);

            Object.Destroy(baseHitbox);
            ResetTimer();
        }

        private void RemoveHitboxes()
        {
            foreach (GameObject hitbox in _activeHitboxes.Values)
            {
                if (hitbox != null)
                    Object.Destroy(hitbox);
            }

            _activeHitboxes.Clear();
        }

        private void ResetTimer()
        {
            _currentDelay = 0;
        }

        private bool HasComponentInParent<T>(GameObject obj)
        {
            Transform parent = obj.transform;
            while (parent != null)
            {
                if (parent.GetComponent<T>() != null)
                    return true;
                parent = parent.parent;
            }

            return false;
        }

        private GameObject CreateBaseHitbox()
        {
            var baseHitbox = new GameObject("Hitbox");
            GameObject side;

            side = new GameObject("TOP");
            side.AddComponent<SpriteRenderer>().sprite = _hitboxImage;
            side.transform.parent = baseHitbox.transform;

            side = new GameObject("LEFT");
            side.AddComponent<SpriteRenderer>().sprite = _hitboxImage;
            side.transform.parent = baseHitbox.transform;

            side = new GameObject("RIGHT");
            side.AddComponent<SpriteRenderer>().sprite = _hitboxImage;
            side.transform.parent = baseHitbox.transform;

            side = new GameObject("BOTTOM");
            side.AddComponent<SpriteRenderer>().sprite = _hitboxImage;
            side.transform.parent = baseHitbox.transform;

            return baseHitbox;
        }
    }
}
