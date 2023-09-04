using Il2CppInterop.Runtime;
using System.Collections.Generic;
using UnityEngine;

namespace BlasII.CheatConsole.Hitboxes
{
    public class HitboxViewer
    {
        private const string GEOMETRY_NAME = "GEO_Block";

        private readonly Dictionary<int, AbstractHitbox> _activeHitboxes = new();

        private bool _showHitboxes = false;

        private float _currentDelay = 0f;
        private bool _inGame = false;

        public HitboxConfig HitboxConfig { get; private set; }
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

        private Material _material;
        public Material RendererMaterial
        {
            get
            {
                if (_material == null)
                {
                    var obj = new GameObject("Temp");
                    _material = obj.AddComponent<SpriteRenderer>().material;
                    Object.Destroy(obj);
                }
                return _material;
            }
        }

        public void CreateHitboxImage()
        {
            var tex = new Texture2D(1, 1, TextureFormat.RGB24, false);
            tex.SetPixel(0, 0, Color.white);
            tex.Apply();
            HitboxImage = Sprite.Create(tex, new Rect(0, 0, 1, 1), Vector2.zero, 1, 0, SpriteMeshType.FullRect);
        }

        public void Initialize()
        {
            HitboxConfig = new HitboxConfig(true, false, 1f);
        }

        public void SceneLoaded()
        {
            _inGame = true;
            CreateHitboxImage();

            if (_showHitboxes)
                AddHitboxes();

            var list = new List<string>();
            foreach (var c in Object.FindObjectsOfType<Collider2D>())
            {
                string name = c.GetIl2CppType().Name;
                if (!list.Contains(name))
                {
                    Main.CheatConsole.Log(c.name + ": " + name);
                    list.Add(name);
                }
            }
        }

        public void SceneUnloaded()
        {
            RemoveHitboxes();

            _inGame = false;
        }

        public void Update()
        {
            if (_showHitboxes && _inGame)
            {
                _currentDelay += Time.deltaTime;
                if (_currentDelay >= HitboxConfig.updateDelay)
                    AddHitboxes();
            }

            if (Input.GetKeyDown(KeyCode.Keypad8))
                ShowHitboxes = !_showHitboxes;
        }

        private void AddHitboxes()
        {
            float newColliders = 0;

            // Foreach collider in the scene, either add it or update it
            var foundColliders = new List<int>();
            foreach (Collider2D collider in Object.FindObjectsOfType<Collider2D>(true))
            {
                if (!HitboxConfig.showGeometry && collider.name.StartsWith(GEOMETRY_NAME))
                    continue;

                // Make sure the collider is a valid type
                string colliderType = collider.GetIl2CppType().Name;
                if (colliderType != "BoxCollider2D" && colliderType != "CircleCollider2D")
                {
                    continue;
                }

                if (_activeHitboxes.TryGetValue(collider.gameObject.GetInstanceID(), out AbstractHitbox abstractHitbox))
                {
                    // If the collider is already stored, just update the colors
                    abstractHitbox.UpdateColors();
                }
                else
                {
                    var obj = new GameObject("Hitbox");
                    obj.transform.parent = collider.transform;
                    obj.transform.localPosition = Vector3.zero;

                    var lr = obj.AddComponent<LineRenderer>();
                    lr.material = RendererMaterial;
                    lr.useWorldSpace = false;

                    float width = 0.04f;
                    lr.SetWidth(width, width);

                    // Create new hitbox of certain type and call setup
                    if (colliderType == "BoxCollider2D")
                    {
                        BoxHitbox boxHitbox = obj.AddComponent(Il2CppType.From(typeof(BoxHitbox))).Cast<BoxHitbox>();
                        boxHitbox.SetupBox(collider.Cast<BoxCollider2D>(), lr);
                        _activeHitboxes.Add(collider.gameObject.GetInstanceID(), boxHitbox);
                    }
                    else if (colliderType == "CircleCollider2D")
                    {
                        CircleHitbox circleHitbox = obj.AddComponent(Il2CppType.From(typeof(CircleHitbox))).Cast<CircleHitbox>();
                        circleHitbox.SetupCircle(collider.Cast<CircleCollider2D>(), lr);
                        _activeHitboxes.Add(collider.gameObject.GetInstanceID(), circleHitbox);
                    }

                    newColliders++;
                }

                foundColliders.Add(collider.gameObject.GetInstanceID());
            }

            // Foreach collider in the list that wasn't found, remove it
            var destroyedColliders = new List<int>();
            foreach (int colliderId in _activeHitboxes.Keys)
            {
                if (!foundColliders.Contains(colliderId))
                    destroyedColliders.Add(colliderId);
            }
            foreach (int colliderId in destroyedColliders)
            {
                _activeHitboxes.Remove(colliderId);
            }

            // Log amounts and reset timer
            if (destroyedColliders.Count > 0)
                Main.CheatConsole.Log($"Removing {destroyedColliders.Count} old colliders");
            if (newColliders > 0)
                Main.CheatConsole.Log($"Adding {newColliders} new colliders");
            ResetTimer();
        }

        private void RemoveHitboxes()
        {
            foreach (AbstractHitbox hitbox in _activeHitboxes.Values)
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
